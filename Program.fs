// Learn more about F# at http://fsharp.org

open System
open Microsoft.CognitiveServices.Speech
open Microsoft.Extensions.Configuration //.EnvironmentVariables

[<EntryPoint>]
let main argv =
    let config = ConfigurationBuilder()
                    .AddEnvironmentVariables("TTS_")
                    .Build()
    printfn "---"
    config.GetChildren() |> Seq.iter (fun x -> printfn "%s : %s" x.Key x.Value)
    printfn "---"
    0 // return an integer exit code
