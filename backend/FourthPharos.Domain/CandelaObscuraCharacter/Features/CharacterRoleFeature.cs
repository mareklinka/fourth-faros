using FourthPharos.Domain.CandelaObscuraCharacter.Models;
using FourthPharos.Domain.CandelaObscuraCircle.Features;
using FourthPharos.Domain.CandelaObscuraCircle.Models;
using FourthPharos.Domain.Features;
using FourthPharos.Domain.Models;

namespace FourthPharos.Domain.CandelaObscuraCharacter.Features;

public sealed record CharacterRoleFeature(Character Target, CharacterSpecialty Specialty) : FeatureBase<Character>(Target)
{
    public override string Code => "char_role";

    public CharacterRole Role => Specialty switch
    {
        CharacterSpecialty.Journalist or CharacterSpecialty.Magician => CharacterRole.Face,
        CharacterSpecialty.Explorer or CharacterSpecialty.Soldier => CharacterRole.Muscle,
        CharacterSpecialty.Doctor or CharacterSpecialty.Professor => CharacterRole.Scholar,
        CharacterSpecialty.Criminal or CharacterSpecialty.Detective => CharacterRole.Slink,
        CharacterSpecialty.Medium or CharacterSpecialty.Occultist => CharacterRole.Weird,
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
        _ => throw ApplicationExceptions.MissingRoleAssignment(Specialty)
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
    };

    public ICollection<CharacterAbility> GetAvailableAbilities()
    {
        if (Target.Circle?.GetFeature<Circle, CircleAbilitiesFeature>().Abilities.Any(_ => _.Code == CircleAbility.Interdisciplinary.Code) is true)
        {
            return CharacterAbility.Abilities.Values.ToList();
        }

        return Specialty switch
        {
            CharacterSpecialty.Criminal => new[] { CharacterAbility.Scout, CharacterAbility.StreetSmarts, CharacterAbility.Leverage },
            CharacterSpecialty.Detective => new[] { CharacterAbility.Scout },
            CharacterSpecialty.Magician => new[] { CharacterAbility.IKnowAGuy, CharacterAbility.Misdirection, CharacterAbility.Prestige },
            CharacterSpecialty.Journalist => new[] { CharacterAbility.IKnowAGuy },
            CharacterSpecialty.Explorer => new[] { CharacterAbility.BehindMe, CharacterAbility.Tenacious, CharacterAbility.FieldExperience },
            CharacterSpecialty.Soldier => new[] { CharacterAbility.BehindMe },
            CharacterSpecialty.Occultist => new[] { CharacterAbility.LetThemIn, CharacterAbility.Ghostblade, CharacterAbility.ExtendYourSenses },
            CharacterSpecialty.Medium => new[] { CharacterAbility.LetThemIn },
            CharacterSpecialty.Professor => new[] { CharacterAbility.WellRead, CharacterAbility.SteelMind, CharacterAbility.ChemicalConcoction },
            CharacterSpecialty.Doctor => new[] { CharacterAbility.WellRead },
            _ => throw ApplicationExceptions.MissingRoleAssignment(Specialty)
        };
    }

    public override int Version => 1;
}