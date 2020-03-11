module Event

open VoidCore

// Domain-level Event
type Request =
    { Input: int }

type Response =
    { Ouptut: int }

let validateRequest (request: Request): Result<Request> =
    let fieldName = nameof request.Input

    let isGreaterThanZero (request: Request): Result<Request> =
        if request.Input > 0 then
            Success request
        else
            Failure
                [ { Message = sprintf "%s not above zero." fieldName
                    Field = fieldName } ]

    let isGreaterThanTwo (request: Request): Result<Request> =
        if request.Input <= 2 then
            Success request
        else
            Failure
                [ { Message = sprintf "%s not above two." fieldName
                    Field = fieldName } ]

    [ isGreaterThanZero; isGreaterThanTwo ]
    |> List.map (fun validator -> validator request)
    |> combineResults
    |> function
    | Success _ -> Success request
    | Failure failures -> Failure failures


let handleEvent (request: Request): Result<Response> =
    if request.Input = 20 then
        Success { Ouptut = request.Input }
    else
        Failure
            [ { Message = "Not found"
                Field = nameof request.Input } ]

let logEvent (result: Result<Response>): Result<Response> = result |> tee (printfn "%A")

let eventPipeline request =
    request
    |> validateRequest
    |> function
    | Success r -> handleEvent r
    | Failure f -> Failure f
    |> logEvent
