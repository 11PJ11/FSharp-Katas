module Tests

    open bankOcr
    open NUnit.Framework
    open FsCheck
    open FsCheck.NUnit
    open FsUnit 


    [<Test>]
    let ignore_me () = ()

    [<Test>]
    let assert_it_works () =
        true |> should equal true 