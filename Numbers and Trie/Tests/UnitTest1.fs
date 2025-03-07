module Tests

open NUnit.Framework
open FsUnit
open FsCheck
open Task

let equiValenceFirst (list: int list) = 
    Task.CountEvenNumbered1 list = Task.CountEvenNumbered2 list

let equiValenceSecond (list: int list) = 
    Task.CountEvenNumbered1 list = Task.CountEvenNumbered3 list

let equiValenceThird (list: int list) = 
    Task.CountEvenNumbered2 list = Task.CountEvenNumbered3 list

[<TestFixture>]
type EquivalenceTests () =
    [<Test>]
    member this.TestEquivalenceFirst () =
        Check.Quick (fun (list: int list) -> equiValenceFirst list)

    [<Test>]
    member this.TestEquivalenceSecond () =
        Check.Quick (fun (list: int list) -> equiValenceSecond list)

    [<Test>]
    member this.TestEquivalenceThird () =
        Check.Quick (fun (list: int list) -> equiValenceThird list)
