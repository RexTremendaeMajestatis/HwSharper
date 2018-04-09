(function()
{
 "use strict";
 var Global,HwProj2,Client,WebSharper,UI,Doc,Forms,Bootstrap,Controls,Simple,List,AttrProxy,Form,IntelliFactory,Runtime,Concurrency,Remoting,AjaxRemotingProvider,Pervasives,Validation;
 Global=window;
 HwProj2=Global.HwProj2=Global.HwProj2||{};
 Client=HwProj2.Client=HwProj2.Client||{};
 WebSharper=Global.WebSharper;
 UI=WebSharper&&WebSharper.UI;
 Doc=UI&&UI.Doc;
 Forms=WebSharper&&WebSharper.Forms;
 Bootstrap=Forms&&Forms.Bootstrap;
 Controls=Bootstrap&&Bootstrap.Controls;
 Simple=Controls&&Controls.Simple;
 List=WebSharper&&WebSharper.List;
 AttrProxy=UI&&UI.AttrProxy;
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
  function r(user,pass,name,email,submit)
  {
   return Doc.Element("form",[],[Simple.InputWithError("Username",user,submit.view),Simple.InputPasswordWithError("Password",pass,submit.view),Simple.InputWithError("Name",name,submit.view),Simple.InputWithError("Email",email,submit.view),((function(a)
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
  },Form.WithSubmit(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Pervasives.op_LessMultiplyGreater(Form.Return(Runtime.Curried(function(login,name,email,pass)
  {
   return{
    UserId:login,
    Password:pass,
    Fullname:name,
    Email:email
   };
  },4)),Validation.IsNotEmpty("Enter an username",Form.Yield(""))),Validation.IsNotEmpty("Enter a password",Form.Yield(""))),Validation.IsNotEmpty("Enter a full name",Form.Yield(""))),Validation.IsNotEmpty("Enter a email",Form.Yield(""))))));
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
     User:user,
     Password:pass
    };
   };
  }),Validation.IsNotEmpty("Enter an username",Form.Yield(""))),Validation.IsNotEmpty("Enter a password",Form.Yield(""))))));
 };
 Client.LoggedInUser$17$34=Runtime.Curried3(function($1,$2,$3)
 {
  var b;
  return Concurrency.Start((b=null,Concurrency.Delay(function()
  {
   return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("HwProj2:HwProj2.Server.LogoutUser:-829366048",[]),function()
   {
    Global.location.reload();
    return Concurrency.Return(null);
   });
  })),null);
 });
 Client.LoggedInUser=function()
 {
  return Doc.Element("div",[],[Doc.Element("p",[],[Doc.TextNode("Click to log out")]),Doc.Element("button",[AttrProxy.HandlerImpl("click",function()
  {
   return function()
   {
    var b;
    return Concurrency.Start((b=null,Concurrency.Delay(function()
    {
     return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("HwProj2:HwProj2.Server.LogoutUser:-829366048",[]),function()
     {
      Global.location.reload();
      return Concurrency.Return(null);
     });
    })),null);
   };
  })],[Doc.TextNode("log out")])]);
 };
}());
