namespace SlackClient

open System.Threading.Tasks

type Payload =
    { Channel : string
      Username : string
      Text : string }

type ISlackClient =
    abstract PostMessageAsync : Payload -> Task
    abstract PostMessage : Payload -> unit
    abstract PostMessageAsync : string * string * string -> Task
    abstract PostMessage : string * string * string -> unit