namespace Test

module Test = 
    let endlessSequence = Seq.initInfinite (fun i -> if i % 2 = 0 then 1 else -1)
    let simpleSequence = Seq.initInfinite (fun i -> i + 1)
    let resultEndlessSequence = Seq.map (fun (x, y) -> x * y) (Seq.zip endlessSequence simpleSequence)

    type Tree<'a > = 
        | Node of 'a * Tree<'a > * Tree<'a >
        | Empty

    let rec filterTree predicate tree =
        match tree with
        | Empty -> []
        | Node (value, left, right) ->
            let rightValue = if predicate value then [value] else []
            List.concat [rightValue; filterTree predicate left; filterTree predicate right]

    type PriorityQueue() =
        let mutable queue = []
        
        member this.Enqueue(value, priority) =
            let rec recEnqueue couples newCouple =
                match couples with
                | [] -> [newCouple]
                | (headValue, headPriority) :: tail when priority <= headPriority -> (value, priority) :: couples
                | head :: tail -> head :: (recEnqueue tail newCouple)
            queue <- recEnqueue queue (value, priority)

        member this.Dequeue() =
            match queue with
            | [] -> failwith "Queue is empty"
            | head :: tail ->
            let result = fst head
            queue <- tail
            result