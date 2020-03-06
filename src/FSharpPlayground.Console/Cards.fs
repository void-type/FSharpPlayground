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
