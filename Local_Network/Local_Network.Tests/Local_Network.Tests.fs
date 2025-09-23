namespace LocalNetworks.Tests
open NUnit.Framework
open FsUnit
open Network
open Computer
open OS

/// Class for tests
[<TestFixture>]
type ComputerTests() =
    [<Test>]
    member _.``Computer becomes infected when calling Infect()``() =
        let computer = Computer(1, Windows)
        computer.Infected |> should be False
        computer.Infect()
        computer.Infected |> should be True

    [<Test>]
    member _.``Neighbors are set correctly from adjacency matrix``() =
        let c1 = Computer(1, Windows)
        let c2 = Computer(2, Linux)
        let c3 = Computer(3, MacOS)
        let computers = [c1; c2; c3]
        let matrix = [| [|0; 1; 0|]; [|1; 0; 1|]; [|0; 1; 0|] |]
        let network = Network(computers, matrix)

        c1.Neighbors |> Seq.exists (fun n -> n.Id = 2) |> should be True
        printf "%d" c1.Neighbors.Count
        printf "%d" c1.Neighbors.Count
        c1.Neighbors |> should not' (contain c3)
        c2.Neighbors |> should contain c1
        c2.Neighbors |> should contain c3
        c3.Neighbors |> should contain c2
    
    [<Test>]
    member _.``Distribution of Infwction``() =
        let c1 = Computer(1, Windows) 
        let c2 = Computer(2, Linux)    
        let c3 = Computer(3, Windows) 
        let c4 = Computer(4, MacOS)    
        
        let computers = [c1; c2; c3; c4]
        let matrix = [|
            [|0; 1; 0; 1|] 
            [|1; 0; 1; 0|]  
            [|0; 1; 0; 1|]  
            [|1; 0; 1; 0|]  
        |]
        let network = Network(computers, matrix)
        c1.Infect()
        network.SimpleInfectStep()

        c1.Infected |> should be True
        c2.Infected |> should be True
        c4.Infected |> should be True
        c3.Infected |> should be False
