module Phone.Tests

open NUnit.Framework
open FsUnit
open Phone.Directory

/// Tests for phone directory
[<Test>]
let ``Tests Add Contact`` () = 
    let book = []
    let newBook = addContact "Jone" "566577" book
    newBook[0] |> should equal ("Jone", "566577")
    newBook |> should haveLength 1

[<Test>]
let ``Tests write and print contact`` () = 
    let book = []
    let newBook = addContact "Jone" "566577" book
    let path = "test_contacts.txt"
    writeContactsToFile newBook path
    let loaded = printContacts path
    loaded |> should contain ("Jone", "566577")
    if System.IO.File.Exists(path) then System.IO.File.Delete(path)

[<Test>]
let ``Tests find number`` () = 
    let book = []
    let newBook = addContact "Jone" "566577" book
    let newBook = addContact "Jack" "775665" newBook
    findNumber "Jack" newBook |> should equal (Some "775665")
    newBook |> should haveLength 2

