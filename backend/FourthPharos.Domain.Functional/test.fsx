#r "nuget: Validus"

#load "Domain.fs"
#load "Circle.fs"
#load "CircleOperations.fs"

open FourthPharos.Domain.Functional
open FourthPharos.Domain.Functional.Circle
open FourthPharos.Domain.Functional.Domain
open Validus
open Validus.Operators
open FourthPharos.Domain.Functional.CircleOperations
open System

let main () =
    validate {
        let! name = createString50 "test"
        and! location = createString200 "home"
        and! illumination = createIllumination 10 // validator is bugged
        and! resources = createResource 3

        let c =
            { Name = name
              Location = location
              Illumination = illumination
              Abilities =
                { StaminaTraining = Unselected
                  ForgedInFire = Unselected
                  NobodyLeftBehind = Unselected
                  Interdisciplinary = Unselected
                  ResourceManagement = Unselected
                  OneLastRun = Unselected }
              Assignment = None
              Gear = List.empty
              Stitch = resources
              Train = resources
              Refresh = resources }

        c |> printfn "Circle %O"

        let! gearName = createString50 "New Gear 1"
        let gear = Gear gearName

        let! withGear = c |> addGear gear
        withGear |> printfn "Circle with gear %O"

        let! started = withGear |> startAssignment DateTime.Now
        started |> printfn "Started assignment %O"

        let! finished = started |> finishAssignment
        finished |> printfn "Finished assignment %O"

        let! removed = finished |> removeGear gear
        removed |> printfn "Removed gear %O"

        let! increased = removed |> addIllumination 31
        increased |> printfn "Increased illumination %O"

        increased |> milestone |> printfn "Milestone %O"
        increased |> rank |> printfn "Rank %O"

        let! consumed =
            increased
            |> consumeResource Stitch
            |> Result.bind (consumeResource Stitch)
            |> Result.bind (consumeResource Stitch)


        consumed |> printfn "Consumed stitch %O"

        let! restored = consumed |> restoreResource Stitch
        restored |> printfn "Restored stitch %O"

        return ()
    }
