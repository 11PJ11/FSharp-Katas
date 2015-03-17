module Tests

    open KataMinesweeper
    open NUnit.Framework
    open System.Collections.Generic

    [<Test>]
    let test() = ()

    [<Test>]
    let ItCanGetAdjacetnCells1 () =        
        let expected = [ '.' ]
        let grid = array2D [["*";"."]]
        let actual = getAdjacentsOf (1,1) grid
        Assert.AreEqual(actual, expected)

    [<Test>]
    let ItCanGetAdjacetnCells2 () =        
        let expected = ["*";"."]
        let grid = array2D [["*";".";"."]]
        let actual = getAdjacentsOf (2,1) grid
        Assert.AreEqual(actual, expected)