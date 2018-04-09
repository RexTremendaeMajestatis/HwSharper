namespace HwProj2

open WebSharper
open WebSharper.Sitelets
open WebSharper.UI
open WebSharper.UI.Server

type EndPoint =
    | [<EndPoint "/">] Home
    | [<EndPoint "/about">] About

module Templating =
    open WebSharper.UI.Html

    type MainTemplate = Templating.Template<"Main.html">

    // Compute a menubar where the menu item for the given endpoint is active
    let MenuBar (ctx: Context<EndPoint>) endpoint : Doc list =
        let ( => ) txt act =
             li [if endpoint = act then yield attr.``class`` "active"] [
                a [attr.href (ctx.Link act)] [text txt]
             ]
        [
            "Home" => EndPoint.Home
            "About" => EndPoint.About
        ]

    let Main ctx action (title: string) (body: Doc list) =
        Content.Page(
            MainTemplate()
                .Title(title)
                .MenuBar(MenuBar ctx action)
                .Body(body)
                .Doc()
        )

module Site =
    open WebSharper.UI.Html
   
    let HomePage (ctx : Context<_>) =
        async {
            let! loggedIn = ctx.UserSession.GetLoggedInUser()
            let content =
                match loggedIn with
                | Some username ->
                    div [] [
                            h1 [] [text ("Welcome, " + username)]
                            client <@ Client.LoggedInUser() @>
                    ]
                | None ->
                    div [] [
                        div[attr.style "float:left; width:400px"][ h1[][text("Log In")]
                                                                   client <@ Client.AnonUser() @>]
                        div[attr.style "float:right; width:400px"][ h1[][text("Register")]
                                                                    client <@ Client.RegUser() @>]
                    ]
            return! Templating.Main ctx EndPoint.Home "Home" [content]
        }

    let AboutPage ctx =
        Templating.Main ctx EndPoint.About "About" [
            h1 [] [text "About"]
            p [] [text "I'm just trying to deal with my course work:с"]
        ]


    [<Website>]
    let Main =
        Application.MultiPage (fun ctx endpoint ->
            match endpoint with
            | EndPoint.Home -> HomePage ctx
            | EndPoint.About -> AboutPage ctx
        )

