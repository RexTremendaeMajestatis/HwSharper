namespace HwProj2

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html
open WebSharper.Forms
open WebSharper.Forms.Bootstrap

[<JavaScript>]
module RegClient =

    let LogOutUser () =
        async{ do! Server.LogoutUser()
               return JS.Window.Location.Href <- "/"
             } |> Async.Start


    let AnonUser () =
        Form.Return(fun user pass -> user, pass)
        <*> (Form.Yield ""
             |> Validation.IsNotEmpty "Enter an username")
        <*> (Form.Yield ""
             |> Validation.IsNotEmpty "Enter a password")
        |> Form.WithSubmit
        |> Form.Run (fun (user, pass) ->
            async {
                do! Server.LoginUser user pass
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
        Form.Return(fun login pass name email isTeacher -> email, pass, name, isTeacher)
        <*> (Form.Yield "" 
             |> Validation.IsNotEmpty "Enter an username")
        <*> (Form.Yield ""
             |> Validation.IsNotEmpty "Enter a password")
        <*> (Form.Yield ""
             |> Validation.IsNotEmpty "Enter a full name")
        <*> (Form.Yield ""
             |> Validation.IsNotEmpty "Enter an email")
        <*> Form.Yield false
        |> Form.WithSubmit
        |> Form.Run (fun (email, pass, name, isTeacher) ->
            async {
                do! Server.RegisterUser email pass name isTeacher
                return JS.Window.Location.Reload()
            } |> Async.Start
        )
        |> Form.Render (fun user pass name email isTeacher submit ->
            form [] [
                Controls.Simple.InputWithError "Username" user submit.View
                Controls.Simple.InputPasswordWithError "Password" pass submit.View
                Controls.Simple.InputWithError "Name" name submit.View
                Controls.Simple.InputWithError "Email" email submit.View
                Controls.Radio "Teacher" [attr.``class`` "radio"; attr.``checked`` "checked"] (isTeacher, [], [attr.``type`` "radio"; attr.name "optradio"]) 
                Controls.Radio "Student" [attr.``class`` "radio"] (Var.Create(not (isTeacher.Get())), [], [attr.``type`` "radio"; attr.name "optradio"])
                Controls.Button "Register" [attr.``class`` "btn btn-primary"] submit.Trigger
                Controls.ShowErrors [attr.style "margin-top:1em;"] submit.View
            ])
