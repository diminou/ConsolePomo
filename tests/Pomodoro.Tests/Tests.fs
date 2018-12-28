module Tests

open Expecto
open Pomodoro

[<Tests>]
let tests =
    testList "samples" 
        [ testCase "Say nothing" <| fun _ -> 
              let subject = Say.nothing()
              Expect.equal subject () "Not an absolute unit"
          testCase "Say hello all" <| fun _ -> 
              let subject = Say.hello "all"
              Expect.equal subject "Hello all" "You didn't say hello" 
          testCase "Check minute constant" <| fun _ ->
              Expect.equal Time.aMinute (60 * Time.aSecond) "A minute is not 60 seconds"
          testCase "Check int 25 constant" <| fun _ ->
              Expect.equal Time.interval25 (25 * Time.aMinute) "25 minutes is not default"
          testCase "Check int 5 constant" <| fun _ ->
              Expect.equal Time.interval5 (5 * Time.aMinute) "5 minutes is not default"
          testCase "Check default inter-sound" <| fun _ ->
              Expect.equal Time.interSound 500 "500ms is not default inter-sound"]
