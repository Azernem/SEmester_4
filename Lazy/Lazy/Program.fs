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
            match result with
            | Some value -> value
            | None ->
                lock lockObject (fun () ->
                    if result = None then 
                        result <- Some (supplier())
                        supplier() <- None
                )
                match result with
                | Some value -> value
                | None -> raise (System.InvalidOperationException("Value has not been calculated."))

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


