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
        IsTeacher: bool
    }

module Server =
    open DataManager

    [<Rpc>]
    let RegisterUser (regData : RegisterData) =
        let ctx = Web.Remoting.GetContext()
        let reg = UserRegistry()
        reg.Create(
                   regData.UserId,
                   regData.Password, 
                   regData.Fullname, 
                   regData.Email,
                   regData.IsTeacher)
        ctx.UserSession.LoginUser(regData.UserId, persistent = true) |> Async.Ignore

    [<Rpc>]
    let LoginUser (userData : LoginData) =
        let ctx = Web.Remoting.GetContext()
        let log = UserRegistry()
        let us = log.Search(userData.User, userData.Password)
        if us
        then ctx.UserSession.LoginUser(userData.User, persistent = true) |> Async.Ignore
        else async.Return()

    [<Rpc>]
    let LogoutUser () =
        let ctx = Web.Remoting.GetContext()
        ctx.UserSession.Logout()
