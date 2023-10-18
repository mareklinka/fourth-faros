namespace FourthPharos.Domain.Functional

module Test =
  // "untying the recursive knot"
  type 'b GenericCircle = { Chars: 'b list }
  type Character = { Name: string; Circle: (Character GenericCircle) option }
  type Circle = Character GenericCircle

  let create name : Circle =
    let rec circle = { Chars = chars }
    and chars = [{ Name = name; Circle = Some circle }]
    circle