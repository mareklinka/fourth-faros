using FourthPharos.Domain.CandelaObscuraCharacter;
using FourthPharos.Domain.CandelaObscuraCharacter.Features;
using FourthPharos.Domain.CandelaObscuraCharacter.Models;
using FourthPharos.Domain.CandelaObscuraCharacter.Operations;
using FourthPharos.Domain.Features;
using FourthPharos.Domain.Models;

namespace FourthPharos.Domain.Tests.CandelaObscuraCharacter.Operations;

public class SetRoleOperationTest
{
    [Theory]
    [MemberData(nameof(SpecialtyData))]
    public void SetRole(CharacterSpecialty specialty)
    {
        var character = CharacterFactory.CreateCharacter("Crowley Thornwood");
        character.SetRole(specialty);

        character.GetFeature<Character, CharacterRoleFeature>().Specialty.ShouldBe(specialty);
    }

    [Fact]
    public void SetRoleClearsStartingAbility()
    {
        var character = CharacterFactory.CreateCharacter("Crowley Thornwood");
        character.SetRole(CharacterSpecialty.Explorer);

        character.SetStartingAbility(CharacterAbility.BehindMe);

        character.SetRole(CharacterSpecialty.Criminal);

        character.GetFeature<Character, CharacterStartingAbilityFeature>().Ability.ShouldBeNull();
    }

    public static IEnumerable<object[]> SpecialtyData => Enum.GetValues<CharacterSpecialty>().Select(_ => new object[] { _ });
}