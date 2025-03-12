namespace ParseTree

module ParseTree =
    type Sign =
        | Plus
        | Minus
        | Multiply
        | Divide

    type Tree =
        | Operand of int
        | Node of Sign * Tree * Tree

    let rec parse tree =
        match tree with
        | Operand(n) -> n
        | Node(sign, left, right) ->
            match sign with
            | Plus -> parse left + parse right
            | Minus -> parse left - parse right
            | Multiply -> parse left * parse right
            | Divide -> if parse right <> 0 then parse left / parse right else failwith "Division by zero"
