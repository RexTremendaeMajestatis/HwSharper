namespace Новая_папка1

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html

[<JavaScript>]
module TeacherClient =

    type TaskInfo = 
        {
            Date: string
            Course: string
            Student: string
        }

        override this.ToString() =
            this.Date + " " + this.Course + " " + this.Student

        static member Create (date: string) (course: string) (student: string) = 
            { Date = date; Course = course; Student = student }

    type ToCheckItem = 
        {
            Info: string
            Source: string
            IsAccepted: Var<bool>
            IsChangesRequired: Var<bool>
            Key: Key
        }

        static member Create (taskInfo: TaskInfo) (source: string) = 
            { Key = Key.Fresh(); Info = taskInfo.ToString(); Source = source; IsAccepted = Var.Create false; IsChangesRequired = Var.Create false }

    type ToCheckModel = 
        {
            Tasks: ListModel<Key, ToCheckItem>
        }

    type StudentInfo = 
        {
            Name: string
            Email: string
            Git: string
            Key: Key
        }

        static member Create (name: string) (email: string) (git: string) = 
            { Key = Key.Fresh(); Name = name; Email = email; Git = git }

        override this.ToString() =
            this.Name + " " + this.Email + " " + this.Git

    type CourseModel = 
        {
            Name: string
            Students: ListModel<Key, StudentInfo>
        }

    let CreateCourseModel(name: string) = 
        { Name = name; Students = ListModel.Create (fun item -> item.Key)[] }

    let RenderCourse (m: CourseModel) (student: StudentInfo) = 
        p [] [text m.Name]
        tr [] [
            td [] [
                text student.ToString()
            ]
            td [] [
                button [on.click (fun _ _ -> m.Students.Remove student)
                ] [text "Remove"]
            ]
        ]

    let CourseList m = 
        m.Students.View
        |> Doc.ConvertBy m.Students.Key (RenderCourse m)

    let CreateToCheckModel() = 
        { Tasks = ListModel.Create (fun item -> item.Key)[] }

    (*Вот где-то тут нужно прикрутить бд*)
    let RenderTasks (m: ToCheckModel) (tochek: ToCheckItem) = 
        tr [] [
            (*Вывод информации о задании. На этом моменте задание нужно подгружать из бд*)
            td [] [
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

    let ToCheckList m = 
        m.Tasks.View
        |> Doc.ConvertBy m.Tasks.Key (RenderTasks m)

    let CheckTasks() =
        (*Создание списка для выведения на экран*)
        let m = CreateToCheckModel()
        div [] [ToCheckList m]

    let AddNewTasks() =
        let taskInput = Var.Create ""
        let exerciseInput = Var.Create ""
        let requirement = textarea [attr.cols "80"; attr.rows "20"; attr.name "requirement"] []
        let taskField = Doc.Input [] taskInput
        let exerciseField = Doc.Input [] exerciseInput
        (*Прикрутить подгрузку курсов из бд. Хз как это делать))0))*)
        let courseSelect = select [] []
        div [] [
            tr [] [
                td [] [exerciseField]
                td [] [taskField]
                td [] [courseSelect]
            ]
            div [] [requirement]
            button [] [text "Submit"]
        ]

    let CreateNewCourse() = 
        let courseNameInput = Var.Create ""
        let courseNameField = Doc.Input [] courseNameInput
        let descriptionArea = textarea [attr.cols "80"; attr.rows "20"; attr.name "description"] []
        div [] [
            courseNameField
            descriptionArea
            button [] [text "Submit"]
        ]

    let ManageCourse() = 
        let m = CreateCourseModel("testCourse")
        div [] [
            p [] [text m.Name]
            div [] [CourseList m]
        ]
        div [] [
            button [] [text "Close course"]
        ]