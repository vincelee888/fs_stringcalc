module fs_stringcalculator

open NUnit.Framework

let stringCalc(input) =
    0

[<Test>]
let ``Empty string equals 0``() = 
    let result = stringCalc ""
    Assert.That(result, Is.EqualTo 0)