namespace SlackClient

open System
open System.Threading.Tasks
open System.Collections.Specialized
open Newtonsoft.Json
open System.Net

type Payload =
    { Channel : string
      Username : string
      Text : string }

type SlackClient(urlAccessWithToken : string) =
    let uri = Uri(urlAccessWithToken)

    member __.PostMessageAsync(payload : Payload) : Task =
        let payloadJson = JsonConvert.SerializeObject(payload)
        use client = new WebClient()
        let data = NameValueCollection()
        data.["payload"] <- payloadJson
        upcast client.UploadValuesTaskAsync(uri, "POST", data)

    member x.PostMessageAsync(text, username, channel) =
        x.PostMessageAsync({ Text = text; Username = username; Channel = channel })