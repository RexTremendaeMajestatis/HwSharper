namespace HwProj2

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html

[<JavaScript>]
module CommonClient =
    open RendersClient
    open ModelsClient

    let CheckTasks() =
        (*Создание списка для выведения на экран*)
        let m = CreateTasksModel()
        div [] [ToCheckList m]

    let CreateTask() = 
        let taskNameInput = Var.Create ""
        let requirementInput = Var.Create ""
        let taskNameField = Doc.Input [] taskNameInput
        let requirement = Doc.InputArea [] requirementInput
        div [] [
            div [] [taskNameField]
            div [] [requirement]
            button [] [text "Push"]
        ]

    let AppointTask() = 
        let tm = CreateRealTasksModel()
        let cm = CreateCoursesModel()
        div [] [
            div [] [TasksToAppoint tm]
            div [] [CoursesToAppoint cm]
            (*Нужно как-то смотреть какое задание назначено*)
            div [] [button [] [text "Push"]]
        ]

    let CreateCourse() = 
        let courseNameInput = Var.Create ""
        let courseYearInput = Var.Create ""
        let courseNameField = Doc.Input [] courseNameInput
        let courseYearField = Doc.Input [] courseYearInput
        div [] [
            div [] [courseNameField]
            div [] [courseYearField]
            div [] [button [] [text "Create"]]
        ] 

    let ManageCourse() =
        let courseName = "test"
        let pm = CreatePeopleModel()
        let inviteInput = Var.Create ""
        let inviteField = Doc.Input [] inviteInput
        div [] [
            h4 [] [text courseName]
            div [] [CoursePeopleList pm]
            div [] [button [] [text "Complete"]]
            div [] [inviteField]
            div [] [button [] [text "Invite"]]
        ]

    let DoTasks() = 
        let m = CreateTasksModel()
        div [] [ToDoList m]

    (*let CoursesOverview() =
        let coursesList = Server.GetAllOngoingCourses()
        div [] [CoursesList coursesList]*)

    let FollowCourse() = 
        let m = CreatePeopleModel()
        div [] [CoursematesList m]
