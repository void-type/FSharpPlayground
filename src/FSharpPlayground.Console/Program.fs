// Learn more about F# at http://fsharp.org

open VoidCore

[<EntryPoint>]
let main argv =
    // let result =
    //     4
    //     |> addOne
    //     |> add 3
    //     |> subtract 2

    // printfn "%d" result

    // let person1 = {
    //     Name = "jeff"
    //     Email = "@gmail.com"
    //     Id = 2
    // }

    // let person2 =
    //     person1
    //     |> nameAdder

    // printfn "%s" person2.Name

    // printfn "Hello World from F#!"
    // printfn "%s" glow

    // let count = Cards.fullDeck.Length

    // for card in Cards.fullDeck do
    //     printfn "%s %s" (card.Rank.ToString()) (card.Suit.ToString())

    // printfn "%d cards" count

    // let money = {
    //     Penny = 1
    //     Nickle = 0
    //     Dime = 2
    //     Quarter = 0
    //     One = 1
    //     Five = 1
    //     Ten = 0
    //     Twenty = 1
    // }

    // let amount = calculateMoney money

    // printfn "%A" money

    // printfn "%s dollars" (amount.ToString("C"))

    let myRequest1 = {
        Selection = 2
    }

    let myResponse1 =
        myRequest1
        |> response

    printfn "%A" myResponse1

    let myRequest20 = {
        Selection = 20
    }

    let myResponse20 =
        myRequest20
        |> response

    printfn "%A" myResponse20

    0 // return an integer exit code
