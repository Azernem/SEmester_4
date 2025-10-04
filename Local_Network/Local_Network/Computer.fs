namespace Computer
open OS

/// Class for computers
type Computer(id: int, os: OS) =
    let mutable infect = false
    let neighbors = ResizeArray<Computer>()  // ← СОЗДАЕМ ОДИН РАЗ в конструкторе
    
    let probability =
        match os with
        | Windows -> 1.0    
        | Linux -> 0.5    
        | MacOS -> 0.7  
    member _.Id = id
    /// type of OS
    member _.OS = os  

    /// probabilty in dependence of OS
    member _.Probability = probability

    /// Isn it infected
    member _.Infected = infect

    /// Neightbors in computer network
    member _.Neighbors = neighbors

    /// Infect with probabilitu one of computers
    member _.InfectComputersWithProbablilty() =
        if not infect then
            let checker = System.Random.Shared.NextDouble()
            if checker <= probability then 
                infect <- true    
    member _.Infect() = infect <- true 
        
