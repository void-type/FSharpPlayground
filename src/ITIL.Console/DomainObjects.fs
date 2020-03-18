namespace DomainObjects

open DomainWorkflows

// All records should get auditable info

type User =
    { Login: string
      FirstName: string
      LastName: string }

type Approval =
    { State: ApprovalWorkflow.State
      Approver: User }

type Task =
    { Number: int
      ShortDescription: string
      Description: string
      AssignedTo: User
      AssignedOn: System.DateTime }

type RequestedItem =
    { Number: int
      ShortDescription: string
      Description: string
      RequestedFor: User
      State: RequestedItemWorkflow.State
      Approvals: Approval list
      Tasks: Task list }

// All enum-like options should be non-deletable with active flag.
type IncidentClosureType =
    | FixedPermanently
    | FixedWorkaround
    | WontFix
    | NotDuplicated
    | Cancelled

type Incident =
    { Number: int
      ShortDescription: string
      Description: string
      ClosureNotes: string
      ClosureType: IncidentClosureType
      State: IncidentWorkflow.State
      Affected: User }
