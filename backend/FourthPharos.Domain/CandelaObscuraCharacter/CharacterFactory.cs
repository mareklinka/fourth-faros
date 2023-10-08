using FourthPharos.Domain.CandelaObscuraCharacter.Features;
using FourthPharos.Domain.CandelaObscuraCharacter.Models;
using FourthPharos.Domain.Features;
using FourthPharos.Domain.Models;

namespace FourthPharos.Domain.CandelaObscuraCharacter;

public static class CharacterFactory
{
    public static Character CreateCharacter(string name)
    {
        CharacterValidators.Name(name);

        return new Character()
            .AddFeature(t => new CharacterBasicInfoFeature(t, name))
            .AddFeature(t => new CharacterRoleFeature(t, CharacterSpecialty.Journalist))
            .AddFeature(t => new CharacterNotesFeature(t))
            .AddFeature(t => new CharacterNerveFeature(t))
            .AddFeature(t => new CharacterCunningFeature(t))
            .AddFeature(t => new CharacterIntuitionFeature(t))
            .AddFeature(t => new CharacterScarsFeature(t))
            .AddFeature(t => new CharacterBodyMarksFeature(t))
            .AddFeature(t => new CharacterBrainMarksFeature(t))
            .AddFeature(t => new CharacterBleedMarksFeature(t))
            .AddFeature(t => new CharacterIlluminationKeysFeature(t))
            .AddFeature(t => new CharacterRelationshipFeature(t))
            .AddFeature(t => new CharacterStartingAbilityFeature(t));
    }
}
