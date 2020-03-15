namespace DomainObjects

module Money =

    type Money =
        | Penny of int
        | Nickle of int
        | Dime of int
        | Quarter of int
        | One of int
        | Five of int
        | Ten of int
        | Twenty of int

    let getValue (money: Money list): decimal =
        let getSingleValue (money: Money) =
            match money with
            | Penny i -> 0.01m * decimal i
            | Nickle i -> 0.05m * decimal i
            | Dime i -> 0.1m * decimal i
            | Quarter i -> 0.25m * decimal i
            | One i -> 1m * decimal i
            | Five i -> 5m * decimal i
            | Ten i -> 10m * decimal i
            | Twenty i -> 20m * decimal i

        money
        |> List.fold (fun s money -> s + getSingleValue money) 0m


module Cards =

    type Suit =
        | Hearts
        | Clubs
        | Diamonds
        | Spades

    type Rank =
        | Value of int
        | Jack
        | Queen
        | King
        | Ace

        static member GetAll() =
            [ for i in 2 .. 10 do
                yield Value i

              yield Jack
              yield Queen
              yield King
              yield Ace ]

    type Card =
        { Suit: Suit
          Rank: Rank }

    let getDeck =
        [ for suit in [ Hearts; Diamonds; Clubs; Spades ] do
            for rank in Rank.GetAll() do
                yield { Suit = suit
                        Rank = rank } ]
