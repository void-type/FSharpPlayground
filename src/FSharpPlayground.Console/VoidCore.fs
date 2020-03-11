module VoidCore

// Library code
let tee func arg =
    func arg
    arg

type Failure =
    { Message: string
      Field: string }

type Result<'T> =
    | Success of 'T
    | Failure of Failure list

let mapResult func (result: Result<'T>) =
    match result with
    | Success value -> func value
    | failure -> failure

let combineResults results =
    results
    |> List.choose (fun result ->
        match result with
        | Failure failures -> Some failures
        | _ -> None )
    |> List.collect id
    |> function
    | [] -> Success()
    | failures -> Failure failures
