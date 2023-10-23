namespace FourthPharos.Domain.Functional

open Circle
open Character
open GenericCircle
open Validus

module CircleOperations =
  let private updateCharacters (c: Circle) =
    let setCircle circle char = { char with Circle = Some circle }
    let rec circle =
      { Name = c.Name
        Location = c.Location
        Illumination = c.Illumination
        Abilities = c.Abilities
        Assignment = c.Assignment
        Gear = c.Gear
        Stitch = c.Stitch
        Train = c.Train
        Refresh = c.Refresh
        Characters = charactersWithCircle }
    and charactersWithCircle = c.Characters |> List.map (setCircle circle)
    circle

  let setName : setName = fun n c -> { c with Name = n } |> updateCharacters

  let setLocation: setLocation = fun l c -> { c with Location = l } |> updateCharacters

  let addIllumination: addIllumination =
    fun illumination circle ->
      validate {
        let! newIlluminationValue =
          match circle.Illumination with
          | Illumination orig -> orig + illumination |> createIllumination

        return
          { circle with
              Illumination = newIlluminationValue } |> updateCharacters
      }

  let startAssignment: startAssignment =
    fun startDate circle ->
      circle.Assignment
      |> Check.WithMessage.Option.isNone (fun _ -> "Circle has an active assignment") (nameof (circle.Assignment))
      |> Result.map (fun _ ->
        { circle with
            Assignment = Some(StartDate startDate) } |> updateCharacters)

  let finishAssignment: finishAssignment =
    fun circle ->
      circle.Assignment
      |> Check.WithMessage.Option.isSome (fun _ -> "Circle has no active assignment") (nameof (circle.Assignment))
      |> Result.map (fun _ -> { circle with Assignment = None } |> updateCharacters)

  let addGear: gearOperation =
    fun gear circle ->
      let rule c g = c.Gear |> List.contains g |> not

      let validator =
        Validator.create (fun _ -> "Gear already present") (rule circle) (nameof (gear))

      validate {
        let! _ = gear |> validator

        return
          { circle with
              Gear = gear :: circle.Gear } |> updateCharacters
      }

  let removeGear: gearOperation =
    fun gear circle ->
      let rule c g = c.Gear |> List.contains g

      let validator =
        Validator.create (fun _ -> "Gear not found") (rule circle) (nameof (gear))

      validate {
        let! _ = gear |> validator
        let index = circle.Gear |> List.findIndex (fun i -> i = gear)

        return
          { circle with
              Gear = circle.Gear |> List.removeAt index } |> updateCharacters
      }

  let private modifyResource amount resource circle =
    let currentResource =
      match resource with
      | Stitch -> circle.Stitch
      | Train -> circle.Train
      | Refresh -> circle.Refresh

    let resourceValue =
      match currentResource with
      | Resource r -> r

    validate {
      let! newResource = createResource (resourceValue + amount)

      return
        match resource with
        | Stitch -> { circle with Stitch = newResource }
        | Train -> { circle with Train = newResource }
        | Refresh -> { circle with Refresh = newResource }
        |> updateCharacters
    }

  let consumeResource: resourceOperation = modifyResource -1
  let recoverResource: resourceOperation = modifyResource 1

  let private modifyStamindDice amount circle =
    validate {
      let! newStamina =
        match circle.Abilities.StaminaTraining with
        | Selected({ Ability = StaminaTraining(StaminaDice d)
                     TakenAtRank = r }) ->
          validate {
            let! newStaminaDice = createStaminaDice (d + amount)

            return
              Selected(
                { Ability = StaminaTraining(newStaminaDice)
                  TakenAtRank = r }
              )

          }
        | _ ->
          Error(
            ValidationErrors.create
              (nameof (StaminaTraining))
              [ "Circle doesn't have the Stamina Training ability selected" ]
          )

      return
        { circle with
            Abilities =
              { circle.Abilities with
                  StaminaTraining = newStamina } } |> updateCharacters
    }

  let consumeStaminaDie: staminaDiceOperation = modifyStamindDice -1
  let recoverStaminaDie: staminaDiceOperation = modifyStamindDice 1

  let private setAbility ability factory circle : Result<Circle, ValidationErrors> =
    let rule ability = ability = Unselected

    validate {
      let! _ =
        ability
        |> Validator.create (fun _ -> "Ability is already taken") rule (nameof (ability))

      return
        { circle with
            Abilities = circle |> factory } |> updateCharacters
    }

  let private unsetAbility ability factory circle : Result<Circle, ValidationErrors> =
    let rule ability = ability <> Unselected

    validate {
      let! _ =
        ability
        |> Validator.create (fun _ -> "Ability is not taken") rule (nameof (ability))

      return
        { circle with
            Abilities = circle |> factory } |> updateCharacters
    }

  let takeAbility: abilityOperation =
    fun ability circle ->
      validate {
        let! setOperation =
          match ability with
          | AbilityType.StaminaTraining ->
            validate {
              let! staminaDice = createStaminaDice 3

              return
                setAbility circle.Abilities.StaminaTraining (fun c ->
                  { c.Abilities with
                      StaminaTraining =
                        Selected(
                          { Ability = StaminaTraining(staminaDice)
                            TakenAtRank = c |> rank }
                        ) })
            }
          | AbilityType.NobodyLeftBehind ->
            validate {
              return
                setAbility circle.Abilities.NobodyLeftBehind (fun c ->
                  { c.Abilities with
                      NobodyLeftBehind =
                        Selected(
                          { Ability = NobodyLeftBehind
                            TakenAtRank = c |> rank }
                        ) })
            }
          | AbilityType.ForgedInFire ->
            validate {
              return
                setAbility circle.Abilities.ForgedInFire (fun c ->
                  { c.Abilities with
                      ForgedInFire =
                        Selected(
                          { Ability = ForgedInFire
                            TakenAtRank = c |> rank }
                        ) })
            }
          | AbilityType.Interdisciplinary ->
            validate {
              return
                setAbility circle.Abilities.Interdisciplinary (fun c ->
                  { c.Abilities with
                      Interdisciplinary =
                        Selected(
                          { Ability = Interdisciplinary
                            TakenAtRank = c |> rank }
                        ) })
            }
          | AbilityType.ResourceManagement ->
            validate {
              return
                setAbility circle.Abilities.ResourceManagement (fun c ->
                  { c.Abilities with
                      ResourceManagement =
                        Selected(
                          { Ability = ResourceManagement
                            TakenAtRank = c |> rank }
                        ) })
            }
          | AbilityType.OneLastRun ->
            validate {
              return
                setAbility circle.Abilities.OneLastRun (fun c ->
                  { c.Abilities with
                      OneLastRun =
                        Selected(
                          { Ability = OneLastRun
                            TakenAtRank = c |> rank }
                        ) })
            }

        return! circle |> setOperation
      }

  let dropAbility: abilityOperation =
    fun ability circle ->
      validate {
        let! unsetOperation =
          match ability with
          | AbilityType.StaminaTraining ->
            validate {
              return
                unsetAbility circle.Abilities.StaminaTraining (fun c ->
                  { c.Abilities with
                      StaminaTraining = Unselected })
            }
          | AbilityType.NobodyLeftBehind ->
            validate {
              return
                unsetAbility circle.Abilities.NobodyLeftBehind (fun c ->
                  { c.Abilities with
                      NobodyLeftBehind = Unselected })
            }
          | AbilityType.ForgedInFire ->
            validate {
              return
                setAbility circle.Abilities.ForgedInFire (fun c ->
                  { c.Abilities with
                      ForgedInFire = Unselected })
            }
          | AbilityType.Interdisciplinary ->
            validate {
              return
                setAbility circle.Abilities.Interdisciplinary (fun c ->
                  { c.Abilities with
                      Interdisciplinary = Unselected })
            }
          | AbilityType.ResourceManagement ->
            validate {
              return
                setAbility circle.Abilities.ResourceManagement (fun c ->
                  { c.Abilities with
                      ResourceManagement = Unselected })
            }
          | AbilityType.OneLastRun ->
            validate {
              return
                setAbility circle.Abilities.OneLastRun (fun c ->
                  { c.Abilities with
                      OneLastRun = Unselected })
            }

        return! circle |> unsetOperation
      }
