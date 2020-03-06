module Selection

open VoidCore

// Domain-level Event
type SelectionRequest =
    { Selection: int }

type SelectionResponse =
    { Feedback: int }

let validateSelectionRequest (request: SelectionRequest): Failure list =

    let selectionIsGreaterThanZero =
        if request.Selection <= 0 then
            Some
                { Message = "Selection not above zero."
                  Field = nameof request.Selection }
        else
            None

    let selectionIsGreaterThanTwo =
        if request.Selection <= 2 then
            Some
                { Message = "Selection not above two."
                  Field = nameof request.Selection }
        else
            None

    [ selectionIsGreaterThanZero; selectionIsGreaterThanTwo ] |> List.choose id

let handleSelectionEvent (request: SelectionRequest): Result<SelectionResponse> =
    if request.Selection = 20 then
        Success { Feedback = request.Selection }
    else
        Failure
            [ { Message = "Not found"
                Field = nameof request.Selection } ]

let logSelectionEvent (result: Result<SelectionResponse>): Result<SelectionResponse> =
    printfn "%A" result
    result
