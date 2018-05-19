namespace HwProj2

open WebSharper
open WebSharper.Sitelets
open WebSharper.UI
open WebSharper.UI.Server

[<JavaScript>]
type Course = 
    { CourseId: int; CourseData: string}

type EndPoint =
    | [<EndPoint "/">] Start
    | [<EndPoint "/about">] About
    | [<EndPoint "/profile">] Profile
    | [<EndPoint "GET /course">] GetCourse of int
    //| [<EndPoint "PUT /course"; Json "course">] UpdateCourse of course: Course
    | [<EndPoint "POST /course"; Json "data">] CreateCourse of data: string
    | [<EndPoint "DELETE /course"; Json "id">] DeleteCourse of id: int
    | [<EndPoint "/edit">] EditCourse of int
    | [<EndPoint "/courses">] ListOfCourses
    

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
             li [if endpoint = ListOfCourses then yield attr.``class`` "active"] [a [attr.href (ctx.Link ListOfCourses)] [text "Courses"]];
             li [on.click (fun _ _ -> RegClient.LogOutUser())][a [attr.href "#"] [text "Log Out"]]
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
                                                                   client <@ RegClient.AnonUser() @>]
                        div[attr.style "float:right; width:400px"][ h1[][text("Register")]
                                                                    client <@ RegClient.RegUser() @>]
                    ]
            return! Templating.Main ctx EndPoint.Start "Start" [content]
        }
    // get all ongoing courses for teacher
    let AllCoursesForTeacher ctx =
        Templating.Main ctx EndPoint.ListOfCourses "Current courses" [
            h1 [] [text "Courses"]
            table [attr.``class`` "table table-striped table-hover"] [
                thead [] [
                    td [] [text "Course #"]
                    td [] [text "Teacher"]
                    td [] [text "Group number"]
                ]
                tbody []
                    (OnGoingCourseID.GetListOfCourses
                    |> Seq.map (fun (Course.Id, Course.TeacherFullName, Course.GroupId) ->
                        tr [] [
                            td [] [sprintf "#%d" Course.Id => EndPoint.GetCourse Course.Id]
                            td [] [text Course.TeacherFullName]
                            td [] [text (string Course.GroupId)]
                            td [] [
                                client <@ Client.DeleteCourse Course.Id @>
                                text " | "
                                "EDIT" => EndPoint.EditCourse CourseId                            ]
                        ] :> Doc
                    ))
            ]
        ]
    // get all ongoing courses for student
    // копипаст суть ересь надо подумать как объединить логику для student и teacher
    let AllCoursesForStudent ctx =
        Templating.Main ctx EndPoint.ListOfCourses "Current courses" [
            h1 [] [text "Courses"]
            table [attr.``class`` "table table-striped table-hover"] [
                thead [] [
                    td [] [text "Course #"]
                    td [] [text "Teacher"]
                    td [] [text "Group number"]
                ]
                tbody []
                    (OnGoingCourseID.GetListOfCourses
                    |> Seq.map (fun (Course.Id, Course.TeacherFullName, Course.GroupId) ->
                        tr [] [
                            td [] [sprintf "#%d" Course.Id => EndPoint.GetCourse Course.Id]
                            td [] [text Course.TeacherFullName]
                            td [] [text (string Course.GroupId)]
                            td [] [
                                client <@ Client.DeleteCourse Course.Id @>
                                text " | "
                                "EDIT" => EndPoint.EditCourse CourseId                            ]
                        ] :> Doc
                    ))
            ]
        ]   

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

    [<Website>]
    let Main =
        Application.MultiPage (fun ctx endpoint ->
            match endpoint with
            | EndPoint.Start -> StartPage ctx
            | EndPoint.About -> AboutPage ctx
            | EndPoint.Profile -> ProfilePage ctx
            | EndPoint.EditCourse i ->
                MyCourses.FindById i
                |> CreateOrEditOrderPage ctx
            | EndPoint.CreateCourse course ->
                MyCourses.Save (MyCourses.GetId()) course
                Content.Text "Course created successfully."
            | EndPoint.DeleteCourse id ->
                MyCourses.Delete id
                Content.Text "Course deleted successfully."
            | EndPoint.GetCourse id ->
                GetCourse id
            | EndPoint.ListOfCourses -> CoursesPage ctx
        )

