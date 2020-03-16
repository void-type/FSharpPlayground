module DomainWorkFlow

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

let getNextState (command: Command) (state: State) =
    match (state, command) with
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

    // Invalid transition
    | _ ->
        Failure
            [ { Message = sprintf "Invalid workflow transition %A." (state, command)
                Field = "Command" } ]
