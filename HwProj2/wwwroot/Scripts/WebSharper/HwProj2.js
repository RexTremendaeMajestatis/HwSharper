(function()
{
 "use strict";
 var Global,HwProj2,ModelsClient,RealTaskItem,TaskItem,CourseItem,PersonItem,RealTasksModel,TasksModel,CoursesModel,PeopleModel,RendersClient,RegClient,CommonClient,Course,Templating,WebSharper,UI,Key,Var,ListModel,List,Doc,AttrProxy,Forms,Bootstrap,Controls,Simple,Concurrency,Remoting,AjaxRemotingProvider,Form,IntelliFactory,Runtime,Pervasives,Validation;
 Global=window;
 HwProj2=Global.HwProj2=Global.HwProj2||{};
 ModelsClient=HwProj2.ModelsClient=HwProj2.ModelsClient||{};
 RealTaskItem=ModelsClient.RealTaskItem=ModelsClient.RealTaskItem||{};
 TaskItem=ModelsClient.TaskItem=ModelsClient.TaskItem||{};
 CourseItem=ModelsClient.CourseItem=ModelsClient.CourseItem||{};
 PersonItem=ModelsClient.PersonItem=ModelsClient.PersonItem||{};
 RealTasksModel=ModelsClient.RealTasksModel=ModelsClient.RealTasksModel||{};
 TasksModel=ModelsClient.TasksModel=ModelsClient.TasksModel||{};
 CoursesModel=ModelsClient.CoursesModel=ModelsClient.CoursesModel||{};
 PeopleModel=ModelsClient.PeopleModel=ModelsClient.PeopleModel||{};
 RendersClient=HwProj2.RendersClient=HwProj2.RendersClient||{};
 RegClient=HwProj2.RegClient=HwProj2.RegClient||{};
 CommonClient=HwProj2.CommonClient=HwProj2.CommonClient||{};
 Course=HwProj2.Course=HwProj2.Course||{};
 Templating=HwProj2.Templating=HwProj2.Templating||{};
 WebSharper=Global.WebSharper;
 UI=WebSharper&&WebSharper.UI;
 Key=UI&&UI.Key;
 Var=UI&&UI.Var;
 ListModel=UI&&UI.ListModel;
 List=WebSharper&&WebSharper.List;
 Doc=UI&&UI.Doc;
 AttrProxy=UI&&UI.AttrProxy;
 Forms=WebSharper&&WebSharper.Forms;
 Bootstrap=Forms&&Forms.Bootstrap;
 Controls=Bootstrap&&Bootstrap.Controls;
 Simple=Controls&&Controls.Simple;
 Concurrency=WebSharper&&WebSharper.Concurrency;
 Remoting=WebSharper&&WebSharper.Remoting;
 AjaxRemotingProvider=Remoting&&Remoting.AjaxRemotingProvider;
 Form=Forms&&Forms.Form;
 IntelliFactory=Global.IntelliFactory;
 Runtime=IntelliFactory&&IntelliFactory.Runtime;
 Pervasives=Forms&&Forms.Pervasives;
 Validation=Forms&&Forms.Validation;
 RealTaskItem.Create=function(name,requirement)
 {
  return RealTaskItem.New(name,requirement,Key.Fresh());
 };
 RealTaskItem.New=function(Name,Requirement,Key$1)
 {
  return{
   Name:Name,
   Requirement:Requirement,
   Key:Key$1
  };
 };
 TaskItem.Create=function(name,info,source,date,course)
 {
  var K;
  K=Key.Fresh();
  return TaskItem.New(name,info,source,Var.Create$1(false),Var.Create$1(false),date,course,K);
 };
 TaskItem.New=function(Name,Info,Source,IsAccepted,IsChangesRequired,Date,Course$1,Key$1)
 {
  return{
   Name:Name,
   Info:Info,
   Source:Source,
   IsAccepted:IsAccepted,
   IsChangesRequired:IsChangesRequired,
   Date:Date,
   Course:Course$1,
   Key:Key$1
  };
 };
 CourseItem.Create=function(teacher,group,course)
 {
  return CourseItem.New(teacher,group,course,Key.Fresh());
 };
 CourseItem.New=function(TeacherFullName,GroupId,CourseId,Key$1)
 {
  return{
   TeacherFullName:TeacherFullName,
   GroupId:GroupId,
   CourseId:CourseId,
   Key:Key$1
  };
 };
 PersonItem.Create=function(name,email,github)
 {
  return PersonItem.New(name,email,github,Key.Fresh());
 };
 PersonItem.New=function(Name,Email,GitHub,Key$1)
 {
  return{
   Name:Name,
   Email:Email,
   GitHub:GitHub,
   Key:Key$1
  };
 };
 RealTasksModel.New=function(RealTasks)
 {
  return{
   RealTasks:RealTasks
  };
 };
 TasksModel.New=function(Tasks)
 {
  return{
   Tasks:Tasks
  };
 };
 CoursesModel.New=function(Courses)
 {
  return{
   Courses:Courses
  };
 };
 PeopleModel.New=function(People)
 {
  return{
   People:People
  };
 };
 ModelsClient.CreatePeopleModel=function()
 {
  return PeopleModel.New(ListModel.Create(function(item)
  {
   return item.Key;
  },List.T.Empty));
 };
 ModelsClient.CreateCoursesModel=function()
 {
  return CoursesModel.New(ListModel.Create(function(item)
  {
   return item.Key;
  },List.T.Empty));
 };
 ModelsClient.CreateTasksModel=function()
 {
  return TasksModel.New(ListModel.Create(function(item)
  {
   return item.Key;
  },List.T.Empty));
 };
 ModelsClient.CreateRealTasksModel=function()
 {
  return RealTasksModel.New(ListModel.Create(function(item)
  {
   return item.Key;
  },List.T.Empty));
 };
 RendersClient.CoursematesList=function(m)
 {
  return Doc.ConvertBy(m.People.key,function(p)
  {
   return RendersClient.RenderCoursemates(m,p);
  },m.People.v);
 };
 RendersClient.CoursesList=function(m)
 {
  return Doc.ConvertBy(m.Courses.key,function(c)
  {
   return RendersClient.RenderCourses(m,c);
  },m.Courses.v);
 };
 RendersClient.ToDoList=function(m)
 {
  return Doc.ConvertBy(m.Tasks.key,function(t)
  {
   return RendersClient.RenderToDo(m,t);
  },m.Tasks.v);
 };
 RendersClient.ToCheckList=function(m)
 {
  return Doc.ConvertBy(m.Tasks.key,function(t)
  {
   return RendersClient.RenderToCheck(m,t);
  },m.Tasks.v);
 };
 RendersClient.TasksToAppoint=function(m)
 {
  return Doc.ConvertBy(m.RealTasks.key,function(t)
  {
   return RendersClient.RenderTasksToAppoint(m,t);
  },m.RealTasks.v);
 };
 RendersClient.CoursesToAppoint=function(m)
 {
  return Doc.ConvertBy(m.Courses.key,function(c)
  {
   return RendersClient.RenderCoursesToAppoint(m,c);
  },m.Courses.v);
 };
 RendersClient.CoursePeopleList=function(m)
 {
  return Doc.ConvertBy(m.People.key,function(p)
  {
   return RendersClient.RenderCoursemates(m,p);
  },m.People.v);
 };
 RendersClient.RenderCourses=function(m,course)
 {
  return Doc.Element("div",[],[Doc.TextNode(course.TeacherFullName+course.GroupId)]);
 };
 RendersClient.RenderToDo$88$38=function(todo,m)
 {
  return function()
  {
   return function()
   {
    return m.Tasks.Remove(todo);
   };
  };
 };
 RendersClient.RenderToDo=function(m,todo)
 {
  return Doc.Element("div",[],[Doc.Element("tr",[],[Doc.Element("td",[],[Doc.TextNode(todo.Date),Doc.TextNode(todo.Name),Doc.TextNode(todo.Info)]),Doc.Element("td",[],[Doc.Element("button",[AttrProxy.HandlerImpl("click",function()
  {
   return function()
   {
    return m.Tasks.Remove(todo);
   };
  })],[Doc.TextNode("Upload")])])])]);
 };
 RendersClient.RenderCoursemates=function(m,person)
 {
  return Doc.Element("div",[],[Doc.TextNode(person.Name)]);
 };
 RendersClient.RenderCourseStudents$71$38=function(person,m)
 {
  return function()
  {
   return function()
   {
    return m.People.Remove(person);
   };
  };
 };
 RendersClient.RenderCourseStudents=function(m,person)
 {
  return Doc.Element("div",[],[Doc.Element("tr",[],[Doc.Element("td",[],[Doc.TextNode(person.Name)]),Doc.Element("td",[],[Doc.Element("button",[AttrProxy.HandlerImpl("click",function()
  {
   return function()
   {
    return m.People.Remove(person);
   };
  })],[Doc.TextNode("Remove")])])])]);
 };
 RendersClient.RenderCoursesToAppoint$59$38=function(course,m)
 {
  return function()
  {
   return function()
   {
    return m.Courses.Remove(course);
   };
  };
 };
 RendersClient.RenderCoursesToAppoint=function(m,course)
 {
  return Doc.Element("div",[],[Doc.Element("tr",[],[Doc.Element("td",[],[Doc.TextNode(course.TeacherFullName+course.GroupId)]),Doc.Element("td",[],[Doc.Element("button",[AttrProxy.HandlerImpl("click",function()
  {
   return function()
   {
    return m.Courses.Remove(course);
   };
  })],[Doc.TextNode("Appoint")])])])]);
 };
 RendersClient.RenderTasksToAppoint$47$38=function(task,m)
 {
  return function()
  {
   return function()
   {
    return m.RealTasks.Remove(task);
   };
  };
 };
 RendersClient.RenderTasksToAppoint=function(m,task)
 {
  return Doc.Element("div",[],[Doc.Element("tr",[],[Doc.Element("td",[],[Doc.Element("h6",[],[Doc.TextNode(task.Name)]),Doc.Element("p",[],[Doc.TextNode(task.Requirement)])]),Doc.Element("td",[],[Doc.Element("button",[AttrProxy.HandlerImpl("click",function()
  {
   return function()
   {
    return m.RealTasks.Remove(task);
   };
  })],[Doc.TextNode("Appoint")])])])]);
 };
 RendersClient.RenderToCheck$31$38=function(tochek,m)
 {
  return function()
  {
   return function()
   {
    tochek.IsChangesRequired.Set(true);
    tochek.IsAccepted.Set(false);
    return m.Tasks.Remove(tochek);
   };
  };
 };
 RendersClient.RenderToCheck$24$38=function(tochek,m)
 {
  return function()
  {
   return function()
   {
    tochek.IsAccepted.Set(true);
    tochek.IsChangesRequired.Set(false);
    return m.Tasks.Remove(tochek);
   };
  };
 };
 RendersClient.RenderToCheck=function(m,tochek)
 {
  return Doc.Element("div",[],[Doc.Element("tr",[],[Doc.Element("td",[],[Doc.TextNode(tochek.Date),Doc.TextNode(tochek.Name),Doc.TextNode(tochek.Info)]),Doc.Element("td",[],[Doc.Element("button",[AttrProxy.HandlerImpl("click",function()
  {
   return function()
   {
    tochek.IsAccepted.Set(true);
    tochek.IsChangesRequired.Set(false);
    return m.Tasks.Remove(tochek);
   };
  })],[Doc.TextNode("Accept")])]),Doc.Element("td",[],[Doc.Element("button",[AttrProxy.HandlerImpl("click",function()
  {
   return function()
   {
    tochek.IsChangesRequired.Set(true);
    tochek.IsAccepted.Set(false);
    return m.Tasks.Remove(tochek);
   };
  })],[Doc.TextNode("Require Changes")])])])]);
 };
 RegClient.RegUser=function()
 {
  function r(email,pass,name,isTeacher,submit)
  {
   return Doc.Element("form",[],[Simple.InputWithError("Email",email,submit.view),Simple.InputPasswordWithError("Password",pass,submit.view),Simple.InputWithError("Name",name,submit.view),Controls.Radio("Teacher",List.ofArray([AttrProxy.Create("class","radio"),AttrProxy.Create("checked","checked")]),isTeacher,[],[AttrProxy.Create("type","radio"),AttrProxy.Create("name","optradio")]),Controls.Radio("Student",List.ofArray([AttrProxy.Create("class","radio")]),Var.Create$1(!isTeacher.Get()),[],[AttrProxy.Create("type","radio"),AttrProxy.Create("name","optradio")]),((function(a$1)
   {
    return(Controls.Button())(a$1);
   }("Register"))(List.ofArray([AttrProxy.Create("class","btn btn-primary")])))(function()
   {
    submit.Trigger();
   }),Controls.ShowErrors([AttrProxy.Create("style","margin-top:1em;")],submit.view)]);
  }
  function a(email,pass,name,isTeacher)
  {
   var b;
   Concurrency.Start((b=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("HwProj2:HwProj2.Server.RegisterUser:1805578801",[email,pass,name,isTeacher]),function()
    {
     Global.location.reload();
     return Concurrency.Return(null);
    });
   })),null);
  }
  return Form.Render(Runtime.Curried(r,5),Form.Run(function($1)
  {
   return a($1[0],$1[1],$1[2],$1[3]);
  },Form.WithSubmit(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Form.Return(Runtime.Curried(function(email,pass,name,isTeacher)
  {
   return[email,pass,name,isTeacher];
  },4)),Validation.IsNotEmpty("Enter an email",Form.Yield(""))),Validation.IsNotEmpty("Enter a password",Form.Yield(""))),Validation.IsNotEmpty("Enter a full name",Form.Yield(""))),Form.Yield(false)))));
 };
 RegClient.AnonUser=function()
 {
  function r(user,pass,submit)
  {
   return Doc.Element("form",[],[Simple.InputWithError("Email",user,submit.view),Simple.InputPasswordWithError("Password",pass,submit.view),((function(a$1)
   {
    return(Controls.Button())(a$1);
   }("Log in"))(List.ofArray([AttrProxy.Create("class","btn btn-primary")])))(function()
   {
    submit.Trigger();
   }),Controls.ShowErrors([AttrProxy.Create("style","margin-top:1em;")],submit.view)]);
  }
  function a(user,pass)
  {
   var b;
   Concurrency.Start((b=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("HwProj2:HwProj2.Server.LoginUser:1381500690",[user,pass]),function()
    {
     Global.location.reload();
     return Concurrency.Return(null);
    });
   })),null);
  }
  return Form.Render(Runtime.Curried3(r),Form.Run(function($1)
  {
   return a($1[0],$1[1]);
  },Form.WithSubmit(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Form.Return(function(user)
  {
   return function(pass)
   {
    return[user,pass];
   };
  }),Validation.IsNotEmpty("Enter an email",Form.Yield(""))),Validation.IsNotEmpty("Enter a password",Form.Yield(""))))));
 };
 RegClient.LogOutUser=function()
 {
  var b;
  Concurrency.Start((b=null,Concurrency.Delay(function()
  {
   return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("HwProj2:HwProj2.Server.LogoutUser:-829366048",[]),function()
   {
    Global.location.href="/";
    return Concurrency.Return(null);
   });
  })),null);
 };
 CommonClient.FollowCourse=function()
 {
  return Doc.Element("div",[],[RendersClient.CoursematesList(ModelsClient.CreatePeopleModel())]);
 };
 CommonClient.DoTasks=function()
 {
  return Doc.Element("div",[],[RendersClient.ToDoList(ModelsClient.CreateTasksModel())]);
 };
 CommonClient.ManageCourse=function()
 {
  var pm,inviteField;
  pm=ModelsClient.CreatePeopleModel();
  inviteField=Doc.Input([],Var.Create$1(""));
  return Doc.Element("div",[],[Doc.Element("h4",[],[Doc.TextNode("test")]),Doc.Element("div",[],[RendersClient.CoursePeopleList(pm)]),Doc.Element("div",[],[Doc.Element("button",[],[Doc.TextNode("Complete")])]),Doc.Element("div",[],[inviteField]),Doc.Element("div",[],[Doc.Element("button",[],[Doc.TextNode("Invite")])])]);
 };
 CommonClient.CreateCourse=function()
 {
  var courseNameInput,courseYearInput,courseNameField,courseYearField;
  courseNameInput=Var.Create$1("");
  courseYearInput=Var.Create$1("");
  courseNameField=Doc.Input([],courseNameInput);
  courseYearField=Doc.Input([],courseYearInput);
  return Doc.Element("div",[],[Doc.Element("div",[],[courseNameField]),Doc.Element("div",[],[courseYearField]),Doc.Element("div",[],[Doc.Element("button",[],[Doc.TextNode("Create")])])]);
 };
 CommonClient.AppointTask=function()
 {
  var tm,cm;
  tm=ModelsClient.CreateRealTasksModel();
  cm=ModelsClient.CreateCoursesModel();
  return Doc.Element("div",[],[Doc.Element("div",[],[RendersClient.TasksToAppoint(tm)]),Doc.Element("div",[],[RendersClient.CoursesToAppoint(cm)]),Doc.Element("div",[],[Doc.Element("button",[],[Doc.TextNode("Push")])])]);
 };
 CommonClient.CreateTask=function()
 {
  var taskNameInput,requirementInput,taskNameField,requirement;
  taskNameInput=Var.Create$1("");
  requirementInput=Var.Create$1("");
  taskNameField=Doc.Input([],taskNameInput);
  requirement=Doc.InputArea([],requirementInput);
  return Doc.Element("div",[],[Doc.Element("div",[],[taskNameField]),Doc.Element("div",[],[requirement]),Doc.Element("button",[],[Doc.TextNode("Push")])]);
 };
 CommonClient.CheckTasks=function()
 {
  return Doc.Element("div",[],[RendersClient.ToCheckList(ModelsClient.CreateTasksModel())]);
 };
 Course.New=function(CourseId,CourseData)
 {
  return{
   CourseId:CourseId,
   CourseData:CourseData
  };
 };
 Templating.MenuBarLogged$42$27=Runtime.Curried3(function($1,$2,$3)
 {
  return RegClient.LogOutUser();
 });
}());
