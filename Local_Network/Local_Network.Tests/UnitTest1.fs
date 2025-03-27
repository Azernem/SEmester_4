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
