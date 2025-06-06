module Test2

open System
open System.Collections.Generic
open System.Threading
open NUnit.Framework
open System.Threading.Tasks

/// type of blocking queue
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

    ///enqueue and dequeue
    [<Test>]
    member this.EnqueueDequeue() =
        let queue = BlockingQueue<int>()
        queue.Enqueue(1)
        let res = queue.Dequeue()
        Assert.AreEqual(1, res)

    ///multithread
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
    
    /// tests blocking
    [<Test>]
    member _.SimpleBlockingTest () =
        let queue = BlockingQueue<int>()
        let consumer = Task.Run(fun () ->
            queue.Dequeue()
        )
        Thread.Sleep 500

        queue.Enqueue(99)
        let result = consumer.Result
        Assert.AreEqual(99, result)
    
    /// checks some elements
    [<Test>]
    member _.EnqueuDequeueSomeElementsTest () =
        let q = BlockingQueue<int>()
        q.Enqueue(1)
        q.Enqueue(2)
        q.Enqueue(3)

        Assert.AreEqual(1, q.Dequeue())
        Assert.AreEqual(2, q.Dequeue())
        Assert.AreEqual(3, q.Dequeue())


