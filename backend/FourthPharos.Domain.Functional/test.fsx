#r "nuget: Validus"

#load "Domain.fs"
#load "Circle.fs"
#load "CircleOperations.fs"

open FourthPharos.Domain.Functional.Circle
open FourthPharos.Domain.Functional.CircleOperations
open FourthPharos.Domain.Functional.Domain
open System
open Validus

let print c = c |> printfn "Circle %O"

let (>=>) f1 f2 arg =
  match f1 arg with
  | Ok data -> f2 data
  | Error e -> Error e

let main () =
  validate {
    let! name = createString50 "test"
    and! location = createString200 "home"
    let! gearName = createString50 "New Gear 1"
    let gear = Gear gearName

    let circle = createCircle name location

    circle |> print

    let! newCircle =
      circle
      |> (takeAbility AbilityType.StaminaTraining
          >=> (addGear gear)
          >=> (startAssignment (DateOnly.FromDateTime(DateTime.Now)))
          >=> (finishAssignment)
          >=> (removeGear gear)
          >=> (addIllumination 31)
          >=> (consumeResource Stitch)
          >=> (consumeResource Stitch)
          >=> (consumeResource Stitch)
          >=> (restoreResource Stitch)
          >=> (consumeStaminaDie)
          >=> (consumeStaminaDie)
          >=> (recoverStaminaDie)
          >=> (takeAbility AbilityType.OneLastRun))

    newCircle |> print

    return ()
  }
