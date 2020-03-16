namespace DomainEvents

open VoidCore
open DomainObjects.Money
open DomainObjects.Cards
open DomainWorkFlow

// Request a buy-in, get approval from the dealer, and get a hand of cards back.
module GetHand =

    type Request =
        { BuyIn: Money
          State: State }

    type Response =
        { Hand: Card list
          State: State }

    let private validateRequest (request: Request): Result<Request> =
        let fieldName = nameof request.BuyIn

        let buyInValue = request.BuyIn.GetValue

        let isGreaterThanZero (request: Request): Result<Request> =
            if buyInValue > 0m then
                Success request
            else
                Failure
                    [ { Message = sprintf "%s should be above zero." fieldName
                        Field = fieldName } ]

        let isLessThanTwoThousand (request: Request): Result<Request> =
            if buyInValue < 2000m then
                Success request
            else
                Failure
                    [ { Message = sprintf "%s should be less than two thousand." fieldName
                        Field = fieldName } ]

        [ isGreaterThanZero; isLessThanTwoThousand ]
        |> List.map (fun validator -> validator request)
        |> combineResults
        |> mapResult (fun () -> Success request)


    let private handleEvent (request: Request): Result<Response> =
        let buyInValue = request.BuyIn.GetValue

        if buyInValue <> 36.41m then
            Failure
                [ { Message = "Amount is not correct."
                    Field = nameof request.BuyIn } ]
        else
            request.State
            |> getNextState Approve
            |> mapResult (fun (newState: State) ->
                Success
                    { Hand = (getDeck.GetSlice(Some 23, Some 27))
                      State = newState })

    let private logRequest (request: Request): Request =
        printfn "Requested with %s dollars" ((request.BuyIn.GetValue).ToString("C"))
        printfn "Requested with %A State" (request.State)
        request

    let private logResponse (result: Result<Response>): Result<Response> =
        printfn "%A" (result)
        result

    let eventPipeline request =
        request
        |> logRequest
        |> validateRequest
        |> mapResult handleEvent
        |> logResponse
