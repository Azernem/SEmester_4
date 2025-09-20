// <copyright file="Program.cs" company="NematMusaev">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Phone
open System.IO
open System

/// The phone directory
module Directory = 
    /// Add contacts
    let addContact name number book = 
        (name, number) :: book
    
    /// find number by name
    let findNumber name book = List.tryFind (fun i -> i |> fst = name) book |> function
        | Some a -> Some (snd a)
        | None -> None
    
    /// find name by number
    let findName number book = List.tryFind (fun i -> i |> snd = number) book |> function
        | Some a -> Some (fst a)
        | None -> None
    
    ///Write contacts to file
    let writeContactsToFile book path =
        book |> List.map (fun (name, number) -> name + "-" + number) |> Array.ofList 
        |> fun list -> File.WriteAllLines(path, list)

    /// Print contacts
    let printContacts path = 
        if File.Exists(path) then
            File.ReadAllLines(path) |> Array.toList |> List.map (fun line ->  (line.Split('-')[0], line.Split('-')[1]))
        else []

/// Interactive process of phone using
module Interactive = 
    let run() = 
        Directory.CreateDirectory("../Details") |> ignore
        let path = "../Details/phone.txt"
        printfn "Phone Directory with Interactive Mode"
        printfn "Commands:"
        printfn "  add <name> <number> - Add contact"
        printfn "  findname <number>   - Find name by number"
        printfn "  findnumber <name>   - Find number by name"
        printfn "  list                - Show contacts"
        printfn "  save                - Save contacts to file"
        printfn "  load                - Load contacts from file" 
        printfn "  exit                - Exit program"
      
        let rec processRequest book = 
            printf "> "
            let input = Console.ReadLine().Trim()
            match input.Split(" ") |> Array.filter(fun el -> el <> "") with
                | [|"exit"|] -> 
                    printfn "Working with phone is completed"  
                    book

                | [|"add"; name; number|] ->
                    let newBook = Directory.addContact name number book 
                    printfn "Contact added: %s - %s" name number
                    processRequest newBook 
                    
                | [|"findname"; number|] ->
                    match Directory.findName number book with
                    | Some name -> printfn "Name: %s" name
                    | None -> printfn "Number not found"
                    processRequest book
                
                | [|"findnumber"; name|] ->
                    match Directory.findNumber name book with
                    | Some number -> printfn "Number: %s" number
                    | None -> printfn "Not found number for this person" 
                    processRequest book
                
                | [|"load"|] -> 
                    let loadedBook = Directory.printContacts path
                    printfn "Contacts loaded from file"
                    processRequest loadedBook

                | [|"list"|] ->
                    if List.isEmpty book then 
                        printfn "Database is empty"
                    else
                        book |> List.iter (fun (name, number) -> printfn "%s - %s" name number) 
                    processRequest book
                
                | [|"save"|] ->
                    Directory.writeContactsToFile book path
                    printfn "Contacts saved to %s" path
                    processRequest book

                | _ ->  
                    printfn "Unknown command. Type 'exit' to quit."
                    processRequest book

        processRequest []  
