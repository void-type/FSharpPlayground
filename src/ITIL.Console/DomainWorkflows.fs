namespace DomainWorkflows

open VoidCore

module ApprovalWorkflow =

    type State =
        | Pending
        | Requested
        | Approved
        | Rejected
        | Cancelled

    type Command =
        | Request
        | Approve
        | Reject
        | Cancel

    let getNextState (command: Command) (state: State): Result<State> =
        match (state, command) with
        | (Pending, Request) -> Success Requested
        | (Pending, Cancel) -> Success Cancelled

        | (Requested, Approve) -> Success Approved
        | (Requested, Reject) -> Success Rejected
        | (Requested, Cancel) -> Success Cancelled

        | _ ->
            Failure
                [ { Message = sprintf "Invalid workflow transition %A." (state, command)
                    Field = "Command" } ]

module RequestedItemWorkflow =

    type State =
        | Pending
        | AwaitingApproval
        | WorkInProgress
        | Completed
        | Cancelled

    type Command =
        | RequestApproval
        | StartWork
        | Complete
        | Cancel

    let getNextState (command: Command) (state: State): Result<State> =
        match (state, command) with
        | (Pending, RequestApproval) -> Success AwaitingApproval
        | (Pending, Cancel) -> Success Cancelled

        | (AwaitingApproval, StartWork) -> Success WorkInProgress
        | (AwaitingApproval, Cancel) -> Success Cancelled

        | (WorkInProgress, Complete) -> Success Completed
        | (WorkInProgress, Cancel) -> Success Cancelled

        | _ ->
            Failure
                [ { Message = sprintf "Invalid workflow transition %A." (state, command)
                    Field = "Command" } ]

module IncidentWorkflow =

    type State =
        | Unassigned
        | Assigned
        | WorkInProgress
        | Closed

    type Command =
        | Assign
        | StartWork
        | Close

    let getNextState (command: Command) (state: State): Result<State> =
        match (state, command) with
        | (Unassigned, Assign) -> Success Assigned

        | (Assigned, StartWork) -> Success WorkInProgress

        | (WorkInProgress, Close) -> Success Closed

        | _ ->
            Failure
                [ { Message = sprintf "Invalid workflow transition %A." (state, command)
                    Field = "Command" } ]
