using FourthPharos.Domain.CandelaObscuraCircle.Features;
using FourthPharos.Domain.CandelaObscuraCircle.Models;
using FourthPharos.Domain.Features;

namespace FourthPharos.Domain.CandelaObscuraCircle;

public static class CircleFactory
{
    public static Circle CreateCirle(string? name)
    {
        CircleValidators.Name(name);

        return new Circle(Guid.NewGuid())
            .AddFeature(t => new CircleNameFeature(t, name))
            .AddFeature(t => new CircleLocationFeature(t))
            .AddFeature(t => new CircleAbilitiesFeature(t))
            .AddFeature(t => new CircleIlluminationFeature(t))
            .AddFeature(t => new CircleGearFeature(t))
            .AddFeature(t => new CircleResourcesFeature(t))
            .AddFeature(t => new CircleActiveAssignmentFeature(t));
    }
}
