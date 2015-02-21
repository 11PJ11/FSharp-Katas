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
        else "invalid"

    let validate (display:string) (dict:string) = 
        let code = readDisplay display dict
        if(code.Contains("?"))
        then "invalid"
        else 
            let numbers = code |> fromStringToNums
            validateChecksum numbers
