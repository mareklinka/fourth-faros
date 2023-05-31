using FourthFaros.Domain.Circle;
using FourthFaros.Domain.Circle.Features;
using FourthFaros.Domain.Circle.Models;
using FourthFaros.Domain.Circle.Operations;
using FourthFaros.Domain.Features;
using Shouldly;

namespace FourthFaros.Domain.Tests.Circle;

public class RestoreStaminaDiceOperationTest
{
    [Fact]
    public void RestoreStaminaDie() =>
        CircleFactory
            .CreateCirle("Test Circle", CircleAbility.StaminaTraining)
            .ConsumeStaminaDice()
            .RestoreStaminaDice()
            .GetFeature<CircleBase, StaminaTrainingFeature>()
            .StaminaDice
            .ShouldBe(3);

    [Fact]
    public void RestoreFullStaminaDice() =>
        Should.Throw<DomainActionException>(() =>
            CircleFactory
                .CreateCirle("Test Circle", CircleAbility.StaminaTraining)
                .RestoreStaminaDice())
            .Code
            .ShouldBe(nameof(DomainExceptions.CircleExceptions.StaminaDiceFull));
}
