module Lambda_interpreter.Tests

open NUnit.Framework
open Interpreter 

[<Test>]
let ``Substitute id`` () =
    let expr = Abstraction ("x", Var "x")
    let s = Var "y"
    let result = substitute "x" s expr
    Assert.AreEqual(expr, result)

[<Test>]
let ``aConversionTest`` () =
    let expr = Abstraction ("x", Application(Var "x", Var "y"))
    let result = aConversionAndReplace "x" "z" expr
    let expected = Abstraction( "z", Application(Var "z", Var "y") )
    Assert.AreEqual(expected, result)

[<Test>]
let ``SubstituteTest`` () =
    let expr = Abstraction ("x", Abstraction ("y", Application (Var "x", Var "y")))
    let s = Var "y"
    let result = substitute "x" s expr
    match result with
    | Abstraction (z, Abstraction (y2, body)) ->
        Assert.AreEqual("x", z)
        Assert.AreEqual("y", y2)
    | _ -> Assert.Fail("Unexpected structure")

[<Test>]
let ``ReduceRedex`` () =
    let expr = Application (Abstraction ("x", Var "x"), Var "y")
    let reduced = reduceNormal expr
    Assert.AreEqual(Var "y", reduced)

[<Test>]
let ``Reduce twice application`` () =
    let id = Abstraction ("x", Var "x")
    let expr = Application (Abstraction ("x", Application (Var "x", Var "x")), id)
    let reduced = reduceNormal expr
    match reduced with
    | Abstraction (x, a) ->
        Assert.AreEqual("x", x)
        Assert.AreEqual(Var "x", a)
    | _ -> Assert.Fail("Unexpected structure")

[<Test>]
let bReduceNormal() =
    let expr = 
        Application(
            Abstraction("x", Abstraction("y", Application(Var "x", Var "y"))),
            Var "y"
        )
    
    let result = reduceNormal expr

    match result with
    | Abstraction(newVar, Application(Var v1, Var v2)) ->
        Assert.AreEqual("x0", newVar)
        Assert.AreEqual("y", v1)
        Assert.AreEqual("x0", v2)
    | _ -> Assert.Fail()