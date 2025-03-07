module ParseTree.Tests

open NUnit.Framework
open FsUnit
open ParseTree

[<SetUp>]
let Setup () =
    ()

[<TestFixture>]
type ParseTreeTests () =
    [<Test>]
    member this.``Test parse tree`` () =
        let tree: Tree = 
            Node(Multiply, Node(Plus, Operand(1), Operand(2)), Operand(2))

        parse tree |> should equal 6

    [<Test>]
    member this.``Test addition`` () =
        let tree: Tree = Node(Plus, Operand(3), Operand(4))
        parse tree |> should equal 7
