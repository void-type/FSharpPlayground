module Money

type Money =
    { Penny: int
      Nickle: int
      Dime: int
      Quarter: int
      One: int
      Five: int
      Ten: int
      Twenty: int }

let calculateMoney x =
    decimal x.Penny * 0.01m + decimal x.Nickle * 0.05m + decimal x.Dime * 0.10m + decimal x.Quarter * 0.25m
    + decimal x.One * 1.00m + decimal x.Five * 5.00m + decimal x.Ten * 10.00m + decimal x.Twenty * 20.00m
