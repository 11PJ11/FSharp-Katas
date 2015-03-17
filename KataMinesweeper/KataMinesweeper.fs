module KataMinesweeper

    let flatten (A:'a[,]) = A |> Seq.cast<'a>

    let getAdjacentsOf (cell:int*int) (grid:string[,]) :string [] =
        let minCol = max 0 (snd cell + 1)
        let maxCol = min (grid.[*,*].Length) (snd cell + 1)
        flatten grid.[0..0, minCol..maxCol]        
        |> Seq.toArray