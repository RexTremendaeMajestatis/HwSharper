(function()
{
 "use strict";
 var Global,WebSharper,Obj,UI,Templating,Runtime,Server,TemplateInitializer,TemplateInstances,Handler,TemplateInstance,Runtime$1,Client,IntelliFactory,Runtime$2,Collections,Dictionary,Arrays,Var,Operators,Doc,Activator,HashSet,Enumerator,Seq;
 Global=window;
 WebSharper=Global.WebSharper;
 Obj=WebSharper&&WebSharper.Obj;
 UI=WebSharper.UI=WebSharper.UI||{};
 Templating=UI.Templating=UI.Templating||{};
 Runtime=Templating.Runtime=Templating.Runtime||{};
 Server=Runtime.Server=Runtime.Server||{};
 TemplateInitializer=Server.TemplateInitializer=Server.TemplateInitializer||{};
 TemplateInstances=Server.TemplateInstances=Server.TemplateInstances||{};
 Handler=Server.Handler=Server.Handler||{};
 TemplateInstance=Server.TemplateInstance=Server.TemplateInstance||{};
 Runtime$1=Server.Runtime=Server.Runtime||{};
 Client=Runtime.Client=Runtime.Client||{};
 IntelliFactory=Global.IntelliFactory;
 Runtime$2=IntelliFactory&&IntelliFactory.Runtime;
 Collections=WebSharper&&WebSharper.Collections;
 Dictionary=Collections&&Collections.Dictionary;
 Arrays=WebSharper&&WebSharper.Arrays;
 Var=UI&&UI.Var;
 Operators=WebSharper&&WebSharper.Operators;
 Doc=UI&&UI.Doc;
 Activator=WebSharper&&WebSharper.Activator;
 HashSet=Collections&&Collections.HashSet;
 Enumerator=WebSharper&&WebSharper.Enumerator;
 Seq=WebSharper&&WebSharper.Seq;
 TemplateInitializer=Server.TemplateInitializer=Runtime$2.Class({
  get_Vars:function()
  {
   return this.vars;
  },
  get_Id:function()
  {
   return this.id;
  },
  get_Instance:function()
  {
   var i,d,a,i$1,$1,f,t;
   if(this.hasOwnProperty("instance"))
    return this.instance;
   else
    {
     d=new Dictionary.New$5();
     a=this.vars;
     for(i$1=0,$1=a.length-1;i$1<=$1;i$1++){
      f=Arrays.get(a,i$1);
      t=f[1];
      d.set_Item(f[0],t===0?Var.Create$1(""):t===1?Var.Create$1(0):t===2?Var.Create$1(false):Operators.FailWith("Invalid value type"));
     }
     i=new TemplateInstance.New({
      $:0,
      $0:d
     },Doc.Empty());
     this.instance=i;
     return i;
    }
  }
 },Obj,TemplateInitializer);
 TemplateInitializer.New=Runtime$2.Ctor(function(id,vars)
 {
  this.id=id;
  this.vars=vars;
 },TemplateInitializer);
 TemplateInstances=Server.TemplateInstances=Runtime$2.Class({},Obj,TemplateInstances);
 TemplateInstances.GetInstance=function(key)
 {
  return(Activator.Instances())[key].get_Instance();
 };
 TemplateInstances.New=Runtime$2.Ctor(function()
 {
 },TemplateInstances);
 Handler.CompleteHoles$142$20=function(key,s)
 {
  return function(el)
  {
   return function()
   {
    return TemplateInstances.GetInstance(key).Hole(s).Set(el.value);
   };
  };
 };
 Handler.EventQ2$117$42=function(key,f)
 {
  return function(el)
  {
   return function(ev)
   {
    return f({
     Vars:TemplateInstances.GetInstance(key),
     Target:el,
     Event:ev
    });
   };
  };
 };
 Handler.CompleteHoles=function(a,filledHoles,vars)
 {
  var allVars,filledVars,e,h,$1,n;
  function c(name,ty)
  {
   var p,r,r$1,r$2;
   return filledVars.Contains(name)?null:(p=ty===0?(r=Var.Create$1(""),[{
    $:8,
    $0:name,
    $1:r
   },r]):ty===1?(r$1=Var.Create$1(0),[{
    $:13,
    $0:name,
    $1:r$1
   },r$1]):ty===2?(r$2=Var.Create$1(false),[{
    $:9,
    $0:name,
    $1:r$2
   },r$2]):Operators.FailWith("Invalid value type"),(allVars.set_Item(name,p[1]),{
    $:1,
    $0:p[0]
   }));
  }
  allVars=new Dictionary.New$5();
  filledVars=new HashSet.New$3();
  e=Enumerator.Get(filledHoles);
  try
  {
   while(e.MoveNext())
    {
     h=e.Current();
     (h.$==8?($1=[h.$0,Client.Box(h.$1)],true):h.$==11?($1=[h.$0,Client.Box(h.$1)],true):h.$==10?($1=[h.$0,Client.Box(h.$1)],true):h.$==13?($1=[h.$0,Client.Box(h.$1)],true):h.$==12?($1=[h.$0,Client.Box(h.$1)],true):h.$==9&&($1=[h.$0,Client.Box(h.$1)],true))?(n=$1[0],filledVars.Add(n),allVars.set_Item(n,$1[1])):void 0;
    }
  }
  finally
  {
   if("Dispose"in e)
    e.Dispose();
  }
  return[Seq.append(filledHoles,Arrays.choose(function($2)
  {
   return c($2[0],$2[1]);
  },vars)),{
   $:0,
   $0:allVars
  }];
 };
 TemplateInstance=Server.TemplateInstance=Runtime$2.Class({
  Hole:function(name)
  {
   return this.allVars.get_Item(name);
  },
  get_Doc:function()
  {
   return this.doc;
  }
 },Obj,TemplateInstance);
 TemplateInstance.New=Runtime$2.Ctor(function(c,doc)
 {
  this.doc=doc;
  this.allVars=c.$==0?c.$0:Operators.FailWith("Should not happen");
 },TemplateInstance);
 Runtime$1.RunTemplate=function(fillWith)
 {
  return Doc.RunFullDocTemplate(fillWith);
 };
 Client.AfterRenderQ2$59$26=function(f)
 {
  return function()
  {
   f();
  };
 };
 Client.Box=Global.id;
}());
