namespace Staples.Tests

open NUnit.Framework
open Staples
open FsUnit

[<TestFixture>]
type StaplesTests () =

    [<Test>]
    member this.EmptyStringIsCorrect () =
        let result = Staples.areCorrectStaples [] 
        Assert.AreEqual(true, result)

    [<Test>]
    member this.SimpleCorrectPairs () =
        Staples.areCorrectStaples (Seq.toList "()") |> should equal true
        Staples.areCorrectStaples (Seq.toList "{}") |> should equal true
        Assert.AreEqual(true, Staples.areCorrectStaples (Seq.toList "[]") 

    [<Test>]
    member this.NestedCorrectPairs () =
        Assert.AreEqual(true, Staples.areCorrectStaples (Seq.toList "({[]})") )
        Assert.AreEqual(true, Staples.areCorrectStaples (Seq.toList "[({})]") )

    [<Test>]
    member this.IncorrectPairs () =
        Assert.AreEqual(false, Staples.areCorrectStaples (Seq.toList "(]"))
        Assert.AreEqual(false, Staples.areCorrectStaples (Seq.toList "([)]") )

    [<Test>]
    member this.StringWithNonStapleCharacters () =
        Assert.AreEqual(true, Staples.areCorrectStaples (Seq.toList "(a[b]{c})") )
        Assert.AreEqual(false, Staples.areCorrectStaples (Seq.toList "(a[b{c})") )
