open VoidCore
open DomainWorkFlow
open DomainEvents.GetHand
open DomainObjects.Money

[<EntryPoint>]
let main argv =
    // All Good
    let response =
        { BuyIn = [
            Penny 1
            Nickle 1
            Dime 1
            Quarter 1
            One 1
            Five 1
            Ten 1
            Twenty 1 ]

          State = ApprovalRequested }
        |> eventPipeline

    // Ammount incorrect
    let response =
        { BuyIn = [
            Penny 1
            Quarter 1 ]

          State = ApprovalRequested }
        |> eventPipeline

    // Invalid request
    let response =
        { BuyIn = [
            Twenty 10000 ]

          State = ApprovalRequested }
        |> eventPipeline

    // Invalid workflow transition
    let response =
        { BuyIn = [
            Penny 1
            Nickle 1
            Dime 1
            Quarter 1
            One 1
            Five 1
            Ten 1
            Twenty 1 ]

          State = Revoked }
        |> eventPipeline

    // return an integer exit code
    0
