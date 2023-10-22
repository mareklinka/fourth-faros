namespace FourthPharos.Domain.Functional

open System
open FourthPharos.Domain.Functional.Domain
open Validus
open Validus.Operators

module GenericCircle =
  type Illumination = internal Illumination of int
  type Rank = internal Rank of int
  type StaminaTrainingDice = internal StaminaDice of int
  type Assignment = StartDate of DateOnly
  type Gear = Gear of String50

  type ResourceType =
    | Stitch
    | Train
    | Refresh

  type AbilityType =
    | StaminaTraining
    | NobodyLeftBehind
    | ForgedInFire
    | Interdisciplinary
    | ResourceManagement
    | OneLastRun

  type Resource = internal Resource of int

  type Milestone =
    | Zero
    | One
    | Two
    | Three

  let (|Illumination|) =
    function
    | Illumination i -> Illumination i

  let (|Rank|) =
    function
    | Rank i -> Rank i

  let (|Resource|) =
    function
    | Resource i -> Resource i

  let (|StaminaDice|) =
    function
    | StaminaDice i -> StaminaDice i

  let createIllumination =
    fun illumination ->
      illumination
      |> (Check.Int.greaterThanOrEqualTo 0 *|* Illumination) (nameof (illumination))

  let createResource =
    fun resource -> resource |> (Check.Int.between 0 3 *|* Resource) (nameof (resource))

  let createStaminaDice =
    fun staminaDice -> staminaDice |> (Check.Int.between 0 3 *|* StaminaDice) (nameof (staminaDice))

  let createRank =
    fun rank -> rank |> (Check.Int.between 0 3 *|* Rank) (nameof (rank))

  type StaminaTraining = StaminaTraining of StaminaTrainingDice
  type NobodyLeftBehind = NobodyLeftBehind
  type ForgedInFire = ForgedInFire
  type Interdisciplinary = Interdisciplinary
  type ResourceManagement = ResourceManagement
  type OneLastRun = OneLastRun

  type AbilitySelection<'T> = { Ability: 'T; TakenAtRank: Rank }

  type AbilityOption<'T> =
    | Selected of AbilitySelection<'T>
    | Unselected

  type Abilities =
    { StaminaTraining: AbilityOption<StaminaTraining>
      NobodyLeftBehind: AbilityOption<NobodyLeftBehind>
      ForgedInFire: AbilityOption<ForgedInFire>
      Interdisciplinary: AbilityOption<Interdisciplinary>
      ResourceManagement: AbilityOption<ResourceManagement>
      OneLastRun: AbilityOption<OneLastRun> }

  type 'b GenericCircle =
    { Name: String50
      Location: String200
      Illumination: Illumination
      Abilities: Abilities
      Assignment: Assignment option
      Gear: Gear list
      Stitch: Resource
      Train: Resource
      Refresh: Resource
      Characters: 'b list }

  let milestone c =
    match c.Illumination with
    | Illumination i when (i % 24) < 7 -> Zero
    | Illumination i when (i % 24) < 14 -> One
    | Illumination i when (i % 24) < 21 -> Two
    | _ -> Three

  let rank c =
    match c.Illumination with
    | Illumination i -> Rank(1 + (i / 24))
