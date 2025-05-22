module Interpreter

type Lambdaexp =
    | Var of string
    | Abstraction of string * Lambdaexp
    | Application of Lambdaexp * Lambdaexp

let rec aConversionAndReplace firstName secondName exp =
    match exp with
    | Var x when x = firstName -> Var secondName
    | Var x -> Var x
    | Abstraction (x, body) when x = firstName -> Abstraction (secondName, aConversionAndReplace firstName secondName body)
    | Abstraction (x, body) -> Abstraction (x, aConversionAndReplace firstName secondName body)
    | Application (f, a) -> Application (aConversionAndReplace firstName secondName f, aConversionAndReplace firstName secondName a)

let rec freeVar exp =
    match exp with
    | Var x -> Set.singleton x
    | Abstraction (x, body) -> Set.remove x (freeVar body)
    | Application (f, a) -> Set.union (freeVar f) (freeVar a)


let rec update used acc= 
    if Set.contains acc used then update used (acc + string 0) else acc 

let rec substitute x s e =
    match e with
    | Var y -> if y = x then s else Var y
    | Application (f, a) -> Application (substitute x s f, substitute x s a)
    | Abstraction (y, body) ->
        if y = x then
            Abstraction (y, body) 
        else
            let freeS = freeVar s
            if Set.contains y freeS then
                let used = Set.union (freeVar body) freeS
                let z = update used "x"
                let bodyRenamed = aConversionAndReplace y z body
                Abstraction (z, substitute x s bodyRenamed)
            else
                Abstraction (y, substitute x s body)

let rec reduceNormal exp =
    match exp with
    | Application (Abstraction (x, body), arg) ->
        reduceNormal (substitute x (reduceNormal arg) body)
    | Application (f, a) ->
        let f' = reduceNormal f
        match f' with
        | Abstraction _ -> reduceNormal (Application (f', a))
        | _ -> Application (f', reduceNormal a)
    | Abstraction (x, body) -> Abstraction (x, reduceNormal body)
    | Var _ -> exp