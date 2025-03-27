namespace Network


let simulate network =
    let rec loop step =
        for c in network.Computers do
            printfn $"Compter {c.Id} ({c.OS}): {if c.Infected then "Infected" else "Clean"}"
        
        let preventInfected = network.Computers |> List.filter (fun c -> c.Infected) |> List.length
        network.InfectStep()
        let currentInfected = network.Computers |> List.filter (fun c -> c.Infected) |> List.length

        if currentInfected = preventInfected then
            printfn "Simulation stopped"
        else
            loop (step + 1)
    
    loop 0
