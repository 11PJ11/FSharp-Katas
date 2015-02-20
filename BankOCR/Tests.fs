module Tests

    open bankOcr
    open NUnit.Framework
    open FsCheck
    open FsCheck.NUnit
    open FsUnit 


    [<Test>]
    //used to enable test discovery in NCrunch
    let ignore_me () = ()

    [<Test>]
    let it_can_grab_a_token_from_dictionary () =
        let expected = " _ "+
                       " _|"+
                       "|_ "+
                       "   "    
        let actual = grabToken 2
        actual |> should equal expected
                        
                           