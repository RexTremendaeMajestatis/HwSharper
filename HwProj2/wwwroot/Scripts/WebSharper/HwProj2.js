(function()
{
 "use strict";
 var Global,HwProj2,Client,Templating,WebSharper,UI,Doc,Forms,Bootstrap,Controls,Simple,List,AttrProxy,Var,Form,IntelliFactory,Runtime,Concurrency,Remoting,AjaxRemotingProvider,Pervasives,Validation;
 Global=window;
 HwProj2=Global.HwProj2=Global.HwProj2||{};
 Client=HwProj2.Client=HwProj2.Client||{};
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
 Form=Forms&&Forms.Form;
 IntelliFactory=Global.IntelliFactory;
 Runtime=IntelliFactory&&IntelliFactory.Runtime;
 Concurrency=WebSharper&&WebSharper.Concurrency;
 Remoting=WebSharper&&WebSharper.Remoting;
 AjaxRemotingProvider=Remoting&&Remoting.AjaxRemotingProvider;
 Pervasives=Forms&&Forms.Pervasives;
 Validation=Forms&&Forms.Validation;
 Client.RegUser=function()
 {
  function r(email,pass,name,isTeacher,submit)
  {
   return Doc.Element("form",[],[Simple.InputWithError("Email",email,submit.view),Simple.InputPasswordWithError("Password",pass,submit.view),Simple.InputWithError("Name",name,submit.view),Controls.Radio("Teacher",List.ofArray([AttrProxy.Create("class","radio"),AttrProxy.Create("checked","checked")]),isTeacher,[],[AttrProxy.Create("type","radio"),AttrProxy.Create("name","optradio")]),Controls.Radio("Student",List.ofArray([AttrProxy.Create("class","radio")]),Var.Create$1(!isTeacher.Get()),[],[AttrProxy.Create("type","radio"),AttrProxy.Create("name","optradio")]),((function(a)
   {
    return(Controls.Button())(a);
   }("Register"))(List.ofArray([AttrProxy.Create("class","btn btn-primary")])))(function()
   {
    submit.Trigger();
   }),Controls.ShowErrors([AttrProxy.Create("style","margin-top:1em;")],submit.view)]);
  }
  return Form.Render(Runtime.Curried(r,5),Form.Run(function(regData)
  {
   var b;
   Concurrency.Start((b=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("HwProj2:HwProj2.Server.RegisterUser:163453041",[regData]),function()
    {
     Global.location.reload();
     return Concurrency.Return(null);
    });
   })),null);
  },Form.WithSubmit(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Form.Return(Runtime.Curried(function(email,pass,name,isTeacher)
  {
   return{
    Role:isTeacher,
    Email:email,
    Password:pass,
    Fullname:name
   };
  },4)),Validation.IsNotEmpty("Enter an email",Form.Yield(""))),Validation.IsNotEmpty("Enter a password",Form.Yield(""))),Validation.IsNotEmpty("Enter a full name",Form.Yield(""))),Form.Yield(false)))));
 };
 Client.AnonUser=function()
 {
  function r(user,pass,submit)
  {
   return Doc.Element("form",[],[Simple.InputWithError("Username",user,submit.view),Simple.InputPasswordWithError("Password",pass,submit.view),((function(a)
   {
    return(Controls.Button())(a);
   }("Log in"))(List.ofArray([AttrProxy.Create("class","btn btn-primary")])))(function()
   {
    submit.Trigger();
   }),Controls.ShowErrors([AttrProxy.Create("style","margin-top:1em;")],submit.view)]);
  }
  return Form.Render(Runtime.Curried3(r),Form.Run(function(userData)
  {
   var b;
   Concurrency.Start((b=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("HwProj2:HwProj2.Server.LoginUser:-526035305",[userData]),function()
    {
     Global.location.reload();
     return Concurrency.Return(null);
    });
   })),null);
  },Form.WithSubmit(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Form.Return(function(user)
  {
   return function(pass)
   {
    return{
     Email:user,
     Password:pass
    };
   };
  }),Validation.IsNotEmpty("Enter an username",Form.Yield(""))),Validation.IsNotEmpty("Enter a password",Form.Yield(""))))));
 };
 Client.LogOutUser=function()
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
 Templating.MenuBarLogged$33$27=Runtime.Curried3(function($1,$2,$3)
 {
  return Client.LogOutUser();
 });
}());
