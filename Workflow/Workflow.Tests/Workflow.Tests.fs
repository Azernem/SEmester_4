module WfTst

open NUnit.Framework
open FsUnit
open Workflow

// Tests for Workflow
/// ROunding Builder workflow, bind and return
[<Test>]
let testRoundingBuilderWorkflow () =  // Test rounding
    let rounding = RoundingBuilder(3)
    let res = 2.085
    
    let rounded = rounding {
        let! a = 2.255
        let! b = 1.105 * 4.255  
        let! c = b / a
        return c
    }
    
    rounded |> should equal res

/// String Operation workflow, bind and return
[<Test>]
let testStringOperationWorkflow () = 
    let calculate = StringOPerationBuilder()
    let res = calculate {
        let! x = "1"
        let! y = "2" 
        return x + y
    }
    
    res |> should equal (Some 3.0)

[<Test>]
let testStringOperationWithDecimals () = 
    let calculate = StringOPerationBuilder()
    let res = calculate {
        let! a = "3"
        let! b = "2"
        let! c = "10"
        return a * b + c   
    }
    
    res |> should equal (Some 16.0)

