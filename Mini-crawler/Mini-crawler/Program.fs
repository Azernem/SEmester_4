
module MiniCrawler
open System
open System.Net.Http
open System.Text.RegularExpressions

/// Downloads a page and prints URL with content length
let downloadAndPrintPageSize (url: string) =
    async {
        use client = new HttpClient()

        try
            let! response = client.GetAsync(url) |> Async.AwaitTask
            let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
            Console.WriteLine($"{url} — {content.Length} characters")
        with
        | :? HttpRequestException as ex ->
            Console.WriteLine($"{url}, {ex.Message}")
    }

/// Get all links at page about regex.
let funcLinks (pageHtml: string) (linkRegex: Regex) =
    [ for matchItem in linkRegex.Matches(pageHtml) do
        matchItem.Groups.["link"].Value ] 

/// Downloads the main page, finds download links
let processPageAndLinks (startUrl: string) =
    async {
        use client = new HttpClient()
        let! pageHtml = client.GetStringAsync(startUrl) |> Async.AwaitTask
        let linkRegex = Regex(@"<a\s+href=""(?<link>http[^""]+)""", RegexOptions.IgnoreCase)
        

        let links = funcLinks pageHtml linkRegex

        links
        |> List.distinct
        |> List.map downloadAndPrintPageSize
        |> Async.Parallel
        |> Async.Ignore
        |> Async.RunSynchronously
    }