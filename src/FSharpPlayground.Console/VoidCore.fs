module VoidCore

type Result<'T> =
    | Success of 'T
    | Failure of (string * string) list

type Request =
    { Selection: int }

type Response =
    { Feedback: int }

let validateGreaterThanZero (request: Request): Result<Unit> =
    match request with
    | { Selection = limit } when limit > 0 -> Success()
    | _ -> Failure [ ("Selection not above zero.", nameof request.Selection) ]

let validateLessThanOneHundred (request: Request): Result<Unit> =
    match request with
    | { Selection = limit } when limit > 2 -> Success()
    | _ -> Failure [ ("Selection not above two.", nameof request.Selection) ]


let validation (request: Request) =
    [ validateGreaterThanZero; validateLessThanOneHundred ]
    |> List.map (fun validator -> validator request)
    // TODO: Use a seq.collect here to concat failures.
    |> List.choose (fun result ->
        (match result with
         | Failure l -> Some l
         | _ -> None))

let event (request: Request): Result<Response> =
    match request with
    | { Selection = 20 } -> Success { Feedback = request.Selection }
    | _ -> Failure [ ("Oops", "Not found") ]

let response (request: Request): Result<Response> =
    request
    |> validateGreaterThanZero
    |> function
    | Success() -> validateLessThanOneHundred request
    | Failure(f) -> Failure(f)
    |> function
    | Success() -> event request
    | Failure(f) -> Failure(f)
