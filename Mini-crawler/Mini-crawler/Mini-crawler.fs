// <copyright file="Mini-crawler.cs" company="NematMusaev">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MiniCrawler
open System
open System.Net.Http
open System.Text.RegularExpressions

module MiniCrawler =

/// Downloads a page and prints URL with content length
    let downloadAndPrintPageSize (url: string) =
        async {
            use client = new HttpClient()

            try
                let! response = client.GetAsync(url) |> Async.AwaitTask
                let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
                Console.WriteLine($"{url} — {content.Length} characters")
                return Ok (url, content.Length)
            with
            | :? HttpRequestException as ex ->
                Console.WriteLine($"{url}, {ex.Message}")
                return Error ()
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