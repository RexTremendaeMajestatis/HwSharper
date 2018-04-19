namespace HwProj2

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html

[<JavaScript>]
module TeacherClient =

    type RealTaskItem = 
        {
            Name: string
            Requirement: string
            Key: Key
        }

        static member Create name requirement = 
            { Key = Key.Fresh(); Name = name; Requirement = requirement }

    type TaskItem = 
        {
            Name: string
            Info: string
            Source: string
            IsAccepted: Var<bool>
            IsChangesRequired: Var<bool>
            Date: string
            Course: string
            Key: Key
        }

        static member Create name info source date course = 
            { Key = Key.Fresh(); IsAccepted = Var.Create false; IsChangesRequired = Var.Create false; Name = name; Info = info; Source = source; Date = date; Course = course }

    type CourseItem = 
        {
            Name: string
            Year: int
            Key: Key
        }

        static member Create name year = 
            { Key = Key.Fresh(); Name = name; Year = year }

    type PersonItem = 
        {
            Name: string
            Email: string
            GitHub: string
            Key: Key
        }

        static member Create name email github = 
            { Key = Key.Fresh(); Name = name; Email = email; GitHub = github }

    type RealTasksModel = 
        {
            RealTasks: ListModel<Key, RealTaskItem>
        }

    type TasksModel = 
        {
            Tasks: ListModel<Key, TaskItem>
        }

    type CoursesModel = 
        {
            Courses: ListModel<Key, CourseItem>
        }

    type PeopleModel = 
        {
            People: ListModel<Key, PersonItem>
        }

    let CreateRealTasksModel() = 
        { RealTasks = ListModel.Create (fun item -> item.Key)[] }

    let CreateTasksModel() = 
        { Tasks = ListModel.Create (fun item -> item.Key)[] }

    let CreateCoursesModel() = 
        { Courses = ListModel.Create (fun item -> item.Key)[] }

    let CreatePeopleModel() = 
        { People = ListModel.Create (fun item -> item.Key)[] }

    (*Сюда подается список задач, нужно его доставать из БД*)
    let RenderToCheck (m: TasksModel) (tochek: TaskItem) = 
        div [] [ 
            tr [] [
                td [] [
                    text tochek.Date
                    text tochek.Name
                    text tochek.Info
                ]
                (*Кнопка принятия задания. Видимо на этом моменте нужно говорить бд,что в таблице для проверки данной задачи нет*)
                td [] [
                    button [on.click (fun _ _ -> tochek.IsAccepted.Value <- true
                                                 tochek.IsChangesRequired.Value <- false
                                                 m.Tasks.Remove tochek)
                    ] [text "Accept"]
                ]
                (*Кнопка запроса изменений. Видимо на этом моменте нужно говорить бд, что в таблице для проверки данной задачи нет*)
                td [] [
                    button [on.click (fun _ _ -> tochek.IsChangesRequired.Value <- true
                                                 tochek.IsAccepted.Value <- false
                                                 m.Tasks.Remove tochek)
                    ] [text "Require Changes"]
                ]
            ]
        ]

    let RenderTasksToAppoint (m: RealTasksModel) (task: RealTaskItem) = 
        div [] [
            tr [] [
                td [] [
                    h6 [] [text task.Name]
                    p [] [text task.Requirement]
                ]
                td [] [
                    button [on.click (fun _ _ -> m.RealTasks.Remove task)] [text "Appoint"]
                ]
            ]
        ]

    let RenderCoursesToAppoint (m: CoursesModel) (course: CourseItem) = 
        div [] [
            tr [] [
                td [] [
                    text course.Name
                ]
                td [] [
                    button [on.click (fun _ _ -> m.Courses.Remove course)] [text "Appoint"]
                ]
            ]
        ]
    
    let RenderCourseStudents (m: PeopleModel) (person: PersonItem) = 
        div [] [
            tr [] [
                td [] [
                    text person.Name
                ]
                td [] [
                    button [on.click (fun _ _ -> m.People.Remove person)] [text "Remove"]
                ]
            ]
        ]

    let RenderCoursemates (m: PeopleModel) (person: PersonItem) = 
        div [] [text person.Name]

    let RenderToDo (m: TasksModel) (todo: TaskItem) =
        div [] [
            tr [] [
                td [] [
                    text todo.Date
                    text todo.Name
                    text todo.Info
                ]
                td [] [
                    button [on.click (fun _ _ -> m.Tasks.Remove todo)] [text "Upload"]
                ]
            ]
        ] 

    let RenderCourses (m: CoursesModel) (course: CourseItem) = 
        div [] [text course.Name]

    let CoursePeopleList m =
        m.People.View
        |> Doc.ConvertBy m.People.Key (RenderCoursemates m)

    let CoursesToAppoint m =
        m.Courses.View
        |> Doc.ConvertBy m.Courses.Key (RenderCoursesToAppoint m)
    
    let TasksToAppoint m =
        m.RealTasks.View
        |> Doc.ConvertBy m.RealTasks.Key (RenderTasksToAppoint m)

    let ToCheckList m = 
        m.Tasks.View
        |> Doc.ConvertBy m.Tasks.Key (RenderToCheck m)

    let ToDoList m = 
        m.Tasks.View
        |> Doc.ConvertBy m.Tasks.Key (RenderToDo m)

    let CoursesList m = 
        m.Courses.View
        |> Doc.ConvertBy m.Courses.Key (RenderCourses m)

    let CoursematesList m = 
        m.People.View
        |> Doc.ConvertBy m.People.Key (RenderCoursemates m)

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

    let CoursesOverview() =
        let m = CreateCoursesModel() 
        div [] [CoursesList m]

    let FollowCourse() = 
        let m = CreatePeopleModel()
        div [] [CoursematesList m]
