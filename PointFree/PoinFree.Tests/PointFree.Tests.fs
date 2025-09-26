module Tests

open NUnit.Framework
open FsUnit
open FsCheck
open FsCheck
open PointFree

// Tests
[<Test>]
let ``Simple test func works correctly`` () =
    PointFree.func 2 [1; 2; 3] |> should equal [2; 4; 6]
    PointFree.func 0 [1; 2; 3] |> should equal [0; 0; 0]

[<Test>]
let ``Simple test functionMain works correctly`` () =
    PointFree.functionMain () 2 [1; 2; 3] |> should equal [2; 4; 6]
    PointFree.functionMain () 0 [1; 2; 3] |> should equal [0; 0; 0]

[<Test>]
let ``functions give same results`` () =
    PointFree.func 3 [1; 2; 3] |> should equal (PointFree.functionMain () 3 [1; 2; 3])
    PointFree.func 10 [] |> should equal (PointFree.functionMain () 10 [])

[<Test>]
let ``FsCheck func and functionMain are equivalent`` =
    Check.QuickThrowOnFailure (fun (x: int) (l: int list) -> PointFree.func x l = PointFree.functionMain () x l)