using FourthPharos.Domain.CandelaObscuraCharacter.Models;
using FourthPharos.Domain.Features;

namespace FourthPharos.Domain.CandelaObscuraCharacter.Features;

public sealed record CharacterStartingAbilityFeature(Character Target) : FeatureBase<Character>(Target)
{
    public override string Code => "char_starting_ability";

    public override int Version => 1;

    public CharacterAbility? Ability { get; init; }
}