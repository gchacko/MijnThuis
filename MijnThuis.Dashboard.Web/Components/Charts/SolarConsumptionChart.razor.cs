using ApexCharts;
using MediatR;
using MijnThuis.Contracts.Solar;
using MijnThuis.Dashboard.Web.Model.Charts;
using System.Globalization;

namespace MijnThuis.Dashboard.Web.Components.Charts;

public partial class SolarConsumptionChart
{
    private readonly PeriodicTimer _periodicTimer = new(TimeSpan.FromMinutes(1));
    private ApexChart<ChartDataEntry<string, decimal>> _apexChart = null!;
    private ApexChartOptions<ChartDataEntry<string, decimal>> _options { get; set; } = new();

    private ChartData3<string, decimal> SolarPower { get; set; } = new();
    private DateTime _solarDate = DateTime.Today;

    public string TitleDescription { get; set; }

    public SolarConsumptionChart()
    {
        _options.Chart = new Chart
        {
            Stacked = true,
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
            OverwriteCategories = Enumerable.Range(0, 24 * 4).Select(x => new DateTime().AddMinutes(15 * x).Minute == 0 ? $"{new DateTime().AddMinutes(15 * x):HH:mm}" : "").ToList()
        };
        _options.Theme = new Theme
        {
            Mode = Mode.Dark,
            Palette = PaletteType.Palette1
        };
        _options.Colors = new List<string> { "#B0D8FD", "#93B6FB", "#FBB550" };
        _options.Stroke = new Stroke
        {
            Show = false
        };
        _options.Fill = new Fill
        {
            Type = new List<FillType> { FillType.Solid, FillType.Solid, FillType.Solid },
            Opacity = new Opacity(1, 1, 1)
        };

        SolarPower.Description = "Zonne-energie: Consumptie";
        SolarPower.Series1Description = "Consumptie vanuit batterij";
        SolarPower.Series2Description = "Consumptie vanuit PV";
        SolarPower.Series3Description = "Consumptie vanuit het net";
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

            var date = DateTime.Today;

            var response = await mediator.Send(new GetSolarPowerHistoryQuery
            {
                From = _solarDate,
                To = _solarDate,
                Unit = PowerHistoryUnit.FifteenMinutes
            });

            TitleDescription = string.Create(CultureInfo.GetCultureInfo("nl-be"), $"De consumptie op {_solarDate:dd MMMM yyyy}");

            SolarPower.Clear();
            SolarPower.Series1.AddRange(FillData(response.Entries.Select(x => new ChartDataEntry<string, decimal>
            {
                XValue = $"{x.Date:dd/MM/yyyy HH:mm}",
                YValue = x.ConsumptionFromBattery
            }), 24 * 4, n => $"{date.AddMinutes(n * 15):dd/MM/yyyy HH:mm}"));
            SolarPower.Series2.AddRange(FillData(response.Entries.Select(x => new ChartDataEntry<string, decimal>
            {
                XValue = $"{x.Date:dd/MM/yyyy HH:mm}",
                YValue = x.ConsumptionFromSolar
            }), 24 * 4, n => $"{date.AddMinutes(n * 15):dd/MM/yyyy HH:mm}"));
            SolarPower.Series3.AddRange(FillData(response.Entries.Select(x => new ChartDataEntry<string, decimal>
            {
                XValue = $"{x.Date:dd/MM/yyyy HH:mm}",
                YValue = x.ConsumptionFromGrid
            }), 24 * 4, n => $"{date.AddMinutes(n * 15):dd/MM/yyyy HH:mm}"));
            await _apexChart.UpdateSeriesAsync(true);

            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to refresh graph data");
        }
    }

    private async Task NavigateBeforeCommand()
    {
        _solarDate = _solarDate.AddDays(-1);

        await RefreshData();
    }

    private async Task NavigateNextCommand()
    {
        _solarDate = _solarDate.AddDays(1);

        await RefreshData();
    }

    public void Dispose()
    {
        _periodicTimer.Dispose();
    }

    private IEnumerable<ChartDataEntry<TX, TY>> FillData<TX, TY>(IEnumerable<ChartDataEntry<TX, TY>> source, int total, Func<int, TX> generator)
    {
        var result = new List<ChartDataEntry<TX, TY>>();

        result.AddRange(source);
        result.AddRange(Enumerable.Range(source.Count(), total - source.Count()).Select(n => new ChartDataEntry<TX, TY>
        {
            XValue = generator(n),
            YValue = default!
        }));

        return result;
    }
}