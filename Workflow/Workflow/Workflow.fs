
// <copyright file="Program.cs" company="NematMusaev">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Workflow
open System

/// Rounding Builder workflow with ibnd and return members 
type RoundingBuilder(decimals: int) = 
    member this.Bind(x: float, f) = 
        let x = Math.Round (x, decimals)
        f x
    member this.Return(x: float) = Math.Round (x, decimals)

/// String Builder workflow with return and bind
type StringOPerationBuilder() = 
    member this.Bind(x: string, f) = 
        match System.Double.TryParse(x) with
        | true, value -> f value
        | false, _ ->  None
    member this.Return(x: float) = Some x