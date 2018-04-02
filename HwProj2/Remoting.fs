namespace HwProj2

open WebSharper

type LoginData = { 
              User : string 
              Password : string 
            }

type RegisterData = 
    {
        UserId: string
        Password: string
        Fullname: string
        Email: string
    }

module Server =
    open DataStorage.UserRegistry

    [<Rpc>]
    let RegisterUser (regData : RegisterData) =
        let ctx = Web.Remoting.GetContext()
        async {
            create "user_accounts.db" 
                   regData.UserId 
                   regData.Password 
                   regData.Fullname 
                   regData.Email |> ignore
            return true 
        } |> Async.Ignore

    [<Rpc>]
    let LoginUser (userData : LoginData) =
        let ctx = Web.Remoting.GetContext()
        let us = get "user_accounts.db" userData.User
        if (us <> None && userData.Password = us.Value.Password)
        then ctx.UserSession.LoginUser(userData.User, persistent = true) |> Async.Ignore
        else async.Return()

    [<Rpc>]
    let LogoutUser () =
        let ctx = Web.Remoting.GetContext()
        ctx.UserSession.Logout()
