module bankOcr

let dictionary =
    " _     _  _     _  _  _  _  _ " +
    "| |  | _| _||_||_ |_   ||_||_|" +
    "|_|  ||_  _|  | _||_|  ||_| _|" +
    "                              "

let grabToken i =
    let xStart = i*3
    let xEnd = xStart + 2
    let lineLength = dictionary.Length / 4
    seq {for line in 0..3        
        do yield dictionary.[(xStart + lineLength * line)..(xEnd + lineLength * line)]}
    |> Seq.concat