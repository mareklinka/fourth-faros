using FourthPharos.Domain.CandelaObscuraCircle.Features;
using FourthPharos.Domain.CandelaObscuraCircle.Models;
using FourthPharos.Domain.CandelaObscuraCircle.Operations;
using FourthPharos.Domain.Features;
using FourthPharos.Host.Extensions;
using FourthPharos.Host.Models;
using Microsoft.AspNetCore.Components;

namespace FourthPharos.Host.Pages.Circle;

public partial class CircleEditor
{
    [Parameter]
    public Guid? Id { get; set; }

    public CircleModel Model { get; set; } = null!;

    private Guid userId = Guid.Empty;

    private string? CircleName
    {
        get => Model.Circle.GetFeature<Domain.CandelaObscuraCircle.Models.Circle, CircleNameFeature>().Name;
        set => Model.Circle.SetName(value);
    }

    private string? CircleLocation
    {
        get => Model.Circle.GetFeature<Domain.CandelaObscuraCircle.Models.Circle, CircleLocationFeature>().Location;
        set => Model.Circle.SetLocation(value);
    }

    private bool IsCircleNameInvalid => string.IsNullOrWhiteSpace(CircleName);

    private readonly IReadOnlyCollection<AbilityModel> abilities = new List<AbilityModel>
    {
        new AbilityModel(CircleAbility.StaminaTraining.Code, "Stamina Training"),
        new AbilityModel(CircleAbility.NobodyLeftBehind.Code, "Nobody Left Behind"),
        new AbilityModel(CircleAbility.ForgedInFire.Code, "Forged in Fire"),
        new AbilityModel(CircleAbility.Interdisciplinary.Code, "Interdisciplinary"),
        new AbilityModel(CircleAbility.ResourceManagement.Code, "Resource Management"),
        new AbilityModel(CircleAbility.OneLastRun.Code, "One Last Run"),
    };

    protected override async Task OnInitializedAsync()
    {
        base.OnInitialized();

        var authState = await authStateProvider.GetAuthenticationStateAsync();
        userId = authState.User.GetUserId();

        if (Id is null)
        {
            Model = circleService.CreateCircle(null, userId);
        }
        else
        {
            var circle = circleService.GetCircles().SingleOrDefault(_ => _.Id == Id);

            if (circle is not null)
            {
                Model = circle;
            }
        }
    }

    private void SetAbility(int rank, string? abilityCode) =>
        Model.Circle.SelectAbility(string.IsNullOrWhiteSpace(abilityCode) ? null : abilityCode, rank);

    private IEnumerable<AbilityModel> GetAvailableAbilities() =>
        abilities.Where(a => !Model
            .Circle
            .GetFeature<Domain.CandelaObscuraCircle.Models.Circle, CircleAbilitiesFeature>()
            .Abilities
            .Select(_ => _.Code)
            .Contains(a.Code));

    private AbilityModel? GetAbilityAtRank(int rank)
    {
        var ability = Model
            .Circle
            .GetFeature<Domain.CandelaObscuraCircle.Models.Circle, CircleAbilitiesFeature>()
            .Abilities
            .FirstOrDefault(_ => _.TakenAtRank == rank);

        return abilities.FirstOrDefault(_ => _.Code == ability?.Code);
    }

    private record AbilityModel(string? Code, string Name);
}