// <copyright file="ParseTree.Tests.cs" company="NematMusaev">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

module ParseTree.Tests

open NUnit.Framework
open FsUnit
open ParseTree

// Test for tree parsing
[<Test>]
let ``Test parse tree`` () =
    let tree: Tree = 
        Node(Multiply, Node(Plus, Operand(1), Operand(2)), Operand(2))
    parse tree |> should equal 6

[<Test>]
let ``Test parseCPS tree`` () =
    let tree: Tree = 
        Node(Multiply, Node(Plus, Operand(1), Operand(2)), Operand(2))
    parseCPS tree |> should equal 6

[<Test>]
let ``Test addition`` () =
    let tree: Tree = Node(Plus, Operand(3), Operand(4))
    parse tree |> should equal 7