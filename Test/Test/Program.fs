// Declaration of the Test module Nemat Musaev
namespace Test

module Test = 
    // Sequence of 1 and -1
    let endLessSequence = Seq.initInfinite (fun i -> if i % 2 = 0 then 1 else -1)
    
    // simple sequence
    let simpleSequence = Seq.initInfinite (fun i -> i + 1)
    
    // Result of elements from two sequences
    let resultEndlessSequence () = Seq.map (fun (x, y) -> x * y) (Seq.zip endLessSequence simpleSequence)

    // Type for a binary tree
    type Tree<'a> = 
        | Node of 'a * Tree<'a> * Tree<'a>
        | Empty

    // Recursive the tree
    let rec filterTree predicate tree =
        match tree with
        | Empty -> [] 
        | Node (value, left, right) ->
            let rightValue = if predicate value then [value] else [] 
            List.concat [rightValue; filterTree predicate left; filterTree predicate right] 

    // Type for a priority queue
    type PriorityQueue<'T>() =
        let mutable queue = [] : ('T * int) list

        // Add an element to the queue
        member this.Enqueue(value, priority) =
            let rec recEnqueue couples newCouple =
                match couples with
                | [] -> [newCouple] 
                | (headValue, headPriority) :: tail when priority <= headPriority -> (value, priority) :: couples 
            queue <- recEnqueue queue (value, priority) 

        // Remove an element from the queue
        member this.Dequeue() =
            match queue with
            | [] -> failwith "Queue is empty"
            | head :: tail ->
                let result = fst head 
                queue <- tail 
                result
