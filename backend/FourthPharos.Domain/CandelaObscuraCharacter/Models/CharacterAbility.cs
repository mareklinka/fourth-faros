namespace FourthPharos.Domain.CandelaObscuraCharacter.Models;

public record CharacterAbility
{
    public static readonly IReadOnlyDictionary<string, CharacterAbility> Abilities = new Dictionary<string, CharacterAbility>()
    {
        { "scout", new("scout") },
        { "street_smarts", new("street_smarts") },
        { "leverage", new("leverage") },
        { "well_read", new("well_read") },
        { "steel_mind", new("steel_mind") },
        { "chemical_concoction", new("chemical_concoction") },
        { "i_know_a_guy", new("i_know_a_guy") },
        { "misdirection", new("misdirection") },
        { "prestige", new("prestige") },
        { "let_them_in", new("let_them_in") },
        { "ghosstblade", new("ghostblade") },
        { "extend_your_senses", new("extend_your_senses") },
        { "behind_me", new("behind_me") },
        { "tenacious", new("tenacious") },
        { "field_experience", new("field_experience") }
    };

    public static CharacterAbility Scout => Abilities["scout"];

    public static CharacterAbility StreetSmarts => Abilities["street_smarts"];

    public static CharacterAbility Leverage => Abilities["leverage"];

    public static CharacterAbility IKnowAGuy => Abilities["i_know_a_guy"];

    public static CharacterAbility Misdirection => Abilities["misdirection"];

    public static CharacterAbility Prestige => Abilities["prestige"];

    public static CharacterAbility BehindMe => Abilities["behind_me"];

    public static CharacterAbility Tenacious => Abilities["tenacious"];

    public static CharacterAbility FieldExperience => Abilities["field_experience"];

    public static CharacterAbility LetThemIn => Abilities["let_them_in"];

    public static CharacterAbility Ghostblade => Abilities["ghostblade"];

    public static CharacterAbility ExtendYourSenses => Abilities["extend_your_senses"];

    public static CharacterAbility WellRead => Abilities["well_read"];

    public static CharacterAbility SteelMind => Abilities["steel_mind"];

    public static CharacterAbility ChemicalConcoction => Abilities["chemical_concoction"];

    private CharacterAbility(string code) => Code = code;

    public string Code { get; }
}