namespace Network
open Computer

module Simulate = 
    let simulate (network: Network) =
        let rec loop step =
            for c in network.Computers do
                let status = if c.Infected then "Infected" else "Clean"
                printfn $"Computer {c.Id} ({c.OS}): {status}"
            
            let preventInfected = network.Computers |> List.filter _.Infected |> List.length
            network.InfectStep()
            let currentInfected = network.Computers |> List.filter (fun c -> c.Infected) |> List.length

            if currentInfected = preventInfected then
                printfn "Simulation stopped"
            else
                loop (step + 1)
        
        loop 0
