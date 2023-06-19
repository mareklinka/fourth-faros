using FourthPharos.Domain.CandelaObscuraCircle.Features;
using FourthPharos.Domain.CandelaObscuraCircle.Models;
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

    private string? newGearName;

    private string? NewGearName
    {
        get => newGearName; set => AddNewGear(value);
    }

    private IReadOnlyDictionary<string, bool> SelectedAbilities { get; set; } = new Dictionary<string, bool>();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var authState = await authStateProvider.GetAuthenticationStateAsync();
        userId = authState.User.GetUserId();

        var circle = circleService.GetCircles().SingleOrDefault(_ => _.Id == Id);

        if (circle is null)
        {
            return;
        }

        Model = circle;
        UpdateSelectedAbilities();
    }

    private void UpdateSelectedAbilities()
    {
        SelectedAbilities = Model!
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

    private void ToggleIlluminationPip(int pip)
    {
        var feature = Model!.Circle.GetFeature<Domain.CandelaObscuraCircle.Models.Circle, CircleIlluminationFeature>();

        if ((feature.Illumination % 24) >= pip)
        {
            if (feature.Illumination > 0)
            {
                Model.Circle.AddIllumination(-1);
                UpdateSelectedAbilities();
            }
        }
        else
        {
            Model.Circle.AddIllumination(1);
        }
    }

    private void ToggleResourcePip(int pip, CircleResource resource)
    {
        var feature = Model!.Circle.GetFeature<Domain.CandelaObscuraCircle.Models.Circle, CircleResourcesFeature>();

        var current = feature.Resources[resource];

        if (current >= pip)
        {
            Model.Circle.ConsumeResource(resource);
        }
        else if (current < feature.ResourceMaximum)
        {
            Model.Circle.RestoreResource(resource);
        }
    }

    private int ResourceMax =>
        Model!.Circle.GetFeature<Domain.CandelaObscuraCircle.Models.Circle, CircleResourcesFeature>().ResourceMaximum;

    private int GetResource(CircleResource resource) =>
        Model!.Circle.GetFeature<Domain.CandelaObscuraCircle.Models.Circle, CircleResourcesFeature>().Resources[resource];

    private void AddNewGear(string? gearName)
    {
        if (string.IsNullOrEmpty(gearName))
        {
            return;
        }

        Model!.Circle.AddGear(gearName);
        newGearName = null;
    }

    private void UpdateGear(Guid id, string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Model!.Circle.RemoveGear(id);
        }
        else
        {
            Model!.Circle.UpdateGear(id, value);
            Console.WriteLine($"Update gear {id} to {value}");
        }
    }
}
