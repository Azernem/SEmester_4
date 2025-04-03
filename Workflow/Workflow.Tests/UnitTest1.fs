module Workflow.Tests
open FsUnit
open Workflow
open NUnit.Framework

[<SetUp>]
let Setup () =
    ()

[<TestFixture>]
type Tests () 

    [<Test>]
    member this.``Test RoundingWorkflow`` () =
        let rounding = RoundingBuilder(3)
        let result = 2.084

        let rounded = rounding {
            let! a = 2.255
            let! b = 1.105 * 4.255
            let! c = b / a
            return c
        }
        
        Assert.AreEqual(rounded, result, 0.001)
    
    [<Test>]
    member this.``Test valid numbers``() =
        let result = calculate {
            let! x = "1"
            let! y = "2"
            return x + y
        }

        Assert.AreEqual(3.0, result)


