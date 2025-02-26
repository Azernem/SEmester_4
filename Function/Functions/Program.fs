// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

module Functions =

    /// Computes factorial of number
    let rec factorial number = 
        match number with
        | _ when number < 0 -> None
        | 0 -> Some(1)
        | _ -> Some(Seq.reduce (*) {1 .. number}) 

    /// Computes Fibonacci in number
    let rec fibonacci number = 
        if number < 0 then None
        elif number = 0 then Some(0)
        elif number = 1 then Some(1)
        else 
            match fibonacci (number - 1), fibonacci(number - 2) with
            | Some(a), Some(b) -> Some(a + b)
            | _ -> None

    /// Reverses a list.
    let reverseList list = 
        let rec recReverseList list acc = 
            match list with
            | [] -> acc
            | head :: tail -> recReverseList tail (head :: acc)
        recReverseList list []

    /// Gets position of number at list.
    let getPositionAtList number list = 
        let rec recGetPositionAtList number list acc = 
            match list with
            | [] -> None
            | head :: tail -> if head = number then Some(acc) else recGetPositionAtList number tail (acc + 1)
        recGetPositionAtList number list 0

    /// Gets list of powers of two from n to n + m.
    let raisePowerTwo n m = 
        if m < 0 then None
        else
            let rec recRaisePowerTwo acc = 
                match acc with
                | head :: tail -> if head = (pown 2 n) then Some(acc) else recRaisePowerTwo ((head / 2) :: acc)
            recRaisePowerTwo [pown 2 (n + m)]