namespace Phone
open System.IO
module Directory = 
    let addContact name number book = 
        (name, number) :: book
    
    let findNumber name book = List.tryFind (fun i -> i |> fst = name) book |> function
        | Some a -> Some (snd a)
        | None -> None
    
    let findName number book = List.tryFind (fun i -> i |> snd = number) book |> function
        | Some a -> Some (snd a)
        | None -> None
    
    let writeContactsToFile book path =
        if File.Exists(path) then
            book |> List.map (fun i -> (i |> fst) + "-" + (i |> snd)) |> Array.ofList 
            |> fun list -> File.WriteAllLines(path, list)
        else failwithf "doesnt exists file"
    
    let printContacts path = 
        if File.Exists(path) then
            File.ReadAllLines(path) |> Array.toList |> List.map (fun line -> (line.Split('-')[0], line.Split('-')[1]))
        else []
