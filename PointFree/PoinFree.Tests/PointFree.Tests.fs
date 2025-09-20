module Tests

open NUnit.Framework
open FsUnit
open FsCheck
open FsCheck

// MMain functions
let func x l = List.map (fun y -> x * y) l
let funMain = (*) >> List.map

// Tests
[<Test>]
let ``Simple test func works correctly`` () =
    func 2 [1; 2; 3] |> should equal [2; 4; 6]
    func 0 [1; 2; 3] |> should equal [0; 0; 0]

[<Test>]
let ``Simple test funMain works correctly`` () =
    funMain 2 [1; 2; 3] |> should equal [2; 4; 6]
    funMain 0 [1; 2; 3] |> should equal [0; 0; 0]

[<Test>]
let ``functions give same results`` () =
    func 3 [1; 2; 3] |> should equal (funMain 3 [1; 2; 3])
    func 10 [] |> should equal (funMain 10 [])

[<Test>]
let ``FsCheck func and funMain are equivalent`` =
    Check.Quick (fun (x: int) (l: int list) -> func x l = funMain x l)