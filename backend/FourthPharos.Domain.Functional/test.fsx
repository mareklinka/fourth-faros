#r "nuget: Validus"

#load "Domain.fs"
#load "Circle.fs"
#load "CircleOperations.fs"
#load "Test.fs"

open FourthPharos.Domain.Functional.Circle
open FourthPharos.Domain.Functional.CircleOperations
open FourthPharos.Domain.Functional.Domain
open System
open Validus
open FourthPharos.Domain.Functional

let printCircle = printfn "Circle %O"

let (>=>) f1 f2 arg =
  match f1 arg with
  | Ok data -> f2 data
  | Error e -> Error e

let (>>=) arg f1 =
  match arg with
  | Ok data -> f1 data
  | Error e -> Error e

let main () =
  validate {
    let! name = createString50 "test"
    and! location = createString200 "home"
    and! gearName = createString50 "New Gear 1"
    let gear = Gear gearName

    let circle = createCircle name location

    circle |> printCircle

    let today = DateOnly.FromDateTime(DateTime.Now)

    let! newCircle1 =
      circle
      |> takeAbility AbilityType.StaminaTraining
      >>= addGear gear
      >>= startAssignment today
      >>= finishAssignment
      >>= removeGear gear
      >>= addIllumination 31
      >>= consumeResource Stitch
      >>= consumeResource Stitch
      >>= consumeResource Stitch
      >>= recoverResource Stitch
      >>= consumeStaminaDie
      >>= consumeStaminaDie
      >>= recoverStaminaDie
      >>= takeAbility AbilityType.OneLastRun

    newCircle1 |> printCircle

    let! newCircle2 =
      (takeAbility AbilityType.StaminaTraining
        >=> addGear gear
        >=> startAssignment today
        >=> finishAssignment
        >=> removeGear gear
        >=> addIllumination 31
        >=> consumeResource Stitch
        >=> consumeResource Stitch
        >=> consumeResource Stitch
        >=> recoverResource Stitch
        >=> consumeStaminaDie
        >=> consumeStaminaDie
        >=> recoverStaminaDie
        >=> takeAbility AbilityType.OneLastRun) circle

    newCircle2 |> printCircle

    let fero = Test.create "Recursive circle"
    fero |> printfn "%O"

    match fero.Chars.Head.Circle with
    | None -> ()
    | Some c ->
      c |> printfn "%O"
      Object.ReferenceEquals (fero, c) |> printfn "Are the two reference-equal? %O"

    return ()
  }
