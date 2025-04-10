module Test.Tests

open NUnit.Framework
open Test

[<SetUp>]
let Setup () =
    ()

[<TestFixture>]
type TreeTests () =

    [<Test>]
    member this.``Test ResultEndlessSequence`` () =
        let list = Seq.take 4 resultEndlessSequence
        let result = Seq.ofList [1; -2; 3; -4]
        Assert.AreEqual(result, list)
        
