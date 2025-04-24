module Lazy
open System.Threading

type ILazy<'a> =
    abstract Get: unit -> 'a

/// One thread working
type OneThreadLazy<'a>(supplier: unit -> 'a) =
    let mutable result = None
    interface ILazy<'a> with
        member _.Get() =
            match result with
            | Some value -> value
            | None ->
                result <- Some (supplier())
                result.Value

/// Multythreading
type MultiThreadLazy<'a>(supplier: unit -> 'a) =
    let lockObject = obj()
    let mutable result = None
    interface ILazy<'a> with
        member _.Get() =
            lock lockObject (fun () ->
                match result with
                | Some value -> value
                | None ->
                    result <- Some (supplier())
                    result.Value
            )

/// Multythreadind with repeated evaluation and right cashing.
type LockFreeLazy<'a>(supplier: unit -> 'a) =
    let mutable result = None
    interface ILazy<'a> with
        member _.Get() =
            match Volatile.Read(&result) with
            | Some value -> value
            | None ->
                Interlocked.CompareExchange(&result, Some (supplier()), None) |> ignore
                result.Value


