using FourthPharos.Domain.CandelaObscuraCircle;
using FourthPharos.Domain.CandelaObscuraCircle.Features;
using FourthPharos.Domain.CandelaObscuraCircle.Models;
using FourthPharos.Domain.CandelaObscuraCircle.Operations;
using FourthPharos.Domain.Features;

namespace FourthPharos.Domain.Tests.CandelaObscuraCircle.Operations;

public class UpdateGearOperationTest
{
    [Fact]
    public void UpdateGear()
    {
        var circle = CircleFactory.CreateCirle("Test Circle");
        var gear = circle
            .AddGear("Lanterna Obscura")
            .GetFeature<Circle, CircleGearFeature>()
            .Gear
            .Single();

        circle = circle.UpdateGear(gear.Id, "Black Lantern");

        circle.GetFeature<Circle, CircleGearFeature>().Gear.ShouldHaveSingleItem().Name.ShouldBe("Black Lantern");
    }

    [Fact]
    public void EmptyNameFails()
    {
        var circle = CircleFactory.CreateCirle("Test Circle");
        var gear = circle
            .AddGear("Lanterna Obscura")
            .GetFeature<Circle, CircleGearFeature>()
            .Gear
            .Single();

        Should
            .Throw<DomainActionException>(() => circle.UpdateGear(gear.Id, string.Empty))
            .Code
            .ShouldBe(nameof(DomainExceptions.CircleGearExceptions.GearNameEmpty));
    }

    [Fact]
    public void NonExistantGearFails() =>
        Should
            .Throw<DomainActionException>(() => CircleFactory.CreateCirle("Test Circle").UpdateGear(Guid.Empty, "Lanterna Obscura"))
            .Code
            .ShouldBe(nameof(DomainExceptions.CircleExceptions.InvalidGear));
}