module LazyTests

open NUnit.Framework
open System.Threading
open System.Threading.Tasks
open Lazy

[<Test>]
let ``OneThreadLazy computes`` () =
    let mutable count = 0
    let supplier () =
        count <- count + 1
        count

    let lazyOblect = OneThreadLazy(supplier) :> ILazy<int>

    let first = lazyOblect.Get()
    Assert.AreEqual(1, count)
    Assert.AreEqual(1, first)
    let second = lazyOblect.Get()
    Assert.AreEqual(1, second)
    Assert.AreEqual(1, count)

[<Test>]
let ``MultiThreadLazy computes `` () =
    let mutable count = 0
    let supplier () =
        count <- count + 1
        count + 10

    let lazyOblect = MultiThreadLazy(supplier) :> ILazy<int>

    let tasks = [| for _ in 1 .. 500 -> Task.Run(fun () -> lazyOblect.Get() |> ignore) |]
    Task.WaitAll(tasks)
    Assert.AreEqual(1, count)
    let value = lazyOblect.Get()
    Assert.AreEqual(11, value)

[<Test>]
let ``LockFreeLazy computes `` () =
    let mutable count = 0
    let supplier () =
        count <- count + 1
        count + 10

    let lazyOblect = LockFreeLazy(supplier) :> ILazy<int>

    let tasks = [| for _ in 1 .. 1000 -> Task.Run(fun () -> lazyOblect.Get() |> ignore) |]
    Task.WaitAll(tasks)

    Assert.AreEqual(1, count)
    Assert.LessOrEqual(count, 1000)
    let value = lazyOblect.Get()
    Assert.AreEqual(11, value)
