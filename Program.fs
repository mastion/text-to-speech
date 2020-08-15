open System
open Microsoft.CognitiveServices.Speech
open Microsoft.Extensions.Configuration //.EnvironmentVariables

[<EntryPoint>]
let main argv =
    async {
        let config = ConfigurationBuilder()
                        .AddEnvironmentVariables("TTS_")
                        .Build()
        printfn "---"
        config.GetChildren() |> Seq.iter (fun x -> printfn "%s : \"%s\"" x.Key x.Value)
        printfn "---"
        printfn "___"
        printfn "subkey: %s" config.["SubscriptionKey"]
        printfn "serreg: %s" config.["ServiceRegion"]
        printfn "___"
        let fileName ="helloworld.wav"
        let speechConfig = SpeechConfig.FromSubscription(config.["SubscriptionKey"], config.["ServiceRegion"])
        
        use fileOutput = Audio.AudioConfig.FromWavFileOutput fileName
        use synthesizer = new SpeechSynthesizer(speechConfig, fileOutput)

        use! result = synthesizer.SpeakTextAsync("Hello") |> Async.AwaitTask

        match result.Reason with
        | ResultReason.SynthesizingAudioCompleted -> printfn "done"
        | ResultReason.Canceled ->
            let cancel = SpeechSynthesisCancellationDetails.FromResult(result)
            printfn "CANCELED: Reason=%s" (cancel.Reason.ToString())
            if cancel.Reason = CancellationReason.Error then
                printfn "CANCELED: ErrorCode=%s" (cancel.ErrorCode.ToString())
                printfn "CANCELED: ErrorDetails=%s" (cancel.ErrorDetails.ToString())
        | _ -> printfn "ERROR: Result Reason=%s" (result.Reason.ToString())

    } 
    |> Async.RunSynchronously
    
    0 // return an integer exit code
