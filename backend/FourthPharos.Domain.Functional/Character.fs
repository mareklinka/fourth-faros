namespace FourthPharos.Domain.Functional

open GenericCircle
open Domain

module Character =
  type Character = { Name: String50; Circle: Character GenericCircle option }

  let createCharacter name = { Name = name; Circle = None }