namespace HwProj2

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html
open WebSharper.Forms
open WebSharper.Forms.Bootstrap

[<JavaScript>]
module Client =

    let LoggedInUser () =
        div [] [
                p [] [text "Click to log out"]
                button [on.click (fun _ _ ->
                    async{
                        do! Server.LogoutUser()
                        return JS.Window.Location.Reload()
                    } |> Async.Start
                    )
            ] [text "log out"]
        ]

    let AnonUser () =
        Form.Return(fun user pass -> { User = user; Password = pass })
        <*> (Form.Yield ""
             |> Validation.IsNotEmpty "Enter an username")
        <*> (Form.Yield ""
             |> Validation.IsNotEmpty "Enter a password")
        |> Form.WithSubmit
        |> Form.Run (fun userData ->
            async {
                do! Server.LoginUser userData
                return JS.Window.Location.Reload()
            } |> Async.Start
        )
        |> Form.Render (fun user pass submit ->
            form [] [
                Controls.Simple.InputWithError "Username" user submit.View
                Controls.Simple.InputPasswordWithError "Password" pass submit.View
                Controls.Button "Log in" [attr.``class`` "btn btn-primary"] submit.Trigger
                Controls.ShowErrors [attr.style "margin-top:1em;"]submit.View
            ])

    let RegUser () =
        Form.Return(fun login name email pass -> { UserId = login; Password = pass; Fullname = name; Email = email })
        <*> (Form.Yield ""
             |> Validation.IsNotEmpty "Enter an username")
        <*> (Form.Yield ""
             |> Validation.IsNotEmpty "Enter a password")
        <*> (Form.Yield ""
             |> Validation.IsNotEmpty "Enter a full name")
        <*> (Form.Yield ""
             |> Validation.IsNotEmpty "Enter a email")
        |> Form.WithSubmit
        |> Form.Run (fun regData ->
            async {
                do! Server.RegisterUser regData
                return JS.Window.Location.Reload()
            } |> Async.Start
        )
        |> Form.Render (fun user pass name email submit ->
            form [] [
                Controls.Simple.InputWithError "Username" user submit.View
                Controls.Simple.InputPasswordWithError "Password" pass submit.View
                Controls.Simple.InputWithError "Name" name submit.View
                Controls.Simple.InputWithError "Email" email submit.View
                Controls.Button "Sign Up" [attr.``class`` "btn btn-primary"] submit.Trigger
                Controls.ShowErrors [attr.style "margin-top:1em;"]submit.View
            ])
