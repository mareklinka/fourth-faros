namespace FourthPharos.Domain.Functional

open FourthPharos.Domain.Functional.Domain

module Circle =
    open System
    type Illumination = private Illumination of int

    let (|Illumination|) =
        function
        | Illumination.Illumination i -> Illumination i

    let createIllumination =
        fun i ->
            match i with
            | _ when i < 0 -> None
            | _ -> Some(Illumination i)

    type Rank = private Rank of int

    let (|Rank|) =
        function
        | Rank.Rank i -> Rank i

    type Milestone =
        | Zero
        | One
        | Two
        | Three

    type StaminaTrainingDice = Stamina of int

    type StaminaTraining = StaminaTraining of StaminaTrainingDice
    type NobodyLeftBehind = NobodyLeftBehind
    type ForgedInFire = ForgedInFire
    type Interdisciplinary = Interdisciplinary
    type ResourceManagement = ResourceManagement
    type OneLastRun = OneLastRun

    type AbilitySelection<'T> = {
        Ability: 'T;
        TakenAtRank: Rank
    }

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

    type Assignment = StartDate of DateTime
    type Gear = Gear of String50

    type Circle =
        { Name: String50
          Location: String200
          Illumination: Illumination
          Abilities: Abilities
          Assignment: Assignment option
          Gear: Gear list }

    type setName = String50 -> Circle -> Circle
    type setLocation = String200 -> Circle -> Circle
    type milestone = Circle -> Milestone
    type addIllumination = int -> Circle -> Circle option
    type startAssignment = DateTime -> Circle -> Circle option
    type finishAssignment = Circle -> Circle option

    let milestone c =
        match c.Illumination with
        | Illumination i when (i % 24) < 7 -> Zero
        | Illumination i when (i % 24) < 14 -> One
        | Illumination i when (i % 24) < 21 -> Two
        | _ -> Three

    let rank c =
        match c.Illumination with
        | Illumination i -> Rank (1 + (i / 24))

