module MapForTree.Tests

open NUnit.Framework
open FsUnit
open FsCheck
open MapTree

[<SetUp>]
let Setup () =
    ()

[<TestFixture>]
type TreeTests () =
    [<Test>]
    member this.``Test mapTree`` () =
        let tree: Tree<int> = 
            Node (3,
                Node (9,
                        Node (1, Empty, Empty),
                        Node (2, Empty, Empty)),
                Node (11, Empty, Empty))

        let checkTree: Tree<int> =
            Node (6,
                Node (18,
                        Node (2, Empty, Empty),
                        Node (4, Empty, Empty)),
                Node (22, Empty, Empty))

        mapTree (fun x -> x * 2) tree |> should equal checkTree

    [<Test>]
    member this.TestRightIntoHerSelf () =
        let GoIntoHerSelf tree =
            mapTree (fun x -> x) tree = tree
            
        Check.Quick (fun (tree: Tree<string>) -> GoIntoHerSelf tree)
        Check.Quick (fun (tree: Tree<int>) -> GoIntoHerSelf tree)


