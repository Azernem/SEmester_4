namespace MapForTree

/// class of tree with constructer
module MapTree = 
    type Tree<'a > = 
        | Node of 'a * Tree<'a > * Tree<'a >
        | Empty
    
    // Direct bypassing of tree in CPS style
    let mapTreeCPS func tree =
            let rec mapTreeCPS tree cont =
                match tree with
                | Node(value, left, right) ->
                    let newValue = func value
                    mapTreeCPS left (fun mappedLeft ->
                        mapTreeCPS right (fun mappedRight ->
                            cont (Node(newValue, mappedLeft, mappedRight))
                        )
                    )
                | Empty -> cont Empty
            
            mapTreeCPS tree id

    /// Simple map for tree without linearization
    let rec mapTree func tree = 
        match tree with
        | Node(value, left, right) -> Node(func value, mapTree func left, mapTree func right)
        | Empty -> Empty
        
    
