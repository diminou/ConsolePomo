namespace Pomodoro

module Say =
    let nothing name = name |> ignore
    let hello name = sprintf "Hello %s" name

module Time =
    let aSecond = 1000
    let aMinute = 60 * aSecond
    let interval25 = 25 * aMinute
    let interval5 = 5 * aMinute
    let interSound = 500
    let sequenceLength = 5

module Delay =
    let waitInterval (i : int) : unit = System.Threading.Thread.Sleep i
    
    let waitAndPrint (i : int) : unit =
        waitInterval i
        printfn "An interval of %i ms has ended" i

module Sounds =
    open FSharpPlus
    open System
    
    let playAlert() = Console.Beep()
    
    let playSequence (n : int) =
        [ 1..n ]
        |> List.map (fun _ -> playAlert)
        |> List.intersperse (fun _ -> Delay.waitInterval Time.interSound)
        |> List.iter (fun f -> f())

module Glue =
    open Delay
    open Sounds
    open System
    open System.Threading
    
    let rec pomo() =
        waitAndPrint Time.interval25
        playSequence Time.sequenceLength
        waitAndPrint Time.interval5
        playSequence Time.sequenceLength
        pomo()
    
    let pomoLoop = async { pomo() }
    
    let listenPomo() =
        let cancellationSource = new CancellationTokenSource()
        // start the task, but this time pass in a cancellation token
        Async.Start(pomoLoop, cancellationSource.Token)
        printfn "Pomodoro timer started"
        // wait a bit
        match Console.Read() with
        | _ -> 
            cancellationSource.Cancel()
            exit 0
