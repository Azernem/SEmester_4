namespace Local_Networks.Tests
open NUnit.Framework
open FsUnit
open Network


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

        c1.Neighbors |> should contain c2
        c1.Neighbors |> should not' (contain c3)
        c2.Neighbors |> should contain c1
        c2.Neighbors |> should contain c3
        c3.Neighbors |> should contain c2
