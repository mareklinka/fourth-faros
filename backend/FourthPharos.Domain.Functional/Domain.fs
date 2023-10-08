namespace FourthPharos.Domain.Functional

module Domain =
    type String50 = private String50 of string
    type String200 = private String200 of string

    let (|String50|) = function
        | String50 s -> String50 s

    let (|String200|) = function
        | String200 s -> String200 s

    let createString50 s =
        match (s |> String.length) with
        | l when l <= 50 -> Some (String50 s)
        | _ -> None

    let createString200 s =
        match (s |> String.length) with
        | l when l <= 200 -> Some (String50 s)
        | _ -> None