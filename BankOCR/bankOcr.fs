module bankOcr

open System

let dictionary =
    " _     _  _     _  _  _  _  _ " +
    "| |  | _| _||_||_ |_   ||_||_|" +
    "|_|  ||_  _|  | _||_|  ||_| _|" +
    "                              "

let grabToken i (dict:string) :string=
    let xStart = i*3
    let xEnd = xStart + 2
    let lineLength = dict.Length / 4
    let token = 
        seq {for row in 0..3        
                do yield dict.[(xStart + lineLength * row)..(xEnd + lineLength * row)]}
        
    token |> String.Concat

let matchToken (token:string) (dict:string) = 
    let indexes = [0..9]
    let found = 
        indexes
        |> List.tryFind (fun i -> token.Equals(grabToken i dict))
    if (found.IsSome)
        then found.Value.ToString()
    else "?"

let readDisplay (display:string) (dict:string) =
    let digits = display.Length / 12 - 1
    [0..digits]
    |> List.map (fun i -> matchToken (grabToken i display) dict)
    |> String.Concat