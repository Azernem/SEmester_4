namespace Computer
open OS

type Computer(id: int, os: OS) =
    let mutable infected = false
    member _.Id = id
    member _.OS = os
    member _.Infected = infected
    member _.Neighbors = ResizeArray<Computer>()
    member _.Infect() = infected <- true
