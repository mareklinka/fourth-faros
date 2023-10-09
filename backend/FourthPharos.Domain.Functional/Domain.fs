namespace FourthPharos.Domain.Functional

open Validus

module Domain =
    type String50 = private String50 of string
    type String200 = private String200 of string

    let (|String50|) =
        function
        | String50 s -> String50 s

    let (|String200|) =
        function
        | String200 s -> String200 s

    let createString50 s =
        validate {
            let! validated = s |> Check.String.lessThanOrEqualToLen 50 "s"
            return String50 validated
        }

    let createString200 s =
        validate {
            let! validated = s |> Check.String.lessThanOrEqualToLen 200 "s"
            return String200 validated
        }
