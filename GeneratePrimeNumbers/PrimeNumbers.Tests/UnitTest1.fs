module PrimeNumbers.Tests

open NUnit.Framework
open PrimeNumbers
open FsUnit

[<TestFixture>]
type PrimeNumbers () =
    [<Test>]
    member this.``Test first ten numbers`` () =
        (generate () |> Seq.take 10 |> Seq.toList) |> should equal [2; 3; 5; 7;  11; 13; 17; 19; 23; 29]

    [<Test>]
    member this.``Test first twenty numbers`` () =
        (generate () |> Seq.take 20 |> Seq.toList) |> should equal [2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47; 53; 59; 61; 67; 71]