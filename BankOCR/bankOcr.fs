module bankOcr
    open System

    let tokenWidth = 3
    let tokenHeight = 4
    let dictionary =
        " _     _  _     _  _  _  _  _ " +
        "| |  | _| _||_||_ |_   ||_||_|" +
        "|_|  ||_  _|  | _||_|  ||_| _|" +
        "                              "

    let grabToken nth (dict:string) :string=        
        let colStart = nth * tokenWidth
        let colEnd = (nth + 1) * tokenWidth - 1
        let rows = [0..tokenHeight - 1]
        let rowLength = dict.Length / rows.Length        
        rows
        |> List.map (fun row -> dict.[(colStart + rowLength * row)..(colEnd + rowLength * row)])
        |> String.Concat

    let toSeqOfInts (numbers:string) :int seq=
        numbers
        |> Seq.map (fun char -> Int32.Parse(char.ToString()))

    let mergeToken (tokens:string) (token:string) :string =
        let tokensWidth = tokens.Length / tokenHeight
        [0..tokenHeight-1]
        |> List.map (fun row -> 
            String.Concat(
                tokens.[row*tokensWidth..row*tokensWidth + (tokensWidth - 1)],
                token.[row*tokenWidth ..row*tokenWidth + (tokenWidth - 1)]
            ))
        |> String.Concat

    let scanNumbers (numbers: string) (dict: string) :string =
        numbers.Split('\n')
        |> Array.filter (fun line -> not(String.IsNullOrEmpty(line)))
        |> Array.map (fun line -> 
                          let tokens = line
                                     |> toSeqOfInts
                                     |> Seq.map (fun nth -> grabToken nth dict)                                     
                          tokens
                          |> Seq.reduce (fun accTokens token -> mergeToken accTokens token ))
        |> String.Concat

    let matchToken (token:string) (dict:string) = 
        let positions = [0..9]
        let found = 
            positions
            |> List.tryFind (fun nth -> token.Equals(grabToken nth dict))
        if (found.IsSome)
            then found.Value.ToString()
        else "?"

    let readDisplay (display:string) (dict:string) =
        let digits = display.Length / (tokenWidth * tokenHeight) - 1
        [0..digits]
        |> List.map (fun nth -> matchToken (grabToken nth display) dict)
        |> String.Concat

    let fromStringToNums (code:string) :int list=
        code 
        |> Seq.map (fun c -> Int32.Parse(c.ToString()))
        |> Seq.toList

    let validateChecksum (numbers:int list) = 
        let checksum = 
            [1..numbers.Length]
            |> List.zip (numbers |> List.rev)
            |> List.map (fun (index, number)-> index*number)
            |> List.sum 
        if(checksum % 11 = 0)
        then "valid"
        else "ERR"

    let validate (display:string) (dict:string) = 
        let code = readDisplay display dict
        if(code.Contains("?"))
        then "ILL"
        else 
            let numbers = code |> fromStringToNums
            validateChecksum numbers
