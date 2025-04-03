namespace Workflow
open System

type RoundingBuilder(decimals: int) = 
    member this.Bind(x: float, f) = 
        let x = Math.Round (x, decimals)
        f x
    member this.Return(x: float) = Math.Round (x, decimals)

type StringOPerationBuilder(decimals: int) = 
    member this.Bind(x: string, f) = 
        match float.TryParse(x) with
        | true, value -> value
        | false, _ -> None
    member this.Return(x: float) = x