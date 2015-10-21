module fs_stringcalculator

open NUnit.Framework
open System

let stringCalc(input) =
    if input = "" then 0
    else Int32.Parse input

[<Test>]
let ``Empty string equals 0``() = 
    let result = stringCalc ""
    Assert.That(result, Is.EqualTo 0)

[<Test>]
let ``Single number in string equals same number``() =
    let result = stringCalc "8"
    Assert.That(result, Is.EqualTo 8)