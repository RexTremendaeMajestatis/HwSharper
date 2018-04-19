namespace HwProj2

open WebSharper
open DataManager.Models
type LoginData = { 
              Email : string
              Password : string 
            }

type RegisterData = 
    {
        Role: bool
        Email: string
        Password: string
        Fullname: string
    }

module Server =
    open DataManager

    [<Rpc>]
    let RegisterUser (regData : RegisterData) =
        let ctx = Web.Remoting.GetContext()
        let success = AccountManager.CreateAccount (regData.Email,
                                                    regData.Password, 
                                                    regData.Fullname,
                                                    regData.Role)
        if success
        then 
            ctx.UserSession.LoginUser(regData.Email, persistent = true) |> Async.Ignore
        else async.Return()

    [<Rpc>]
    let LoginUser (userData : LoginData) =
        let ctx = Web.Remoting.GetContext()
        let isValid = AccountManager.ValidateUser(userData.Email, userData.Password)
        if isValid
        then 
            ctx.UserSession.LoginUser (userData.Email, persistent = true) |> Async.Ignore
        else 
            async.Return()

    [<Rpc>]
    let LogoutUser () =
        let ctx = Web.Remoting.GetContext()
        ctx.UserSession.Logout()
