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
    let it_can_map_a_string_to_a_seq_of_ints () =
        let numbers = "123456789"
        let expected = seq {1..9}
        let actual = toSeqOfInts numbers
        actual |> should equal expected

    [<Test>]
    let it_can_merge_two_tokens () =
        let token1 = "   "+
                     "  |"+
                     "  |"+
                     "   "

        let token2 = " _ "+
                     " _|"+ 
                     "|_ "+
                     "   "
        let expected = "    _ "+
                       "  | _|"+
                       "  ||_ "+
                       "      "
        let actual = mergeToken token1 token2
        actual |> should equal expected


    [<Test>]           
    let it_can_create_a_list_of_displays_from_numbers () =
        let numbers = "123456789\n"
        let expected =
            "    _  _     _  _  _  _  _ " +
            "  | _| _||_||_ |_   ||_||_|" +
            "  ||_  _|  | _||_|  ||_| _|" +
            "                           "
        let actual = scanNumbers numbers dictionary
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
    let it_can_convert_a_string_to_a_list_of_int () =
        let expected = [1;2;3;4;5;6;7;8;9]
        let input = "123456789"
        let actual = input |> fromStringToNums
        actual |> should equal expected

    [<Test>]
    let it_can_calculate_a_valid_checksum () = 
        let expected = "valid"
        let display =
            "    _  _     _  _  _  _  _ " +
            "  | _| _||_||_ |_   ||_||_|" +
            "  ||_  _|  | _||_|  ||_| _|" +
            "                           "
        let actual = validate display dictionary
        actual |> should equal expected

    [<Test>]
    let it_can_calculate_an_invalid_checksum () = 
        let expected = "ERR"
        let display =
            "    _  _     _  _  _  _  _ " +
            "  || | _||_||_ |_   ||_||_|" +
            "  ||_| _|  | _||_|  ||_| _|" +
            "                           "
        let actual = validate display dictionary
        actual |> should equal expected

    [<Test>]
    let an_ureadable_sequence_is_invalid () = 
        let expected = "ILL"
        let display =
            "    _  _     _  _  _  _  _ " +
            "  | _| _||_||_ ||   ||_||_|" +
            "  ||_  _| || _||_| |||_| _|" +
            "                           "
        let actual = validate display dictionary
        actual |> should equal expected