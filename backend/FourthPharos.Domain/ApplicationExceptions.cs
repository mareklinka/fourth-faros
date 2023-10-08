using FourthPharos.Domain.Models;

namespace FourthPharos.Domain;

public static class ApplicationExceptions
{
    public static DomainActionException MissingRoleAssignment(CharacterSpecialty specialty) => new(nameof(MissingRoleAssignment), $"The specialty {specialty} has no role assigned");

    public static DomainActionException MissingAbilityAssignment(CharacterSpecialty specialty) => new(nameof(MissingAbilityAssignment), $"The specialty {specialty} has no abilities assigned");
}