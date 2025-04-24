open NUnit.Framework
open FsUnit
open System.Text.RegularExpressions
open MiniCrawler

[<TestFixture>]
type FuncLinksTests() =

    let linkRegex = Regex(@"<a\s+href=""(?<link>http[^""]+)""", RegexOptions.IgnoreCase)

    [<Test>]
    member _.``Takes unique http links from HTML``() =
        let html = """
            <html>
                <body>
                    <a href="http://example.com/page1">Page 1</a>
                    <a href="http://example.com/page2">Page 2</a>
                    <a href="http://example.com/page1">Page 1 again</a>
                </body>
            </html>
        """

        let links = funcLinks html linkRegex |> List.distinct

        links.Length |> should equal 2
        links |> should contain "http://example.com/page1"
        links |> should contain "http://example.com/page2"
    
    [<Test>]
    member _.``Returns empty list if page without links``() =
        let html = "<html><body>No links here</body></html>"
        let links = funcLinks html linkRegex |> List.distinct
        links |> should be Empty

    [<Test>]
    member this.``processPageAndLinks should process links without exceptions`` () =
        let testUrl = "https://example.com"
        let computation = MiniCrawler.processPageAndLinks testUrl
        let exn =
            try
                Async.RunSynchronously computation
                null
            with ex -> ex
        Assert.That(exn, Is.Null)


    
         
    
  