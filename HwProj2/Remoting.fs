namespace HwProj2

open WebSharper
open DataManager.Models

module Server =
    open DataManager
    open ModelsClient

    [<Rpc>]
    let RegisterUser email password fullname role =
        let ctx = Web.Remoting.GetContext()
        let success = AccountManager.CreateAccount (email,
                                                    password, 
                                                    fullname,
                                                    role)
        if success
        then 
            ctx.UserSession.LoginUser(email, persistent = true) |> Async.Ignore
        else async.Return()

    [<Rpc>]
    let LoginUser email password =
        let ctx = Web.Remoting.GetContext()
        let isValid = AccountManager.ValidateUser(email, password)
        if isValid
        then 
            ctx.UserSession.LoginUser (email, persistent = true) |> Async.Ignore
        else 
            async.Return()

    [<Rpc>]
    let LogoutUser () =
        let ctx = Web.Remoting.GetContext()
        ctx.UserSession.Logout()

    [<Rpc>]
    let GetAllOngoingCourses () =
        let courses = OngoingCoursesManager.GetAllOngoingCourses()
        courses

    [<Rpc>]
    let GetTitleCourseById (id: int) =
        let courses = CoursesBankManager.GetAllCourses()
        let result = Seq.find (fun (course: Course) -> course.Id = id) courses 
        result.Title
