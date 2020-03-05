module VoidCore

type Failure =
    { Message: string
      Field: string }

type Result<'T> =
    | Success of 'T
    | Failure of Failure list

type Request =
    { Selection: int }

type Response =
    { Feedback: int }

let validateRequest (request: Request): Failure list =

    let validateRequestIsGreaterThanZero =
        if request.Selection <= 0 then
            Some
                { Message = "Selection not above zero."
                  Field = nameof request.Selection }
        else
            None

    let validateRequestIsGreaterThanTwo =
        if request.Selection <= 2 then
            Some
                { Message = "Selection not above two."
                  Field = nameof request.Selection }
        else
            None

    [ validateRequestIsGreaterThanZero; validateRequestIsGreaterThanTwo ] |> List.choose id

let performEvent (request: Request): Result<Response> =
    if request.Selection = 20 then
        Success { Feedback = request.Selection }
    else
        Failure
            [ { Message = "Not found"
                Field = nameof request.Selection } ]

let eventPipeline (request: Request): Result<Response> =
    request
    |> validateRequest
    |> function
    | [] -> performEvent request
    | failures -> Failure failures
