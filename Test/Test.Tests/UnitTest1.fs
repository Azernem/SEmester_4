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
        let firstFour = resultEndlessSequence () |> Seq.take 4 |> Seq.toList
        let expected = [1; -2; 3; -4]
        Assert.AreEqual(expected, firstFour)

    [<Test>]
    member this.``Test FilterTree`` () =
        let tree = Node (1, Node (2, Empty, Empty), Node (3, Empty, Empty))
        let filtered = filterTree (fun x -> x % 2 = 0) tree
        let result = [2]
        Assert.AreEqual(result, filtered)

    [<Test>]
    member this.``Test Enqueue and Dequeue`` () =
        let queue = PriorityQueue<int>()
        queue.Enqueue(10, 3)
        queue.Enqueue(20, 1)

        let firstElement = queue.Dequeue()
        Assert.AreEqual(20, firstElement)

        let secondElement = queue.Dequeue()
        Assert.AreEqual(10, secondElement)

    

    

    
