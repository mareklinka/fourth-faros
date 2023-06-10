using FourthPharos.Domain.CandelaObscuraCircle.Features;
using FourthPharos.Domain.CandelaObscuraCircle.Operations;
using FourthPharos.Domain.Features;
using FourthPharos.Host.Extensions;
using FourthPharos.Host.Models;
using Microsoft.AspNetCore.Components;

namespace FourthPharos.Host.Pages.Circle;

public partial class CircleSheet
{
    [Parameter]
    public Guid? Id { get; set; }

    private CircleModel? Model { get; set; }

    private Guid userId = Guid.Empty;

    private IReadOnlyDictionary<string, bool> SelectedAbilities { get; set; } = new Dictionary<string, bool>();

    protected override async Task OnInitializedAsync()
    {
        base.OnInitialized();

        var authState = await authStateProvider.GetAuthenticationStateAsync();
        userId = authState.User.GetUserId();

        var circle = circleService.GetCircles().SingleOrDefault(_ => _.Id == Id);

        if (circle is null)
        {
            return;
        }

        Model = circle;
        SelectedAbilities = Model
            .Circle
            .GetFeature<Domain.CandelaObscuraCircle.Models.Circle, CircleAbilitiesFeature>()
            .Abilities
            .ToDictionary(_ => _.Code, _ => true);
    }

    private int GetStaminaDice() =>
        Model!.Circle.GetFeature<Domain.CandelaObscuraCircle.Models.Circle, StaminaTrainingFeature>().StaminaDice;

    private void ToggleStaminaDie(int die)
    {
        var feature = Model!.Circle.GetFeature<Domain.CandelaObscuraCircle.Models.Circle, StaminaTrainingFeature>();

        if (feature.StaminaDice >= die)
        {
            Model.Circle.ConsumeStaminaDice();
        }
        else
        {
            Model.Circle.RestoreStaminaDice();
        }
    }
}