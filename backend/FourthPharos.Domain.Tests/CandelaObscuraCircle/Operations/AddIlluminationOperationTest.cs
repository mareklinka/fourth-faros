using FourthPharos.Domain.CandelaObscuraCircle;
using FourthPharos.Domain.CandelaObscuraCircle.Features;
using FourthPharos.Domain.CandelaObscuraCircle.Models;
using FourthPharos.Domain.CandelaObscuraCircle.Operations;
using FourthPharos.Domain.Features;

namespace FourthPharos.Domain.Tests.CandelaObscuraCircle.Operations;

public class AddIlluminationOperationTest
{
    [Theory]
    [MemberData(nameof(IlluminationData))]
    public void AddOneIllumination(int illumination) =>
        CircleFactory
            .CreateCirle("Test Circle")
            .AddIllumination(illumination)
            .GetFeature<Circle, CircleIlluminationFeature>()
            .Illumination
            .ShouldBe(illumination);

    [Fact]
    public void RemovingRankRemovesSelectedAbility() =>
        CircleFactory
            .CreateCirle("Test Circle")
            .SelectAbility(CircleAbility.StaminaTraining.Code, 1)
            .AddIllumination(24)
            .SelectAbility(CircleAbility.ForgedInFire.Code, 2)
            .AddIllumination(-10)
            .GetFeature<Circle, CircleAbilitiesFeature>()
            .Abilities
            .ShouldHaveSingleItem()
            .Code
            .ShouldBe(CircleAbility.StaminaTraining.Code);

    public static IEnumerable<object[]> IlluminationData => Enumerable.Range(1, 100).Select(_ => new object[] { _ });
}
