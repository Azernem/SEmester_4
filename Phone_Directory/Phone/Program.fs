namespace Phone
open System.IO

/// The phone directory
module Directory = 
    /// add contacts
    let addContact name number book = 
        (name, number) :: book
    
    /// find number about name
    let findNumber name book = List.tryFind (fun i -> i |> fst = name) book |> function
        | Some a -> Some (snd a)
        | None -> None
    
    /// find name about number
    let findName number book = List.tryFind (fun i -> i |> snd = number) book |> function
        | Some a -> Some (fst a)
        | None -> None
    
    ///write contacts to file
    let writeContactsToFile book path =
        book |> List.map (fun i -> (i |> fst) + "-" + (i |> snd)) |> Array.ofList 
        |> fun list -> File.WriteAllLines(path, list)

    /// print contacts
    let printContacts path = 
        if File.Exists(path) then
            File.ReadAllLines(path) |> Array.toList |> List.map (fun line -> (line.Split('-')[0], line.Split('-')[1]))
        else []
