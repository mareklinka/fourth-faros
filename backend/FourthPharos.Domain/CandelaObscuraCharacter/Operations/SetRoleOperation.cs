using FourthPharos.Domain.CandelaObscuraCharacter.Features;
using FourthPharos.Domain.CandelaObscuraCharacter.Models;
using FourthPharos.Domain.Features;
using FourthPharos.Domain.Models;

namespace FourthPharos.Domain.CandelaObscuraCharacter.Operations;

public static class SetRoleOperation
{
    public static Character SetRole(this Character character, CharacterSpecialty specialty)
    {
        var feature = character.GetFeature<Character, CharacterRoleFeature>();

        if (specialty != feature.Specialty)
        {
            character.SetStartingAbility(null);
        }

        return character.UpdateFeature(feature with { Specialty = specialty });
    }
}
