namespace DomainEvents

open VoidCore
open DomainObjects.Money
open DomainObjects.Cards
open DomainWorkFlow

module GetHand =

    type Request =
        { BuyIn: Money list
          State: State }

    type Response =
        { Hand: Card list
          State: State }

    let validateRequest (request: Request): Result<Request> =
        let fieldName = nameof request.BuyIn

        let buyInValue = getValue request.BuyIn

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
        |> function
        | Success _ -> Success request
        | Failure failures -> Failure failures


    let handleEvent (request: Request): Result<Response> =
        let buyInValue = getValue request.BuyIn

        if buyInValue = 36.41m then
            request.State
            |> getNextState Approve
            |> mapResult (fun (newState: State) ->
                Success
                    { Hand = (getDeck.GetSlice(Some 23, Some 27))
                      State = newState })
        else
            Failure
                [ { Message = "Amount is not correct."
                    Field = nameof request.BuyIn } ]

    let logRequest (request: Request): Request =
        printfn "Requested with %s dollars" ((getValue request.BuyIn).ToString("C"))
        printfn "Requested with %A State" (request.State)
        request

    let logResponse (result: Result<Response>): Result<Response> = result |> tee (printfn "%A")

    let eventPipeline request =
        request
        |> logRequest
        |> validateRequest
        |> function
        | Success r -> handleEvent r
        | Failure f -> Failure f
        |> logResponse
