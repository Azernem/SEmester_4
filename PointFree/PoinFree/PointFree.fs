// <copyright file="PointFree.cs" company="NematMusaev">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace PointFree

/// Designations for steps in getting point-free from simple fucntion.
module PointFree =
    let func x l = List.map (fun y -> y * x) l
    let fun1 x = List.map (fun y -> x * y)
    let fun2 x = List.map ((*) x)
    let functionMain () = (*) >> List.map 
