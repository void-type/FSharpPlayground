module Money

type Money =
    | Penny of int
    | Nickle of int
    | Dime of int
    | Quarter of int
    | One of int
    | Five of int
    | Ten of int
    | Twenty of int

let getValue (money: Money): decimal =
    match money with
    | Penny i -> 0.01m * decimal i
    | Nickle i -> 0.05m * decimal i
    | Dime i -> 0.1m * decimal i
    | Quarter i -> 0.25m * decimal i
    | One i -> 1m * decimal i
    | Five i -> 5m * decimal i
    | Ten i -> 10m * decimal i
    | Twenty i -> 20m * decimal i
