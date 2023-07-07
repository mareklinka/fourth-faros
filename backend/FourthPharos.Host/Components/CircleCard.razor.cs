using Microsoft.AspNetCore.Components;

namespace FourthPharos.Host.Components;

public partial class CircleCard
{
    [Parameter]
    public Guid Id { get; set; } = Guid.Empty;

    [Parameter]
    public string Name { get; set; } = string.Empty;

    [Parameter]
    public int Members { get; set; }

    [Parameter]
    public string Location { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<Guid> OnCircleDeleted { get; set; }

    private Task DeleteCircle() => OnCircleDeleted.InvokeAsync(Id);
}