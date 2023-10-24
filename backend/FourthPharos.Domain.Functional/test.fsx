#r "nuget: Validus"

#load "Domain.fs"
#load "GenericCircle.fs"
#load "Character.fs"
#load "Circle.fs"
#load "CircleOperations.fs"
#load "CharacterOperations.fs"

open FourthPharos.Domain.Functional.GenericCircle
open FourthPharos.Domain.Functional.Character
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

    let circle = createCircle name location []

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

    // -------- character tests

    // create two characters
    let! feroName = createString50 "Fero"
    let! jozoName = createString50 "Jozo"

    let fero = createCharacter feroName
    let jozo = createCharacter jozoName

    // add characters to circle
    let circle = createCircle name location [fero; jozo]
    circle |> printCircle

    // verify that both characters reference the same circle
    match (circle.Characters.Head.Circle, circle.Characters.Tail.Head.Circle) with
    | Some c1, Some c2 ->
      (Object.ReferenceEquals(circle, c1) && Object.ReferenceEquals(circle, c2))
      |> printfn "Same circle for Fero and Jozo: %O"
    | _ -> printfn "Nope"

    // update one character's name
    let! newName = createString50 "Ferislav"
    let newFero = circle.Characters.Head |> CharacterOperations.setName newName

    // verify that both characters still reference the same circle
    let circle = newFero.Circle.Value
    circle |> printCircle

    match (circle.Characters.Head.Circle, circle.Characters.Tail.Head.Circle) with
    | Some c1, Some c2 ->
      (Object.ReferenceEquals(circle, c1) && Object.ReferenceEquals(circle, c2))
      |> printfn "Same circle for Fero and Jozo 2: %O"
    | _ -> printfn "Nope"

    // update the circle's name
    let! circle = circle |> takeAbility AbilityType.OneLastRun
    circle |> printCircle

    // verify that both characters still reference the same circle
    match (circle.Characters.Head.Circle, circle.Characters.Tail.Head.Circle) with
    | Some c1, Some c2 ->
      (Object.ReferenceEquals(circle, c1) && Object.ReferenceEquals(circle, c2))
      |> printfn "Same circle for Fero and Jozo 3: %O"
    | _ -> printfn "Nope"

    // test character add/remove
    let circle = createCircle name location []

    let! feroName = createString50 "fero"
    let fero = createCharacter feroName
    let circleWithFero = circle |> addCharacter fero

    circleWithFero |> printCircle
    match (circleWithFero.Characters.Head.Circle) with
    | Some c ->
      Object.ReferenceEquals(circleWithFero, c)
      |> printfn "Same circle after adding fero: %O"
    | _ -> printfn "Nope"

    let! jozoName = createString50 "jozo"
    let jozo = createCharacter jozoName
    let circleWithJozo = circleWithFero |> addCharacter jozo

    circleWithJozo |> printCircle
    match (circleWithJozo.Characters.Head.Circle, circleWithJozo.Characters.Tail.Head.Circle) with
    | Some c1, Some c2 ->
      (Object.ReferenceEquals(circleWithJozo, c1) && Object.ReferenceEquals(circleWithJozo, c2))
      |> printfn "Same circle after adding jozo: %O"
    | _ -> printfn "Nope"

    let circleWithoutJozo = circleWithJozo |> removeCharacter circleWithJozo.Characters.Head
    circleWithoutJozo |> printCircle

    match (circleWithoutJozo.Characters.Head.Circle) with
    | Some c ->
      Object.ReferenceEquals(circleWithoutJozo, c)
      |> printfn "Same circle after removing jozo: %O"
    | _ -> printfn "Nope"

    return ()
  }
