namespace DomainObjects

module Money =

    // Money is a record of quantities of denominations
    type Money =
        { Penny: int
          Nickle: int
          Dime: int
          Quarter: int
          One: int
          Five: int
          Ten: int
          Twenty: int }

        member this.GetValue =
            [ yield decimal this.Penny * 0.01m
              yield decimal this.Nickle * 0.05m
              yield decimal this.Dime * 0.1m
              yield decimal this.Quarter * 0.25m
              yield decimal this.One * 1m
              yield decimal this.Five * 5m
              yield decimal this.Ten * 10m
              yield decimal this.Twenty * 20m ]
            |> List.sum

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
