open System
open System.Collections.Generic
open System.Threading

module Test2 = 

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
