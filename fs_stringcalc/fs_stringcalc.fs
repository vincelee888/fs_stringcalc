module fs_stringcalculator

open NUnit.Framework
open System
open Swensen.Unquote

let newline = '\n'
let customDelimiterPrefix = "//"

let getDelimiter(input:string) =
    match input.StartsWith customDelimiterPrefix with 
    | true -> input.[2]
    | false -> ','

let getNumbers(input:string) =
    match input.StartsWith customDelimiterPrefix with 
    | true -> input.Substring(input.IndexOf(newline) + 1)
    | false -> input

exception NegativeValueError of string

let parse(input:string) =
    let value = Int32.Parse input
    if value < 0 then raise (NegativeValueError("Can not accept negatives"))
    else value

let stringCalc(input:string) =
    let delimiter = getDelimiter input
    let values = getNumbers input
    match values.Length with
    | 0 ->  0
    | _ ->  values.Replace(newline, delimiter).Split delimiter
            |> Seq.map parse
            |> Seq.filter (fun x -> x < 1000)
            |> Seq.sum

[<Test>]
let ``Empty string equals 0``() = 
    test <@ 0 = stringCalc "" @>

[<Test>]
let ``Single number in string equals same number``() =
    test <@ 8 = stringCalc "8" @>

[<Test>]
let ``Multiple numbers delimited by commas are added``() =
    test <@ 6 = stringCalc "1,2,3" @>

[<Test>]
let ``New line can act as delimiter``() =
    test <@ 6 = stringCalc "1\n2,3" @>

[<Test>]
let ``Input can define delimiter``() =
    test <@ 6 = stringCalc "//;\n1;2;3" @>

[<Test>]
let ``Negative numbers throw exception``() =
    raises<NegativeValueError> <@ stringCalc "1,-2,3" @>

[<Test>]
let ``Large numbers (over 999) are ignored``() =
    raises<NegativeValueError> <@ stringCalc "1,-2,3,1000" @>