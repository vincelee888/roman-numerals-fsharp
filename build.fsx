// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"
open Fake

RestorePackages()

let buildDir = "./build/"
let testDir = "./test/"

Target "Clean" (fun _ ->
    CleanDir buildDir
    CleanDir testDir
)

Target "BuildApp" (fun _ ->
    !! "src/app/**/*.fsproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "AppBuild-Output: "
)

Target "BuildTest" (fun _ ->
    !! "src/tests/**/*.fsproj"
        |> MSBuildDebug testDir "Build"
        |> Log "TestBuild-Output: "
)

Target "Test" (fun _ ->
    !! (testDir + "/tests.dll")
        |> NUnitParallel (fun p ->
            {p with
                DisableShadowCopy = true;
                OutputFile = testDir + "TestResults.xml" })
)

Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

"Clean"
    ==> "BuildApp"
    ==> "BuildTest"
    ==> "Test"
    ==> "Default"

// start build
RunTargetOrDefault "Default"