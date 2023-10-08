namespace FourthPharos.Domain.Functional

open Circle

module CircleOperations =
    let setName: setName = fun n c -> { c with Name = n }

    let setLocation: setLocation = fun l c -> { c with Location = l }

    let addIllumination: addIllumination =
        fun i c ->
            let newIlluminationValue =
                match c.Illumination with
                | Illumination orig -> orig + i

            let newIllumination = createIllumination newIlluminationValue

            match newIllumination with
            | Some i -> Some({ c with Illumination = i })
            | None -> None

    let startAssignment: startAssignment =
        fun dt c ->
            match c.Assignment with
            | None -> Some({ c with Assignment = Some(StartDate dt) })
            | _ -> None

    let finishAssignment: finishAssignment =
        fun c ->
            match c.Assignment with
            | None -> None
            | _ -> Some({ c with Assignment = None })
