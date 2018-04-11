namespace HwProj2

open WebSharper
open WebSharper.Sitelets
open WebSharper.UI
open WebSharper.UI.Server

type EndPoint =
    | [<EndPoint "/">] Start
    | [<EndPoint "/about">] About
    | [<EndPoint "/profile">] Profile
    | [<EndPoint "/courses">] Courses

module Templating =
    open WebSharper.UI.Html

    type MainTemplate = Templating.Template<"Main.html">

    let MenuBarAnon (ctx: Context<EndPoint>) endpoint : Doc list =
        let ( => ) txt act =
             li [if endpoint = act then yield attr.``class`` "active"] [
                a [attr.href (ctx.Link act)] [text txt]
             ]
        [
            "Start" => EndPoint.Start
            "About" => EndPoint.About
        ]
      
    let MenuBarLogged (ctx: Context<EndPoint>) endpoint : Doc list =
            [li [if endpoint = Profile then yield attr.``class`` "active"][a [attr.href (ctx.Link Profile)] [text "Profile"]];
             li [if endpoint = About then yield attr.``class`` "active"] [a [attr.href (ctx.Link About)] [text "About"]];
             li [if endpoint = Courses then yield attr.``class`` "active"] [a [attr.href (ctx.Link Courses)] [text "Courses"]];
             li [on.click (fun _ _ -> Client.LogOutUser())][a [attr.href "#"] [text "Log Out"]]
            ]                    

    let MenuBar (ctx: Context<EndPoint>) endpoint =
        let getUser = ctx.UserSession.GetLoggedInUser() |> Async.RunSynchronously
        match getUser with
        | Some (username) -> MenuBarLogged ctx endpoint
        | None -> MenuBarAnon ctx endpoint

    let Main (ctx : Context<EndPoint>) action (title: string) (body: Doc list) =
        Content.Page(
            MainTemplate()
                .Title(title)
                .MenuBar(MenuBar ctx action)
                .Body(body)
                .Doc()
        )

module Site =
    open WebSharper.UI.Html
   
    let StartPage (ctx : Context<_>) =
        async {
            let! loggedIn = ctx.UserSession.GetLoggedInUser()
            let content =
                match loggedIn with
                | Some _ ->
                    div [] [
                            h1 [] [text "Work in progress"]
                    ]
                | None ->
                    div [] [
                        div[attr.style "float:left; width:400px"][ h1[][text("Log In")]
                                                                   client <@ Client.AnonUser() @>]
                        div[attr.style "float:right; width:400px"][ h1[][text("Register")]
                                                                    client <@ Client.RegUser() @>]
                    ]
            return! Templating.Main ctx EndPoint.Start "Start" [content]
        }

    let AboutPage ctx =
        Templating.Main ctx EndPoint.About "About" [
            h1 [] [text "About"]
            p [] [text "I'm just trying to deal with my course work:с"]
        ]

    let ProfilePage ctx =
        Templating.Main ctx EndPoint.Profile "Profile" [
            h1 [] [text "Profile"]
            p [] [text "*Profile information*"]
        ]

    let CoursesPage ctx =
        Templating.Main ctx EndPoint.Courses "Courses" [
            h1 [] [text "Courses"]
            p [] [text "*Courses list*"]
        ]


    [<Website>]
    let Main =
        Application.MultiPage (fun ctx endpoint ->
            match endpoint with
            | EndPoint.Start -> StartPage ctx
            | EndPoint.About -> AboutPage ctx
            | EndPoint.Profile -> ProfilePage ctx
            | EndPoint.Courses -> CoursesPage ctx
        )

