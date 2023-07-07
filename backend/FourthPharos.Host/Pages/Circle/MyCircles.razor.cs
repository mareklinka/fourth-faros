using FourthPharos.Host.Extensions;

namespace FourthPharos.Host.Pages.Circle;

public partial class MyCircles
{
    private Guid userId = Guid.Empty;

    protected override async Task OnInitializedAsync()
    {
        var authState = await authStateProvider.GetAuthenticationStateAsync();
        userId = authState.User.GetUserId();
    }

    private void CreateCircle(string name) => circleService.CreateCircle(name, userId);

    private void DeleteCircle(Guid id) => circleService.DeleteCircle(id);
}
