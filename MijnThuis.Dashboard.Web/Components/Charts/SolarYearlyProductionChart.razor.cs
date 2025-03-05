using ApexCharts;
using Azure;
using MediatR;
using MijnThuis.Contracts.Solar;
using MijnThuis.Dashboard.Web.Model.Charts;

namespace MijnThuis.Dashboard.Web.Components.Charts;

public partial class SolarYearlyProductionChart
{
    private readonly PeriodicTimer _periodicTimer = new(TimeSpan.FromMinutes(15));
    private ApexChart<ChartDataEntry<string, decimal>> _apexChart = null!;
    private ApexChartOptions<ChartDataEntry<string, decimal>> _options { get; set; } = new();

    private ChartData3<string, decimal> SolarPower { get; set; } = new();

    public SolarYearlyProductionChart()
    {
        _options.Chart = new Chart
        {
            Toolbar = new Toolbar
            {
                Show = false
            },
            Zoom = new Zoom
            {
                Enabled = false
            },
            Background = "#373740",
        };
        _options.Xaxis = new XAxis
        {
            Type = XAxisType.Category,
            OverwriteCategories = ["Jan", "Feb", "Maa", "Apr", "Mei", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dec"],
        };
        _options.Theme = new Theme
        {
            Mode = Mode.Dark,
            Palette = PaletteType.Palette1
        };
        _options.Colors = new List<string> { "#001EC6", "#34B9C9", "#C021C7" };
        _options.Stroke = new Stroke
        {
            Show = false
        };
        _options.Fill = new Fill
        {
            Type = new List<FillType> { FillType.Solid, FillType.Solid, FillType.Solid },
            Opacity = new Opacity(1, 1, 1)
        };

        SolarPower.Description = $"Zonne-energie: Jaarlijkse opbrengst {DateTime.Today.Year - 2} - {DateTime.Today.Year}";
        SolarPower.Series1Description = $"Opbrengst {DateTime.Today.Year - 2}";
        SolarPower.Series2Description = $"Opbrengst {DateTime.Today.Year - 1}";
        SolarPower.Series3Description = $"Opbrengst {DateTime.Today.Year}";
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _ = RunTimer();
        }

        return base.OnAfterRenderAsync(firstRender);
    }

    private async Task RunTimer()
    {
        await RefreshData();

        while (await _periodicTimer.WaitForNextTickAsync())
        {
            try
            {
                await RefreshData();
            }
            catch (ObjectDisposedException)
            {
                _periodicTimer.Dispose();
                break;
            }
        }
    }

    private async Task RefreshData()
    {
        try
        {
            using var scope = ServiceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var beginTwoYearsAgo = new DateTime(DateTime.Today.Year - 2, 1, 1);
            var endThisYear = new DateTime(DateTime.Today.Year, 12, 31);

            var response = await mediator.Send(new GetSolarEnergyHistoryQuery
            {
                From = beginTwoYearsAgo,
                To = endThisYear,
                Unit = EnergyHistoryUnit.Month
            });

            SolarPower.Clear();
            SolarPower.Description = "Zonne-energie: Jaarlijkse productie";
            SolarPower.Series1Description = "Consumptie vanuit PV";
            SolarPower.Series1.AddRange(FillData(response.Entries
                .Where(x => x.Date.Year == DateTime.Today.Year - 2)
                .Select(x => new ChartDataEntry<string, decimal>
                {
                    XValue = $"{x.Date:MMMM yyyy}",
                    YValue = x.Production
                }), 12, n => $"{new DateTime(DateTime.Today.Year - 2, n, 1):MMMM yyyy}"));
            SolarPower.Series2Description = "Consumptie vanuit batterij";
            SolarPower.Series2.AddRange(FillData(response.Entries
                .Where(x => x.Date.Year == DateTime.Today.Year - 1)
                .Select(x => new ChartDataEntry<string, decimal>
                {
                    XValue = $"{x.Date:MMMM yyyy}",
                    YValue = x.Production
                }), 12, n => $"{new DateTime(DateTime.Today.Year - 1, n, 1):MMMM yyyy}"));
            SolarPower.Series3Description = "Consumptie vanuit het net";
            SolarPower.Series3.AddRange(FillData(response.Entries
                .Where(x => x.Date.Year == DateTime.Today.Year)
                .Select(x => new ChartDataEntry<string, decimal>
                {
                    XValue = $"{x.Date:MMMM yyyy}",
                    YValue = x.Production
                }), 12, n => $"{new DateTime(DateTime.Today.Year, n, 1):MMMM yyyy}"));
            await _apexChart.UpdateSeriesAsync(true);

            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to refresh graph data");
        }
    }

    public void Dispose()
    {
        _periodicTimer.Dispose();
    }

    private IEnumerable<ChartDataEntry<TX, TY>> FillData<TX, TY>(IEnumerable<ChartDataEntry<TX, TY>> source, int total, Func<int, TX> generator)
    {
        var result = new List<ChartDataEntry<TX, TY>>();

        for (int i = 1; i <= total; i++)
        {
            var fromSource = source.SingleOrDefault(x => x.XValue.Equals(generator(i)));
            if (fromSource != null)
            {
                result.Add(fromSource);
                continue;
            }
            else
            {
                result.Add(new ChartDataEntry<TX, TY>
                {
                    XValue = generator(i),
                    YValue = default!
                });
            }
        }

        return result;
    }
}