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

    let CreateToCheckModel() = 
        { Tasks = ListModel.Create (fun item -> item.Key) [] }

    (*Вот где-то тут нужно прикрутить бд*)
    let RenderTasks (m: ToCheckModel) (tochek: ToCheckItem) = 
        tr [] [
            (*Вывод информации о задании. На этом моменте задание нужно подгружать из бд*)
            td [] [
                tochek.IsAccepted.View
                |> View.Map (fun isAccepted -> text tochek.Info)
                |> Doc.EmbedView
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

    let TeacherToCheck() =
        (*Создание списка для выведения на экран*)
        let m = CreateToCheckModel()
        div [] [ToCheckList m]

    let TeacherToAdd() =
        let taskInput = Var.Create ""
        let exerciseInput = Var.Create ""
        let console = textarea [attr.cols "80"; attr.rows "20"; attr.name "requirement"] []
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
            div [] [console]
            button [] [text "Submit"]
        ]