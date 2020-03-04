module Cards

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

    static member GetAllRanks() =
        [ for i in 2 .. 10 do
            yield Value i
          yield Jack
          yield Queen
          yield King
          yield Ace ]

type Card =
    { Suit: Suit
      Rank: Rank }

let fullDeck =
    [ for suit in [ Hearts; Diamonds; Clubs; Spades ] do
        for rank in Rank.GetAllRanks() do
            yield { Suit = suit
                    Rank = rank } ]
