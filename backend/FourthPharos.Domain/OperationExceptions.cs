using FourthPharos.Domain.CandelaObscuraCircle.Models;

namespace FourthPharos.Domain;

public static class DomainExceptions
{
    public static class CircleExceptions
    {
        public static DomainActionException CircleNameTooLong(int length) => new(nameof(CircleNameTooLong), $"The circle name must not exceed {length} characters", length);

        public static DomainActionException CircleLocationTooLong(int length) => new(nameof(CircleLocationTooLong), $"The circle's location must not exceed {length} characters", length);

        public static DomainActionException NotEnoughResource(CircleResource resource) => new(nameof(NotEnoughResource), "The circle doesn't have enough resource to spend", resource);

        public static DomainActionException ResourceFull(CircleResource resource) => new(nameof(ResourceFull), "The circle's resource is at maximum", resource);

        public static DomainActionException AbilityAlreadyExists(string abilityCode) => new(nameof(AbilityAlreadyExists), "The circle already has this ability", abilityCode);

        public static DomainActionException AbilityLimitReached() => new(nameof(AbilityLimitReached), "The circle cannot take a new ability");

        public static DomainActionException NotEnoughStaminaDice() => new(nameof(NotEnoughStaminaDice), "The circle doesn't have enough stamina dice to spend");

        public static DomainActionException StaminaDiceFull() => new(nameof(StaminaDiceFull), "The circle's stamina dice are at maximum");

        public static DomainActionException IlluminationBelowZero() => new(nameof(IlluminationBelowZero), "Illumination cannot be less than 0");

        public static DomainActionException InvalidAbility(string abilityCode) => new(nameof(InvalidAbility), $"Ability {abilityCode} does not exist", abilityCode);

        public static DomainActionException AbilityForRankExists(int rank) => new(nameof(AbilityForRankExists), $"Ability for circle rank {rank} is already taken", rank);

        public static DomainActionException InsufficientRank(int takenAtRank) => new(nameof(InsufficientRank), $"Circle has not reached rank {takenAtRank}", takenAtRank);

        public static DomainActionException AssignmentAlreadyActive() => new(nameof(AssignmentAlreadyActive), $"Circle is already on an assignment");

        public static DomainActionException NoActiveAssignment() => new(nameof(NoActiveAssignment), $"Circle is already on an assignment");

        public static DomainActionException InvalidGear(Guid id) => new(nameof(InvalidGear), $"Gear {id} does not exist", id);
    }

    public static class CircleGearExceptions
    {
        public static DomainActionException GearNameEmpty() => new(nameof(GearNameEmpty), "The gear name must not be empty");

        public static DomainActionException GearNameTooLong(int length) => new(nameof(GearNameTooLong), $"The gear name must not exceed {length} characters", length);
    }

    public static class FeatureExceptions
    {
        public static DomainActionException FeatureNotAttached(string featureName) =>
            new(nameof(FeatureNotAttached), $"The feature {featureName} is not available");

        public static DomainActionException FeatureAlreadyAttached(string featureName) =>
            new(nameof(FeatureAlreadyAttached), $"The feature {featureName} is already attached");
    }

    public static class CharacterExceptions
    {
        public static DomainActionException CharacterNameEmpty() => new(nameof(CharacterNameEmpty), "The character name must not be empty");

        public static DomainActionException CharacterNameTooLong(int length) => new(nameof(CharacterNameTooLong), $"The character name must not exceed {length} characters", length);

        public static DomainActionException BasicInfoTooLong(int length) => new(nameof(BasicInfoTooLong), $"The character name must not exceed {length} characters", length);

        public static DomainActionException ScarDescriptionTooLong(int length) => new(nameof(ScarDescriptionTooLong), $"The scar description must not exceed {length} characters", length);

        public static DomainActionException InsufficientDrive() => new(nameof(InsufficientDrive), "The character doesn't have enough drive");

        public static DomainActionException MaximumDriveReached() => new(nameof(MaximumDriveReached), "The character has maximum allowed drive");

        public static DomainActionException MaximumDriveOutOfRange() => new(nameof(MaximumDriveOutOfRange), "Maximum drive must be between 0 and 9");

        public static DomainActionException ActionRatingOutOfRange() => new(nameof(ActionRatingOutOfRange), "Action rank must be between 0 and 3");

        public static DomainActionException InvalidAction(string actionCode) => new(nameof(InvalidAction), $"Action {actionCode} does not exist", actionCode);

        public static DomainActionException InvalidNote(Guid id) => new(nameof(InvalidNote), $"The note {id} was does not exist", id);

        public static DomainActionException ScarSourceActionSameAsTargetAction(string actionCode) => new(nameof(ScarSourceActionSameAsTargetAction), "A scar's source action must be different from its target action", actionCode);

        public static DomainActionException InvalidScar(Guid id) => new(nameof(InvalidScar), $"Scar {id} does not exist", id);

        public static DomainActionException InsufficientActionRating(string actionCode) => new(nameof(InsufficientActionRating), $"Insufficient rating for action {actionCode}");

        public static DomainActionException ActionRatingFull(string actionCode) => new(nameof(ActionRatingFull), $"Insufficient rating for action {actionCode}");

        public static DomainActionException InsufficientMarks() => new(nameof(InsufficientMarks), "The character doesn't have enough marks");

        public static DomainActionException InvalidIlluminationKey(string keyCode) => new(nameof(InvalidIlluminationKey), $"Illumination key {keyCode} does not exist", keyCode);

        public static DomainActionException IlluminationKeyAlreadyMarked(string keyCode) => new(nameof(IlluminationKeyAlreadyMarked), $"Illumination key {keyCode} is already marked", keyCode);

        public static DomainActionException RelationshipNameTooLong(int length) => new(nameof(RelationshipNameTooLong), $"The relationship name must not exceed {length} characters", length);

        public static DomainActionException RelationshipDescriptionTooLong(int length) => new(nameof(RelationshipDescriptionTooLong), $"The relationship description must not exceed {length} characters", length);

        public static DomainActionException InvalidRelationship(Guid id) => new(nameof(InvalidRelationship), $"Relationship {id} does not exist", id);
    }
}
