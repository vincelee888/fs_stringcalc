module fs_stringcalculator

open NUnit.Framework
open System

let getDelimiter(input:string) =
    match input.StartsWith "//" with 
    | true -> input.[2]
    | false -> ','

let getNumbers(input:string) =
    match input.StartsWith "//" with 
    | true -> input.Substring(input.IndexOf("\n") + 1)
    | false -> input

let stringCalc(input:string) =
    let delimiter = getDelimiter input
    let values = getNumbers input
    match values.Length with
    | 0 ->  0
    | _ ->  values.Replace('\n', delimiter).Split delimiter
            |> Seq.map Int32.Parse
            |> Seq.sum

[<Test>]
let ``Empty string equals 0``() = 
    let result = stringCalc ""
    Assert.That(result, Is.EqualTo 0)

[<Test>]
let ``Single number in string equals same number``() =
    let result = stringCalc "8"
    Assert.That(result, Is.EqualTo 8)

[<Test>]
let ``Multiple numbers delimited by commas are added``() =
    let result = stringCalc "1,2,3"
    Assert.That(result, Is.EqualTo 6)

[<Test>]
let ``New line can act as delimiter``() =
    let result = stringCalc "1\n2,3"
    Assert.That(result, Is.EqualTo 6)

[<Test>]
let ``Input can define delimiter``() =
    let result = stringCalc "//;\n1;2;3"
    Assert.That(result, Is.EqualTo 6)