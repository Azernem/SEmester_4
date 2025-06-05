module Test2

open System
open System.Collections.Generic
open System.Threading
open NUnit.Framework
open System.Threading.Tasks

type BlockingQueue<'a>() =
    let queue = Queue<'a>()
    let lockObject = obj()

    member this.Enqueue(item: 'a) =
        lock lockObject (fun () ->
            queue.Enqueue(item)
            Monitor.Pulse lockObject
        )

    member this.Dequeue() : 'a =
        lock lockObject (fun () ->
            while queue.Count = 0 do
                Monitor.Wait lockObject |> ignore
            queue.Dequeue()
        )

[<TestFixture>]
type BlockingQueueTests () =

    [<Test>]
    member this.EnqueueDequeue() =
        let queue = BlockingQueue<int>()
        queue.Enqueue(1)
        let res = queue.Dequeue()
        Assert.AreEqual(1, res)


    [<Test>]
    member _.SimpleMultiThreadedTest () =
        let q = BlockingQueue<int>()
        let produced = [1 .. 100]
        let consumed = ResizeArray<int>() 
        let lockObj = obj()

        let producter = Task.Run(fun () ->
            for x in produced do
                q.Enqueue(x)
        )

        let consumer = Task.Run(fun () ->
            for _ in produced do
                let v = q.Dequeue()
                lock lockObj (fun () -> consumed.Add(v))
        )

        Task.WaitAll(producter, consumer)

        Assert.AreEqual(produced.Length, consumed.Count)
        Assert.AreEqual(Set.ofList produced, Set.ofSeq consumed)


