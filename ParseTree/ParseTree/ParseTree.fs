// <copyright file="Program.cs" company="NematMusaev">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ParseTree

module ParseTree =

// class of sign, operations in tree calculating
    type Sign =
        | Plus
        | Minus
        | Multiply
        | Divide

// class of tree
    type Tree =
        | Operand of int
        | Node of Sign * Tree * Tree

// Parse tree, calculates and fetches result for root
    let rec parse tree =
        match tree with
        | Operand(n) -> n
        | Node(sign, left, right) ->
            match sign with
            | Plus -> parse left + parse right
            | Minus -> parse left - parse right
            | Multiply -> parse left * parse right
            | Divide -> if parse right <> 0 then parse left / parse right else failwith "Division by zero"

//Parse tree, calculates and fetches result for root in CPS style
    let parseCPS tree =
        let rec parseCPSin tree cont =
            match tree with
            | Operand(n) -> cont n
            | Node(sign, left, right) ->
                parseCPSin left (fun leftResult ->
                    parseCPSin right (fun rightResult ->
                        match sign with
                        | Plus -> cont (leftResult + rightResult)
                        | Minus -> cont (leftResult - rightResult)
                        | Multiply -> cont (leftResult * rightResult)
                        | Divide -> 
                            if rightResult <> 0 then cont (leftResult / rightResult)
                            else failwith "Division by zero"
                    )
                )
    
        parseCPSin tree id
