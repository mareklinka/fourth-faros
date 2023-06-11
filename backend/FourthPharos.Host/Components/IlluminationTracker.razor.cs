using Microsoft.AspNetCore.Components;

namespace FourthPharos.Host.Components;

public partial class IlluminationTracker
{
    [Parameter]
    public int Illumination { get; set; }

    [Parameter]
    public int Rank { get; set; }

    [Parameter]
    public EventCallback<int> PipClicked { get; set; }

    private int NormalizedIIlumination { get; set; }

    protected override void OnParametersSet() => NormalizedIIlumination = Illumination % 24;
}