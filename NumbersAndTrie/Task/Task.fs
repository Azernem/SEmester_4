namespace Task
module Task = 
    let CountEvenNumbered1 list = 
        List.length (List.filter (fun x -> x % 2 = 0) list)
    let CountEvenNumbered2 list = 
        List.sum (List.map (fun x -> if x % 2 = 0 then 1 else 0) list)

    let CountEvenNumbered3 list = 
        List.fold (fun x acc -> if x % 2 = 0 then (acc + 1) else acc) 0 list
    
    printf "%d" (CountEvenNumbered1 [1; 2; 3; 4])