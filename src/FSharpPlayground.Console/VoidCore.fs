module VoidCore

// Library code
type Failure =
    { Message: string
      Field: string }

type Result<'T> =
    | Success of 'T
    | Failure of Failure list

let tee func arg =
    func arg
    arg

// Create a reusable default pipeline
let domainEventPipeline validator eventHandler postProcessor request =
    request
    |> validator
    |> function
    | [] -> eventHandler request
    | failures -> Failure failures
    |> postProcessor
