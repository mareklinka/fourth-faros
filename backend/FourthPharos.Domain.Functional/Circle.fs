﻿namespace FourthPharos.Domain.Functional

open FourthPharos.Domain.Functional.Domain
open System
open Validus
open Validus.Operators

module Circle =
    type Illumination = private Illumination of int

    let (|Illumination|) =
        function
        | Illumination.Illumination i -> Illumination i

    let createIllumination =
        fun i ->
            i
            |> (Check.Int.greaterThanOrEqualTo 0 *|* Illumination) (nameof (i))

    type Rank = private Rank of int

    let (|Rank|) =
        function
        | Rank.Rank i -> Rank i

    type Milestone =
        | Zero
        | One
        | Two
        | Three

    type StaminaTrainingDice = Dice of int

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

    type Assignment = StartDate of DateTime
    type Gear = Gear of String50

    type ResourceType = Stitch | Train | Refresh
    type Resource = private Resource of int

    let (|Resource|) =
        function
        | Resource.Resource i -> Resource i

    let createResource =
        fun i ->
            i
            |> (Check.Int.between 0 3 *|* Resource) (nameof (i))

    type Circle =
        { Name: String50
          Location: String200
          Illumination: Illumination
          Abilities: Abilities
          Assignment: Assignment option
          Gear: Gear list
          Stitch: Resource
          Refresh: Resource
          Train: Resource }

    type setName = String50 -> Circle -> Circle
    type setLocation = String200 -> Circle -> Circle
    type milestone = Circle -> Milestone
    type addIllumination = int -> Circle -> Result<Circle, ValidationErrors>
    type startAssignment = DateTime -> Circle -> Result<Circle, ValidationErrors>
    type finishAssignment = Circle -> Result<Circle, ValidationErrors>
    type addGear = Gear -> Circle -> Result<Circle, ValidationErrors>
    type removeGear = Gear -> Circle -> Result<Circle, ValidationErrors>
    type resourceOperation = ResourceType -> Circle -> Result<Circle, ValidationErrors>

    let milestone c =
        match c.Illumination with
        | Illumination i when (i % 24) < 7 -> Zero
        | Illumination i when (i % 24) < 14 -> One
        | Illumination i when (i % 24) < 21 -> Two
        | _ -> Three

    let rank c =
        match c.Illumination with
        | Illumination i -> Rank(1 + (i / 24))
