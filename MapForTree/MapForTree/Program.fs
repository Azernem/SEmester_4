namespace MapForTree
module MapTree = 
    type Tree<'a > = 
        | Node of 'a * Tree<'a > * Tree<'a >
        | Empty
    
    let rec mapTree func tree = 
        match tree with
        | Node(value, left, right) -> Node(func value, mapTree func left, mapTree func right)
        | Empty -> Empty
        
    
