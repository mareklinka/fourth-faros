namespace FourthPharos.Domain.Functional

open Character
open Circle

module CharacterOperations =
  let private updateCircle originalCharacter newCharacter =
    let setCircle circle char = { char with Circle = Some circle }

    match newCharacter.Circle with
    | None -> newCharacter
    | Some circle ->
      let index = circle.Characters |> List.findIndex (fun char -> System.Object.ReferenceEquals(char, originalCharacter))
      let otherChars = circle.Characters |> List.removeAt index

      let rec character =
        { Name = newCharacter.Name
          Circle = circleOption }
      and newCircle : Circle =
        { Name = circle.Name
          Location = circle.Location
          Illumination = circle.Illumination
          Abilities = circle.Abilities
          Assignment = circle.Assignment
          Gear = circle.Gear
          Stitch = circle.Stitch
          Train = circle.Train
          Refresh = circle.Refresh
          Characters = charactersWithCircle }
      and charactersWithCircle = otherChars |> List.map (setCircle newCircle) |> List.append [character]
      and circleOption = Some newCircle

      character

  let setName newName character = { character with Name = newName } |> updateCircle character