//define includes
#I @"packages/FAKE.3.21.0/tools"
#r @"FakeLib.dll"

open Fake
open Fake.FileUtils

//constants
let buildDir = "/build"
let bankOcrDir = "./BankOCR"
let mineSweeperDir = "./KataMinesweeper"
let buildBankOcrDir = bankOcrDir + buildDir
let buildKataMineSweeperDir = mineSweeperDir + buildDir

let buildPaths = [
        (bankOcrDir, buildBankOcrDir);
        (mineSweeperDir, buildKataMineSweeperDir)
    ]

//define targets
Target "Clean" 
    (fun _ -> 
        let outputDirs = (buildPaths 
                            |> List.map (fun paths -> snd paths))
        outputDirs 
        |> CleanDirs )

Target "Build"
    (fun _ ->
        buildPaths
        |> List.map (fun paths ->
            let (projDir, outputDir) = paths
            !! (projDir + "/*.fsproj")        
            |> MSBuildRelease outputDir "Build"
            |> Log "AppBuild-Output: ")
        |> ignore)

Target "BuildTest"
    (fun _ ->
        buildPaths
        |> List.map (fun paths ->
            let (projDir, outputDir) = paths
            !! (projDir + "/*.fsproj")
            |> MSBuildDebug outputDir "Build"
            |> Log "Test build: ")
        |> ignore)

Target "Test" 
    (fun _ ->
        buildPaths
        |> List.map (fun paths ->
            let (projDir, outputDir) = paths
            //nunit
            !! (outputDir + "/*.exe")
            |> NUnitParallel
                (fun p ->
                    { p with
                        DisableShadowCopy = true;
                        ToolPath = "./packages/NUnit.Runners.2.6.3/tools/";
                        OutputFile = outputDir + "/TestResults.xml" 
                    }))
        |> ignore)
        
Target "Default" (fun _ ->
    trace "STARTING BUILD SCRIPT")

//define dependencies
"Clean"
    ==> "Build"
    ==> "BuildTest"
    ==> "Test"
    ==> "Default"

// start build
RunTargetOrDefault "Default"