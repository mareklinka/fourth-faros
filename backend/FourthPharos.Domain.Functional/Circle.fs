namespace FourthPharos.Domain.Functional

open System
open Character
open FourthPharos.Domain.Functional.Domain
open GenericCircle
open Validus

module Circle =
  type Circle = Character GenericCircle

  type setName = String50 -> Circle -> Circle
  type setLocation = String200 -> Circle -> Circle
  type milestone = Circle -> Milestone
  type addIllumination = int -> Circle -> Result<Circle, ValidationErrors>
  type startAssignment = DateOnly -> Circle -> Result<Circle, ValidationErrors>
  type finishAssignment = Circle -> Result<Circle, ValidationErrors>
  type gearOperation = Gear -> Circle -> Result<Circle, ValidationErrors>
  type resourceOperation = ResourceType -> Circle -> Result<Circle, ValidationErrors>
  type staminaDiceOperation = Circle -> Result<Circle, ValidationErrors>
  type abilityOperation = AbilityType -> Circle -> Result<Circle, ValidationErrors>

  let createCircle name location characters : Circle =
    let setCircle circle char = { char with Circle = Some circle }
    let rec circle =
      { Name = name
        Location = location
        Illumination = Illumination 0
        Abilities =
          { StaminaTraining = Unselected
            ForgedInFire = Unselected
            NobodyLeftBehind = Unselected
            Interdisciplinary = Unselected
            ResourceManagement = Unselected
            OneLastRun = Unselected }
        Assignment = None
        Gear = List.empty
        Stitch = Resource 3
        Train = Resource 3
        Refresh = Resource 3
        Characters = charactersWithCircle }
    and charactersWithCircle = characters |> List.map (setCircle circle)

    circle
