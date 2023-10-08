using FourthPharos.Domain.CandelaObscuraCharacter.Features;
using FourthPharos.Domain.CandelaObscuraCharacter.Models;
using FourthPharos.Domain.Features;

namespace FourthPharos.Domain.CandelaObscuraCharacter.Operations;

public static class SetStartingAbilityOperation
{
    public static Character SetStartingAbility(this Character character, CharacterAbility? ability)
    {
        var feature = character.GetFeature<Character, CharacterStartingAbilityFeature>();
        var roleFeature = character.GetFeature<Character, CharacterRoleFeature>();

        if (ability is not null && !roleFeature.GetAvailableAbilities().Contains(ability))
        {
            throw DomainExceptions.CharacterExceptions.InvalidAbility(ability.Code);
        }

        // todo: add check to prevent an ability being added multiple times
        // this requires character ability feature

        return character.UpdateFeature(feature with { Ability = ability });
    }
}