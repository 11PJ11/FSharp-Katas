module bankOcr

    open System

    let dictionary =
        " _     _  _     _  _  _  _  _ " +
        "| |  | _| _||_||_ |_   ||_||_|" +
        "|_|  ||_  _|  | _||_|  ||_| _|" +
        "                              "

    let tokenWidth = 3
    let tokenHeight = 4

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

    let validate display = "invalid"