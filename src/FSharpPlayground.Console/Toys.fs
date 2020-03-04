module Toys

  type Person =
      { Name: string
        Email: string
        Id: int }

  let add x y = x + y
  let addOne x = add x 1
  let subtract x y = x - y

  let nameAdder (p: Person) = { p with Name = p.Name + " smith" }

  let glow = "ball"
