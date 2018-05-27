namespace HwProj2

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html

[<JavaScript>]
module RendersClient = 
    
    open ModelsClient
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
                    text (course.TeacherFullName + course.GroupId)
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
        div [] [text (course.TeacherFullName + course.GroupId)]

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

