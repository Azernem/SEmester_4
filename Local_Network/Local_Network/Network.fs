namespace Network
open Computer

type Network(computers: Computer list, adjacencyMatrix: int[][]) =
    do
        for i in 0..adjacencyMatrix.Length-1 do
            for j in 0..adjacencyMatrix[i].Length-1 do
                if adjacencyMatrix[i][j] = 1 then
                    computers[i].Neighbors.Add(computers[j])

    member _.Computers = computers

    member _.InfectStep() =
        let newInfections = ResizeArray<Computer>()
        for computer in computers do
            if computer.Infected then
                for neighbor in computer.Neighbors do
                    if not neighbor.Infected then
                        newInfections.Add(neighbor)
        
        for computer in newInfections do
            computer.Infect()
