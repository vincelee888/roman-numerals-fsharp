module tests
    open NUnit.Framework
    open Swensen.Unquote

    [<Test>]
    let ``2 + 2 == 4``() = 
        test <@ 2 + 2 = 5 @>
