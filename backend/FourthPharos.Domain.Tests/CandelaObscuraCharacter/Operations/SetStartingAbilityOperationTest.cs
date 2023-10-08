using FourthPharos.Domain.CandelaObscuraCharacter;
using FourthPharos.Domain.CandelaObscuraCharacter.Features;
using FourthPharos.Domain.CandelaObscuraCharacter.Models;
using FourthPharos.Domain.CandelaObscuraCharacter.Operations;
using FourthPharos.Domain.Features;

namespace FourthPharos.Domain.Tests.CandelaObscuraCharacter.Operations;

public class SetStartingAbilityOperationTest
{
    [Fact]
    public void SetStartingAbility()
    {
        var character = CharacterFactory.CreateCharacter("Crowley Thornwood");
        character.SetRole(Models.CharacterSpecialty.Explorer);

        character.SetStartingAbility(CharacterAbility.BehindMe);

        character.GetFeature<Character, CharacterStartingAbilityFeature>().Ability
            .ShouldNotBeNull().Code
            .ShouldBe(CharacterAbility.BehindMe.Code);
    }

    [Fact]
    public void ClearStartingAbility()
    {
        var character = CharacterFactory.CreateCharacter("Crowley Thornwood");
        character.SetRole(Models.CharacterSpecialty.Explorer);

        character.SetStartingAbility(CharacterAbility.BehindMe);

        character.GetFeature<Character, CharacterStartingAbilityFeature>().Ability
            .ShouldNotBeNull().Code
            .ShouldBe(CharacterAbility.BehindMe.Code);

        character.SetStartingAbility(null);

        character.GetFeature<Character, CharacterStartingAbilityFeature>().Ability.ShouldBeNull();
    }
}
