(function()
{
 "use strict";
 var Global,HwProj2,RegClient,TeacherClient,RealTaskItem,TaskItem,CourseItem,PersonItem,RealTasksModel,TasksModel,CoursesModel,PeopleModel,Templating,WebSharper,UI,Doc,Forms,Bootstrap,Controls,Simple,List,AttrProxy,Var,Concurrency,Remoting,AjaxRemotingProvider,Form,IntelliFactory,Runtime,Pervasives,Validation,Key,ListModel;
 Global=window;
 HwProj2=Global.HwProj2=Global.HwProj2||{};
 RegClient=HwProj2.RegClient=HwProj2.RegClient||{};
 TeacherClient=HwProj2.TeacherClient=HwProj2.TeacherClient||{};
 RealTaskItem=TeacherClient.RealTaskItem=TeacherClient.RealTaskItem||{};
 TaskItem=TeacherClient.TaskItem=TeacherClient.TaskItem||{};
 CourseItem=TeacherClient.CourseItem=TeacherClient.CourseItem||{};
 PersonItem=TeacherClient.PersonItem=TeacherClient.PersonItem||{};
 RealTasksModel=TeacherClient.RealTasksModel=TeacherClient.RealTasksModel||{};
 TasksModel=TeacherClient.TasksModel=TeacherClient.TasksModel||{};
 CoursesModel=TeacherClient.CoursesModel=TeacherClient.CoursesModel||{};
 PeopleModel=TeacherClient.PeopleModel=TeacherClient.PeopleModel||{};
 Templating=HwProj2.Templating=HwProj2.Templating||{};
 WebSharper=Global.WebSharper;
 UI=WebSharper&&WebSharper.UI;
 Doc=UI&&UI.Doc;
 Forms=WebSharper&&WebSharper.Forms;
 Bootstrap=Forms&&Forms.Bootstrap;
 Controls=Bootstrap&&Bootstrap.Controls;
 Simple=Controls&&Controls.Simple;
 List=WebSharper&&WebSharper.List;
 AttrProxy=UI&&UI.AttrProxy;
 Var=UI&&UI.Var;
 Concurrency=WebSharper&&WebSharper.Concurrency;
 Remoting=WebSharper&&WebSharper.Remoting;
 AjaxRemotingProvider=Remoting&&Remoting.AjaxRemotingProvider;
 Form=Forms&&Forms.Form;
 IntelliFactory=Global.IntelliFactory;
 Runtime=IntelliFactory&&IntelliFactory.Runtime;
 Pervasives=Forms&&Forms.Pervasives;
 Validation=Forms&&Forms.Validation;
 Key=UI&&UI.Key;
 ListModel=UI&&UI.ListModel;
 RegClient.RegUser=function()
 {
  function r(user,pass,name,email,isTeacher,submit)
  {
   return Doc.Element("form",[],[Simple.InputWithError("Username",user,submit.view),Simple.InputPasswordWithError("Password",pass,submit.view),Simple.InputWithError("Name",name,submit.view),Simple.InputWithError("Email",email,submit.view),Controls.Radio("Teacher",List.ofArray([AttrProxy.Create("class","radio"),AttrProxy.Create("checked","checked")]),isTeacher,[],[AttrProxy.Create("type","radio"),AttrProxy.Create("name","optradio")]),Controls.Radio("Student",List.ofArray([AttrProxy.Create("class","radio")]),Var.Create$1(!isTeacher.Get()),[],[AttrProxy.Create("type","radio"),AttrProxy.Create("name","optradio")]),((function(a$1)
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
  return Form.Render(Runtime.Curried(r,6),Form.Run(function($1)
  {
   return a($1[0],$1[1],$1[2],$1[3]);
  },Form.WithSubmit(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Form.Return(Runtime.Curried(function($1,pass,name,email,isTeacher)
  {
   return[email,pass,name,isTeacher];
  },5)),Validation.IsNotEmpty("Enter an username",Form.Yield(""))),Validation.IsNotEmpty("Enter a password",Form.Yield(""))),Validation.IsNotEmpty("Enter a full name",Form.Yield(""))),Validation.IsNotEmpty("Enter an email",Form.Yield(""))),Form.Yield(false)))));
 };
 RegClient.AnonUser=function()
 {
  function r(user,pass,submit)
  {
   return Doc.Element("form",[],[Simple.InputWithError("Username",user,submit.view),Simple.InputPasswordWithError("Password",pass,submit.view),((function(a$1)
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
  }),Validation.IsNotEmpty("Enter an username",Form.Yield(""))),Validation.IsNotEmpty("Enter a password",Form.Yield(""))))));
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
 TaskItem.New=function(Name,Info,Source,IsAccepted,IsChangesRequired,Date,Course,Key$1)
 {
  return{
   Name:Name,
   Info:Info,
   Source:Source,
   IsAccepted:IsAccepted,
   IsChangesRequired:IsChangesRequired,
   Date:Date,
   Course:Course,
   Key:Key$1
  };
 };
 CourseItem.Create=function(name,year)
 {
  return CourseItem.New(name,year,Key.Fresh());
 };
 CourseItem.New=function(Name,Year,Key$1)
 {
  return{
   Name:Name,
   Year:Year,
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
 TeacherClient.FollowCourse=function()
 {
  return Doc.Element("div",[],[TeacherClient.CoursematesList(TeacherClient.CreatePeopleModel())]);
 };
 TeacherClient.CoursesOverview=function()
 {
  return Doc.Element("div",[],[TeacherClient.CoursesList(TeacherClient.CreateCoursesModel())]);
 };
 TeacherClient.DoTasks=function()
 {
  return Doc.Element("div",[],[TeacherClient.ToDoList(TeacherClient.CreateTasksModel())]);
 };
 TeacherClient.ManageCourse=function()
 {
  var pm,inviteField;
  pm=TeacherClient.CreatePeopleModel();
  inviteField=Doc.Input([],Var.Create$1(""));
  return Doc.Element("div",[],[Doc.Element("h4",[],[Doc.TextNode("test")]),Doc.Element("div",[],[TeacherClient.CoursePeopleList(pm)]),Doc.Element("div",[],[Doc.Element("button",[],[Doc.TextNode("Complete")])]),Doc.Element("div",[],[inviteField]),Doc.Element("div",[],[Doc.Element("button",[],[Doc.TextNode("Invite")])])]);
 };
 TeacherClient.CreateCourse=function()
 {
  var courseNameInput,courseYearInput,courseNameField,courseYearField;
  courseNameInput=Var.Create$1("");
  courseYearInput=Var.Create$1("");
  courseNameField=Doc.Input([],courseNameInput);
  courseYearField=Doc.Input([],courseYearInput);
  return Doc.Element("div",[],[Doc.Element("div",[],[courseNameField]),Doc.Element("div",[],[courseYearField]),Doc.Element("div",[],[Doc.Element("button",[],[Doc.TextNode("Create")])])]);
 };
 TeacherClient.AppointTask=function()
 {
  var tm,cm;
  tm=TeacherClient.CreateRealTasksModel();
  cm=TeacherClient.CreateCoursesModel();
  return Doc.Element("div",[],[Doc.Element("div",[],[TeacherClient.TasksToAppoint(tm)]),Doc.Element("div",[],[TeacherClient.CoursesToAppoint(cm)]),Doc.Element("div",[],[Doc.Element("button",[],[Doc.TextNode("Push")])])]);
 };
 TeacherClient.CreateTask=function()
 {
  var taskNameInput,requirementInput,taskNameField,requirement;
  taskNameInput=Var.Create$1("");
  requirementInput=Var.Create$1("");
  taskNameField=Doc.Input([],taskNameInput);
  requirement=Doc.InputArea([],requirementInput);
  return Doc.Element("div",[],[Doc.Element("div",[],[taskNameField]),Doc.Element("div",[],[requirement]),Doc.Element("button",[],[Doc.TextNode("Push")])]);
 };
 TeacherClient.CheckTasks=function()
 {
  return Doc.Element("div",[],[TeacherClient.ToCheckList(TeacherClient.CreateTasksModel())]);
 };
 TeacherClient.CoursematesList=function(m)
 {
  return Doc.ConvertBy(m.People.key,function(p)
  {
   return TeacherClient.RenderCoursemates(m,p);
  },m.People.v);
 };
 TeacherClient.CoursesList=function(m)
 {
  return Doc.ConvertBy(m.Courses.key,function(c)
  {
   return TeacherClient.RenderCourses(m,c);
  },m.Courses.v);
 };
 TeacherClient.ToDoList=function(m)
 {
  return Doc.ConvertBy(m.Tasks.key,function(t)
  {
   return TeacherClient.RenderToDo(m,t);
  },m.Tasks.v);
 };
 TeacherClient.ToCheckList=function(m)
 {
  return Doc.ConvertBy(m.Tasks.key,function(t)
  {
   return TeacherClient.RenderToCheck(m,t);
  },m.Tasks.v);
 };
 TeacherClient.TasksToAppoint=function(m)
 {
  return Doc.ConvertBy(m.RealTasks.key,function(t)
  {
   return TeacherClient.RenderTasksToAppoint(m,t);
  },m.RealTasks.v);
 };
 TeacherClient.CoursesToAppoint=function(m)
 {
  return Doc.ConvertBy(m.Courses.key,function(c)
  {
   return TeacherClient.RenderCoursesToAppoint(m,c);
  },m.Courses.v);
 };
 TeacherClient.CoursePeopleList=function(m)
 {
  return Doc.ConvertBy(m.People.key,function(p)
  {
   return TeacherClient.RenderCoursemates(m,p);
  },m.People.v);
 };
 TeacherClient.RenderCourses=function(m,course)
 {
  return Doc.Element("div",[],[Doc.TextNode(course.Name)]);
 };
 TeacherClient.RenderToDo$165$38=function(todo,m)
 {
  return function()
  {
   return function()
   {
    return m.Tasks.Remove(todo);
   };
  };
 };
 TeacherClient.RenderToDo=function(m,todo)
 {
  return Doc.Element("div",[],[Doc.Element("tr",[],[Doc.Element("td",[],[Doc.TextNode(todo.Date),Doc.TextNode(todo.Name),Doc.TextNode(todo.Info)]),Doc.Element("td",[],[Doc.Element("button",[AttrProxy.HandlerImpl("click",function()
  {
   return function()
   {
    return m.Tasks.Remove(todo);
   };
  })],[Doc.TextNode("Upload")])])])]);
 };
 TeacherClient.RenderCoursemates=function(m,person)
 {
  return Doc.Element("div",[],[Doc.TextNode(person.Name)]);
 };
 TeacherClient.RenderCourseStudents$148$38=function(person,m)
 {
  return function()
  {
   return function()
   {
    return m.People.Remove(person);
   };
  };
 };
 TeacherClient.RenderCourseStudents=function(m,person)
 {
  return Doc.Element("div",[],[Doc.Element("tr",[],[Doc.Element("td",[],[Doc.TextNode(person.Name)]),Doc.Element("td",[],[Doc.Element("button",[AttrProxy.HandlerImpl("click",function()
  {
   return function()
   {
    return m.People.Remove(person);
   };
  })],[Doc.TextNode("Remove")])])])]);
 };
 TeacherClient.RenderCoursesToAppoint$136$38=function(course,m)
 {
  return function()
  {
   return function()
   {
    return m.Courses.Remove(course);
   };
  };
 };
 TeacherClient.RenderCoursesToAppoint=function(m,course)
 {
  return Doc.Element("div",[],[Doc.Element("tr",[],[Doc.Element("td",[],[Doc.TextNode(course.Name)]),Doc.Element("td",[],[Doc.Element("button",[AttrProxy.HandlerImpl("click",function()
  {
   return function()
   {
    return m.Courses.Remove(course);
   };
  })],[Doc.TextNode("Appoint")])])])]);
 };
 TeacherClient.RenderTasksToAppoint$124$38=function(task,m)
 {
  return function()
  {
   return function()
   {
    return m.RealTasks.Remove(task);
   };
  };
 };
 TeacherClient.RenderTasksToAppoint=function(m,task)
 {
  return Doc.Element("div",[],[Doc.Element("tr",[],[Doc.Element("td",[],[Doc.Element("h6",[],[Doc.TextNode(task.Name)]),Doc.Element("p",[],[Doc.TextNode(task.Requirement)])]),Doc.Element("td",[],[Doc.Element("button",[AttrProxy.HandlerImpl("click",function()
  {
   return function()
   {
    return m.RealTasks.Remove(task);
   };
  })],[Doc.TextNode("Appoint")])])])]);
 };
 TeacherClient.RenderToCheck$108$38=function(tochek,m)
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
 TeacherClient.RenderToCheck$101$38=function(tochek,m)
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
 TeacherClient.RenderToCheck=function(m,tochek)
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
 TeacherClient.CreatePeopleModel=function()
 {
  return PeopleModel.New(ListModel.Create(function(item)
  {
   return item.Key;
  },List.T.Empty));
 };
 TeacherClient.CreateCoursesModel=function()
 {
  return CoursesModel.New(ListModel.Create(function(item)
  {
   return item.Key;
  },List.T.Empty));
 };
 TeacherClient.CreateTasksModel=function()
 {
  return TasksModel.New(ListModel.Create(function(item)
  {
   return item.Key;
  },List.T.Empty));
 };
 TeacherClient.CreateRealTasksModel=function()
 {
  return RealTasksModel.New(ListModel.Create(function(item)
  {
   return item.Key;
  },List.T.Empty));
 };
 Templating.MenuBarLogged$33$27=Runtime.Curried3(function($1,$2,$3)
 {
  return RegClient.LogOutUser();
 });
}());
