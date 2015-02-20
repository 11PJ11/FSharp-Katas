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
        let actual = matchToken token dictionary
        actual |> should equal expected

    [<Test>]                  
    let unmatched_token_returns_question_mark () = 
        let expected = "?"
        let token = " _ "+
                    " _ "+
                    " _|"+
                    "   "
        let actual = matchToken token dictionary
        actual |> should equal expected

    [<Test>]                  
    let it_can_recognize_a_display () = 
        let expected = "123456789"
        let display =
            "    _  _     _  _  _  _  _ " +
            "  | _| _||_||_ |_   ||_||_|" +
            "  ||_  _|  | _||_|  ||_| _|" +
            "                           "
        let actual = readDisplay display dictionary
        actual |> should equal expected

    [<Test>]                  
    let it_can_recognize_a_display_with_incomplete_values () = 
        let expected = "1234?6?89"
        let display =
            "    _  _     _  _  _  _  _ " +
            "  | _| _||_||| |_  |||_||_|" +
            "  ||_  _|  | |||_|| ||_| _|" +
            "                           "
        let actual = readDisplay display dictionary
        actual |> should equal expected

    [<Test>]                  
    let it_can_recognize_a_display_with_a_different_number_of_digits () = 
        let expected = "8234?6?"
        let display =
            " _  _  _     _  _  _ " +
            "|_| _| _||_||| |_  ||" +
            "|_||_  _|  | |||_|| |" +
            "                     "
        let actual = readDisplay display dictionary
        actual |> should equal expected

    [<Test>]
    let it_can_calculate_the_checksum () = 
        let expected = "valid"
        let display =
            "    _  _     _  _  _  _  _ " +
            "  | _| _||_||| |_  |||_||_|" +
            "  ||_  _|  | |||_|| ||_| _|" +
            "                           "
        let actual = validate display
        actual |> should equal expected