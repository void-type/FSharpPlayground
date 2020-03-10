module Workflow

open VoidCore

type State =
    | NotStarted
    | ApprovalRequested
    | Approved
    | Cancelled
    | Revoked
    | Expired

type Command =
    | Start
    | Approve
    | Reject
    | Cancel
    | Revoke
    | Expire

type Transition = State * Command

let getNextState (command: Command) (state:State) =
    (state, command)
    |> function

    // Not Started
    | (NotStarted, Start) -> Success ApprovalRequested
    | (NotStarted, Cancel) -> Success Cancelled

    // Approval Requested
    | (ApprovalRequested, Approve) -> Success Approved
    | (ApprovalRequested, Reject) -> Success NotStarted
    | (ApprovalRequested, Cancel) -> Success Cancelled

    // Approved
    | (Approved, Revoke) -> Success Revoked
    | (Approved, Expire) -> Success Expired

    | _ ->
        Failure
            [ { Message = sprintf "Invalid workflow transition %A." (state, command)
                Field = "Command" } ]

let flow (command:Command) (result:Result<State>) =
    match result with
    | Success state -> getNextState command state
    | failure -> failure
    |> tee (printfn "%A")
