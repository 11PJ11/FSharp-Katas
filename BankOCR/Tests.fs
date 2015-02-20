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
        let actual = grabToken 2 dictionary
        actual |> should equal expected
                        
    [<Test>]                  
    let it_can_match_a_token () = 
        let expected = "5"
        let token = " _ "+
                    "|_ "+
                    " _|"+
                    "   "
        let actual = matchToken token
        actual |> should equal expected

    [<Test>]                  
    let unmatched_token_returns_question_mark () = 
        let expected = "?"
        let token = " _ "+
                    " _ "+
                    " _|"+
                    "   "
        let actual = matchToken token
        actual |> should equal expected