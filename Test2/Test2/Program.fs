open System
open System.Collections.Generic
open System.Threading

module Test2 = 

    type BlockingQueue<'a>() =
        // Create a queue
        let queue = Queue<'a>()
        // Object used for lock
        let lockObject = obj()

        // Add element to quueue Safe
        member this.Enqueue(item: 'a) =
            lock lockObject (fun () ->
                queue.Enqueue(item)
                Monitor.Pulse lockObject 
            )

        // Remove and return element of queue Safe
        member this.Dequeue() : 'a =
            lock lockObject (fun () ->
                while queue.Count = 0 do
                    Monitor.Wait lockObject |> ignore
                queue.Dequeue()         
            )
