using FourthPharos.Domain.CandelaObscuraCircle.Features;
using FourthPharos.Domain.CandelaObscuraCircle.Models;
using FourthPharos.Domain.Features;

namespace FourthPharos.Domain.CandelaObscuraCircle.Operations;

public static class UpdateGearOperation
{
    public static Circle UpdateGear(this Circle circle, Guid id, string newName)
    {
        var feature = circle.GetFeature<Circle, CircleGearFeature>();

        CircleGearValidators.Name(newName);

        var item = feature.Gear.FirstOrDefault(_ => _.Id == id);

        return item switch
        {
            CircleGear =>
                circle.UpdateFeature(feature with
                {
                    Gear = feature.Gear.Insert(feature.Gear.IndexOf(item), item with { Name = newName }).Remove(item)
                }),
            null => throw DomainExceptions.CircleExceptions.InvalidGear(id)
        };
    }
}