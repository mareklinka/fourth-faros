namespace FourthPharos.Domain.Functional

open Circle
open Validus

module CircleOperations =
    let setName: setName = fun n c -> { c with Name = n }

    let setLocation: setLocation = fun l c -> { c with Location = l }

    let addIllumination: addIllumination =
        fun i c ->
            validate {
                let! newIlluminationValue =
                    match c.Illumination with
                    | Illumination orig -> orig + i |> createIllumination

                return { c with Illumination = newIlluminationValue }
            }

    let startAssignment: startAssignment =
        fun dt c ->
            c.Assignment
            |> Check.WithMessage.Option.isNone (fun _ -> "Circle has an active assignment") (nameof (c.Assignment))
            |> Result.map (fun _ -> { c with Assignment = Some(StartDate dt) })

    let finishAssignment: finishAssignment =
        fun c ->
            c.Assignment
            |> Check.WithMessage.Option.isSome (fun _ -> "Circle has no active assignment") (nameof (c.Assignment))
            |> Result.map (fun _ -> { c with Assignment = None })

    let addGear: gearOperation =
        fun g c ->
            let rule c g = c.Gear |> List.contains g |> not

            let validator =
                Validator.create (fun _ -> "Gear already present") (rule c) (nameof (g))

            validate {
                let! _ = g |> validator

                return { c with Gear = g :: c.Gear }
            }

    let removeGear: gearOperation =
        fun g c ->
            let rule c g = c.Gear |> List.contains g
            let validator = Validator.create (fun _ -> "Gear not found") (rule c) (nameof (g))

            validate {
                let! _ = g |> validator
                let index = c.Gear |> List.findIndex (fun i -> i = g)

                return { c with Gear = c.Gear |> List.removeAt index }
            }

    let private modifyResource amount rt c =
        let currentResource =
            match rt with
            | Stitch -> c.Stitch
            | Train -> c.Train
            | Refresh -> c.Refresh

        let resourceValue =
            match currentResource with
            | Resource r -> r

        validate {
            let! newResource = createResource (resourceValue + amount)

            return
                match rt with
                | Stitch -> { c with Stitch = newResource }
                | Train -> { c with Train = newResource }
                | Refresh -> { c with Refresh = newResource }
        }

    let consumeResource: resourceOperation = modifyResource -1
    let restoreResource: resourceOperation = modifyResource 1
