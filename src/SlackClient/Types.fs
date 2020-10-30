namespace SlackClient

open System
open System.Threading.Tasks
open System.Collections.Specialized
open Newtonsoft.Json
open System.Net

type SlackClient(urlAccessWithToken : string) =
    let uri = Uri(urlAccessWithToken)

    member __.PostMessageAsync(payload : Payload) : Task =
        let payloadJson = JsonConvert.SerializeObject(payload)
        use client = new WebClient()
        let data = NameValueCollection()
        data.["payload"] <- payloadJson
        upcast client.UploadValuesTaskAsync(uri, "POST", data)

    member x.PostMessage(payload : Payload) =
        x.PostMessageAsync(payload).GetAwaiter().GetResult()

    member x.PostMessageAsync(text, username, channel) =
        x.PostMessageAsync({ Text = text; Username = username; Channel = channel })

    member x.PostMessage(text, username, channel) =
        x.PostMessageAsync(text, username, channel).GetAwaiter().GetResult()

    interface ISlackClient with
        member x.PostMessageAsync(payload) = x.PostMessageAsync(payload)
        member x.PostMessage(payload) = x.PostMessage(payload)
        member x.PostMessageAsync(text, username, channel) = x.PostMessageAsync(text, username, channel)
        member x.PostMessage(text, username, channel) = x.PostMessage(text, username, channel)