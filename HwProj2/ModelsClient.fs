namespace HwProj2

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html

[<JavaScript>]
module ModelsClient = 
    open DataManager.Models

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
            TeacherFullName: string
            GroupId: string
            Key: Key
        }

        static member Create teacher group = 
            { Key = Key.Fresh(); TeacherFullName = teacher; GroupId = group }

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

