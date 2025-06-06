namespace PrimeNumbers

module PrimeNumbers = 
    let generate () = 
        let IsPrime n = 
           not (List.exists  (fun i ->  n % i = 0) [2 .. (sqrt (float n)) |> int])
        Seq.filter  IsPrime (Seq.initInfinite (fun i -> i + 2) )
    
    let primes = Seq.take 20 (generate ())
    printfn "%A" (Seq.toList primes)

