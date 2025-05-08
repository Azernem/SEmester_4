module Phone.Tests

open NUnit.Framework
open FsUnit
open Phone.Directory

[<SetUp>]
let Setup () =
    ()

[<TestFixture>]
type UnitTests () = 

    [<Test>]
    member this.``Tests Add Contact`` () = 
        let book = []
        let newBook = addContact "Jone" "566577" book
        newBook[0] |> should equal ("Jone", "566577")
        newBook |> should haveLength 1
    
    [<Test>]
    member this.``Tests write and print contact`` () = 
        let book = []
        let newBook = addContact "Jone" "566577" book
        let path = "test_contacts.txt"
        writeContactsToFile newBook path
        let loaded = printContacts path
        loaded |> should contain ("Jone", "566577")
        if System.IO.File.Exists(path) then System.IO.File.Delete(path)

    [<Test>]
    member this.``Tests find number`` () = 
        let book = []
        let newBook = addContact "Jone" "566577" book
        let newBook = addContact "Jack" "775665" newBook
        findNumber "Jack" newBook |> should equal (Some "775665")
        newBook |> should haveLength 2

    [<Test>]
    member this.``Tests print contacts from nonexistent file`` () =
        let path = "nonexistent_file.txt"
        let loaded = printContacts path
        loaded |> should equal []
