module Guess

open VoidCore

// Domain-level Event
type GuessRequest =
    { Guess: int }

type GuessResponse =
    { Ouptut: int }

let validateGuessRequest (request: GuessRequest): Failure list =

    let guessIsGreaterThanZero =
        if request.Guess <= 0 then
            Some
                { Message = "Guess not above zero."
                  Field = nameof request.Guess }
        else
            None

    let guessIsGreaterThanTwo =
        if request.Guess <= 2 then
            Some
                { Message = "Guess not above two."
                  Field = nameof request.Guess }
        else
            None

    [ guessIsGreaterThanZero; guessIsGreaterThanTwo ] |> List.choose id

let handleGuessEvent (request: GuessRequest): Result<GuessResponse> =
    if request.Guess = 20 then
        Success { Ouptut = request.Guess }
    else
        Failure
            [ { Message = "Not found"
                Field = nameof request.Guess } ]

let logGuessEvent (result: Result<GuessResponse>): Result<GuessResponse> =
    printfn "%A" result
    result
