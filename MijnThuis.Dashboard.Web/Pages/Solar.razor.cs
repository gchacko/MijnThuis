using Microsoft.AspNetCore.Components;

namespace MijnThuis.Dashboard.Web.Pages;

public partial class Solar
{
    [Inject]
    protected NavigationManager NavigationManager { get; set; }


    public void BackCommand()
    {
        NavigationManager.NavigateTo($"/{new Uri(NavigationManager.Uri).Query}");
    }
}