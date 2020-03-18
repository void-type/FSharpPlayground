module VoidCore

// Perform sideeffects in a pipeline.
let tee func arg =
    func arg
    arg

// Represent a failure that we can display on the UI
type Failure =
    { Message: string
      Field: string }

// Represent the result of a fallible operation.
type Result<'T> =
    | Success of 'T
    | Failure of Failure list

// If the incoming result is success, then run the map function, else return a new failure with incoming failures.
let mapResult (func:'T -> Result<'TNew>) (result: Result<'T>) =
    match result with
    | Success value -> func value
    | Failure failures -> Failure failures

// Select all failurs in multiple results. If any failures, return them, else return success.
let combineResults results =
    results
    |> List.choose (fun result ->
        match result with
        | Failure failures -> Some failures
        | _ -> None)
    |> List.collect id
    |> function
    | [] -> Success()
    | failures -> Failure failures
