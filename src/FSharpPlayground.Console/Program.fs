open VoidCore
open Selection
open Guess
open Money

[<EntryPoint>]
let main argv =
    // Cards demo
    let deck = Cards.getDeck

    for card in deck do
        printfn "%s %s" (card.Rank.ToString()) (card.Suit.ToString())

    printfn "%d cards" deck.Length

    // Money demo
    let monies = [
        Penny 1
        Nickle 1
        Dime 1
        Quarter 1
        One 1
        Five 1
        Ten 1
        Twenty 1
    ]

    let amount =
        monies
        |> List.fold (fun s money -> s + getValue money) 0m

    printfn "%A" monies
    printfn "%s dollars" (amount.ToString("C"))

    // Event pipeline demo
    // Use partial application of the default pipline (don't apply the request yet) to create a reusable custom pipeline.
    let selectionEventPipeline = domainEventPipeline validateSelectionRequest handleSelectionEvent logSelectionEvent
    let guessEventPipeline = domainEventPipeline validateGuessRequest handleGuessEvent logGuessEvent

    let resultsSelection = [
        selectionEventPipeline {Selection = 1}
        selectionEventPipeline {Selection = -1}
        selectionEventPipeline {Selection = 20}
        selectionEventPipeline {Selection = 30}
    ]

    let resultsGuess = [
        guessEventPipeline {Guess = 1}
        guessEventPipeline {Guess = -1}
        guessEventPipeline {Guess = 20}
        guessEventPipeline {Guess = 30}
    ]

    0 // return an integer exit code
