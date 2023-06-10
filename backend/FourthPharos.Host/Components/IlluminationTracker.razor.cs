using Microsoft.AspNetCore.Components;

namespace FourthPharos.Host.Components;

public partial class IlluminationTracker
{
    [Parameter]
    public int Illumination { get; set; }

    private int NormalizedIIlumination { get; set; }

    protected override void OnInitialized() => NormalizedIIlumination = Illumination % 24;
}