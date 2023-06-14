using Microsoft.AspNetCore.Components;

namespace FourthPharos.Host.Components;

public partial class ResourceCounter
{
    [Parameter]
    public int Max { get; set; }

    [Parameter]
    public int Available { get; set; }

    [Parameter]
    public string Name { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<int> PipClicked { get; set; }
}