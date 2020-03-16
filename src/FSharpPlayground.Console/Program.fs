open DomainWorkFlow
open DomainEvents

[<EntryPoint>]
let main argv =
    // All Good
    let response =
        { GetHand.BuyIn = {
            Penny = 1
            Nickle = 1
            Dime = 1
            Quarter = 1
            One = 1
            Five = 1
            Ten = 1
            Twenty = 1 }
          GetHand.State = ApprovalRequested }
        |> GetHand.eventPipeline

    // Ammount incorrect
    let response =
        { GetHand.BuyIn = {
            Penny = 1
            Nickle = 0
            Dime = 0
            Quarter = 1
            One = 0
            Five = 0
            Ten = 0
            Twenty = 0 }
          GetHand.State = ApprovalRequested }
        |> GetHand.eventPipeline

    // Invalid request
    let response =
        { GetHand.BuyIn = {
            Penny = 0
            Nickle = 0
            Dime = 0
            Quarter = 0
            One = 0
            Five = 0
            Ten = 0
            Twenty = 10000 }
          GetHand.State = ApprovalRequested }
        |> GetHand.eventPipeline

    // Invalid workflow transition
    let response =
        { GetHand.BuyIn = {
            Penny = 1
            Nickle = 1
            Dime = 1
            Quarter = 1
            One = 1
            Five = 1
            Ten = 1
            Twenty = 1 }
          GetHand.State = Revoked }
        |> GetHand.eventPipeline

    // return an integer exit code
    0
