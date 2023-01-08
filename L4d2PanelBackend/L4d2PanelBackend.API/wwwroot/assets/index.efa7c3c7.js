import{r as W,o as x,c as O,a as N,p as T,b as I,d as b,e as D,f as v,g as X,u as ae,h as c,w as m,t as re,i,N as S,j as se,k as ie,H as ue,U as ce,l as de,m as _e,n as M,q as U,v as j,s as V,x as pe,y as fe,z as Y,A as f,B as ve,C as me,D as he,E as we,F as z,G as ye,I as ge,J as be,K as J,L as xe,M as ke,O as Ee,P as Ce,Q as Fe,R as Se,S as De,T as F,V as Ne,W as Ae,X as Be,Y as $e,Z as Le,_ as Me,$ as Oe}from"./vendor.a0809b5e.js";const Te=function(){const n=document.createElement("link").relList;if(n&&n.supports&&n.supports("modulepreload"))return;for(const a of document.querySelectorAll('link[rel="modulepreload"]'))l(a);new MutationObserver(a=>{for(const e of a)if(e.type==="childList")for(const o of e.addedNodes)o.tagName==="LINK"&&o.rel==="modulepreload"&&l(o)}).observe(document,{childList:!0,subtree:!0});function r(a){const e={};return a.integrity&&(e.integrity=a.integrity),a.referrerpolicy&&(e.referrerPolicy=a.referrerpolicy),a.crossorigin==="use-credentials"?e.credentials="include":a.crossorigin==="anonymous"?e.credentials="omit":e.credentials="same-origin",e}function l(a){if(a.ep)return;a.ep=!0;const e=r(a);fetch(a.href,e)}};Te();var k=(t,n)=>{const r=t.__vccOpts||t;for(const[l,a]of n)r[l]=a;return r};const Ie={};function Re(t,n){const r=W("router-view");return x(),O(r)}var He=k(Ie,[["render",Re]]);const Pe={},Q=t=>(T("data-v-0a38735b"),t=t(),I(),t),Ue={class:"logo"},je=Q(()=>b("h2",{class:"title"},"L4D2 Server Dashboard",-1)),Ve=Q(()=>b("span",null,"logo\u8FD8\u6CA1\u505A\u5148\u51D1\u5408\u4E00\u4E0B",-1)),ze=[je,Ve];function Ke(t,n){return x(),N("div",Ue,ze)}var Ge=k(Pe,[["render",Ke],["__scopeId","data-v-0a38735b"]]);const qe=t=>(T("data-v-1b1cc04d"),t=t(),I(),t),We={class:"switch_edition"},Xe={class:"switcher"},Ye=qe(()=>b("svg",{xmlns:"http://www.w3.org/2000/svg","xmlns:xlink":"http://www.w3.org/1999/xlink",viewBox:"0 0 320 512"},[b("path",{d:"M143 352.3L7 216.3c-9.4-9.4-9.4-24.6 0-33.9l22.6-22.6c9.4-9.4 24.6-9.4 33.9 0l96.4 96.4l96.4-96.4c9.4-9.4 24.6-9.4 33.9 0l22.6 22.6c9.4 9.4 9.4 24.6 0 33.9l-136 136c-9.2 9.4-24.4 9.4-33.8 0z",fill:"currentColor"})],-1)),Je=D({setup(t){const n=v("professional"),r=[{label:"\u6807\u51C6\u7248",value:"standard",disabled:!0},{label:"\u9AD8\u7EA7\u7248",value:"premium",disabled:!0},{label:"\u4E13\u4E1A\u7248",value:"professional"}],l=o=>{if(o!=="professional"){e.error("\u522B\u770B\u4E86\u4E0D\u652F\u6301\u8FD9\u4E2A");return}n.value=o},a=X(()=>{for(let o of r)if(o.value===n.value)return o.label}),e=ae();return(o,w)=>(x(),N("div",We,[c(i(se),{value:n.value,"onUpdate:value":[w[0]||(w[0]=y=>n.value=y),l],options:r,size:"small"},{default:m(()=>[b("div",Xe,[b("span",null,re(i(a)),1),c(i(S),null,{default:m(()=>[Ye]),_:1})])]),_:1},8,["value"])]))}});var Qe=k(Je,[["__scopeId","data-v-1b1cc04d"]]);const h=ie("store",()=>{const t=v(""),n=v(""),r=v(!1),l=v(""),a=v(""),e=v("./");return{endpoint:t,component:n,is_server_running:r,selected_file_name:l,selected_file_type:a,current_path:e}}),Ze=async()=>{h(),await fetch("/processes",{method:"POST"})},et=async()=>{h(),await fetch("/processes",{method:"PATCH"})},tt=async t=>{h();let n="/processes/command",r=new FormData;r.append("command",t),await fetch(n,{method:"POST",body:r})},nt=async()=>{h();let t="/processes/",n=await(await fetch(t,{method:"GET"})).json();return n.length==0?!1:(t+=n[0],(await(await fetch(t,{method:"GET"})).json()).exit_time===null)};setInterval(()=>{nt().then(t=>h().is_server_running=t)},1e3);const ot=async()=>{h();let t="/hub";return new ue().withUrl(t).build()},lt=async t=>{h();let n="/files/"+t;return await(await fetch(n)).json()},K=async t=>{h();let n="/files/"+t;return await fetch(n,{method:"POST"})},G=async(t,n,r,l)=>{h();let a="/upload",e=new ce(t,{endpoint:a,retryDelays:[0,3e3,5e3,1e4,2e4],metadata:{name:t.name,contentType:t.type||"application/octet-stream",emptyMetaKey:"",location:n},onError:function(o){console.log("Failed because: "+o),l()},onProgress:function(o,w){let y=(o/w*100).toFixed(2);console.log(e.file.name,o,w,y+"%")},onSuccess:function(){console.log("Succeed. Download %s from %s",e.file.name,e.url),r()}});e.findPreviousUploads().then(function(o){o.length&&e.resumeFromPreviousUpload(o[0]),e.start()})},q=async t=>{h();let n="/files/"+t;return await fetch(n,{method:"DELETE"})};const at={class:"side_menu"},rt=Y(" \u542F\u52A8 "),st=Y(" \u505C\u6B62 "),it=D({setup(t){var w;const n=de(),r=_e(),l=v((w=r.name)==null?void 0:w.toString());M(l,y=>{n.push(`/${r.params.endpoint}/${String(y)}`)});const a=[{key:"file",label:"\u6587\u4EF6\u7BA1\u7406",icon:()=>f(S,null,{default:()=>[f("svg",{xmlns:"http://www.w3.org/2000/svg","xmlns:xlink":"http://www.w3.org/1999/xlink",viewBox:"0 0 32 32"},f("path",{d:"M11.17 6l3.42 3.41l.58.59H28v16H4V6h7.17m0-2H4a2 2 0 0 0-2 2v20a2 2 0 0 0 2 2h24a2 2 0 0 0 2-2V10a2 2 0 0 0-2-2H16l-3.41-3.41A2 2 0 0 0 11.17 4z",fill:"currentColor"}))]})},{key:"terminal",label:"\u63A7\u5236\u53F0",icon:()=>f(S,null,{default:()=>[f("svg",{xmlns:"http://www.w3.org/2000/svg","xmlns:xlink":"http://www.w3.org/1999/xlink",viewBox:"0 0 24 24"},f("g",{fill:"none",stroke:"currentColor","stroke-width":"2","stroke-linecap":"round","stroke-linejoin":"round"},[f("path",{d:"M5 7l5 5l-5 5"}),f("path",{d:"M12 19h7"})]))]})}],e=v(!1),o=v(!1);return M(()=>h().is_server_running,y=>{e.value=y,o.value=!1}),(y,d)=>(x(),N("div",at,[U(c(i(V),{class:"control_button",strong:"",secondary:"",type:"primary",loading:o.value,onClick:d[0]||(d[0]=_=>{i(Ze)(),o.value=!0})},{default:m(()=>[rt]),_:1},8,["loading"]),[[j,!e.value]]),U(c(i(V),{class:"control_button",strong:"",secondary:"",type:"error",loading:o.value,onClick:d[1]||(d[1]=_=>{i(et)(),o.value=!0})},{default:m(()=>[st]),_:1},8,["loading"]),[[j,e.value]]),c(i(pe)),c(i(fe),{options:a,value:l.value,"onUpdate:value":d[2]||(d[2]=_=>l.value=_)},null,8,["value"])]))}});var ut=k(it,[["__scopeId","data-v-6307a47d"]]);const ct=t=>(T("data-v-92666210"),t=t(),I(),t),dt=ct(()=>b("span",{style:{color:"#66CCFF"}},"\u8FD9\u91CC\u597D\u50CF\u5E94\u8BE5\u5199\u70B9\u4EC0\u4E48\uFF0C\u4F46\u6211\u61D2\u5F97\u60F3\u4E86\u3002",-1)),_t=D({setup(t){return(n,r)=>{const l=W("router-view");return x(),O(i(z),{class:"dashboard","content-style":"display: flex; flex-direction: column;"},{default:m(()=>[c(i(me),{class:"header",bordered:!0},{default:m(()=>[c(Ge),c(i(ve),null,{default:m(()=>[c(Qe)]),_:1})]),_:1}),c(i(z),{class:"content","has-sider":!0},{default:m(()=>[c(i(he),null,{default:m(()=>[c(ut)]),_:1}),c(i(we),null,{default:m(()=>[c(l,{onOnCommitCommand:i(tt)},null,8,["onOnCommitCommand"])]),_:1})]),_:1}),c(i(ye),{class:"footer"},{default:m(()=>[dt]),_:1})]),_:1})}}});var pt=k(_t,[["__scopeId","data-v-92666210"]]);const ft=D({emits:["OnCommitCommand"],setup(t,{emit:n}){const r=v();let l="",a=new Array;const e=new ge.exports.Terminal({rendererType:"canvas",convertEol:!0,disableStdin:!1,cursorBlink:!0,scrollback:0}),o=new be.exports.FitAddon;e.loadAddon(o),e.onKey(d=>{const _=d.domEvent,A=!_.altKey&&!_.ctrlKey&&!_.metaKey;if(console.log(_.code,e.buffer),_.code==="Enter"||_.code==="NumpadEnter"){if(l.replace(/^\s+|\s+$/g,"").length!=0){n("OnCommitCommand",l),a.push(l);for(let B of l)e.write("\b \b");l=""}}else _.code==="Backspace"?(e.write("\b \b"),l=l.slice(0,l.length-1)):_.code==="ArrowLeft"?e.write(d.key):_.code==="ArrowRight"?e.buffer.active.cursorX<l.length&&e.write(d.key):A&&(e.write(d.key),l+=d.key)}),ot().then(d=>{d.on("ReceiveMessage",_=>{e.writeln(_)}),d==null||d.start().then(()=>{}).catch(_=>{console.log(_)})});let w=new ResizeObserver(()=>{J().then(()=>{o.fit()})});xe(()=>{if(r.value===void 0)throw new Error("Cannot find terminal element!");e.open(r.value),w.observe(r.value)}),ke(()=>{w.disconnect()});const y=X(()=>h().is_server_running);return(d,_)=>(x(),O(i(Ee),{class:"loading",show:!i(y),stroke:"#66CCFF"},{default:m(()=>[b("div",{class:"terminal",ref_key:"terminal_element",ref:r},null,512)]),_:1},8,["show"]))}});var vt=k(ft,[["__scopeId","data-v-f1276280"]]);const mt={class:"file_manager"},ht=D({setup(t){const n=h(),r=[{key:"file_type",fixed:"left",width:"0",className:"file_type_icon",render:s=>s.file_type==="directory"?f("div",{class:"file_type_icon_wrapper"},f(S,{},{default:()=>[f("svg",{xmlns:"http://www.w3.org/2000/svg","xmlns:xlink":"http://www.w3.org/1999/xlink",viewBox:"0 0 24 24"},f("path",{d:"M10.59 4.59C10.21 4.21 9.7 4 9.17 4H4c-1.1 0-1.99.9-1.99 2L2 18c0 1.1.9 2 2 2h16c1.1 0 2-.9 2-2V8c0-1.1-.9-2-2-2h-8l-1.41-1.41z",fill:"currentColor"}))]})):f("div",{class:"file_type_icon_wrapper"},f(S,{},{default:()=>[f("svg",{xmlns:"http://www.w3.org/2000/svg","xmlns:xlink":"http://www.w3.org/1999/xlink",viewBox:"0 0 24 24"},f("path",{d:"M6 2c-1.1 0-1.99.9-1.99 2L4 20c0 1.1.89 2 1.99 2H18c1.1 0 2-.9 2-2V8l-6-6H6zm7 7V3.5L18.5 9H13z",fill:"currentColor"}))]}))},{title:"\u6587\u4EF6\u540D",key:"file_name",className:"file_name"}],l=v([]),a=s=>({onDblclick:u=>{s.file_type==="directory"?w(s.file_name):s.file_type==="file"?y(s.file_name):console.error("Unknown file type!")},onContextmenu:u=>{u.preventDefault(),E.value=!1,n.selected_file_type=s.file_type,n.selected_file_name=s.file_name,B.value=u.clientX,H.value=u.clientY,J().then(()=>{E.value=!0})}});let{current_path:e}=Ce(n);const o=async s=>{let u=await lt(s);if(u===void 0){console.log("Empty response!");return}l.value.length=0,l.value.push({key:"directory_..",file_type:"directory",file_name:".."});for(let p of u.directories)l.value.push({key:"directory_"+p,file_type:"directory",file_name:p});for(let p of u.files)l.value.push({key:"file_"+p,file_type:"file",file_name:p})},w=async s=>{e.value=F.join(e.value,s),e.value.startsWith("..")&&(e.value="./")},y=async s=>{console.log("Try to open file: "+s)},d=()=>{$.value=!0},_=async s=>{await q(F.join(e.value,s)),await o(e.value)},A=async s=>{await q(F.join(e.value,s)),await o(e.value)};M(e,s=>{o(s)}),o(e.value);const B=v(0),H=v(0),Z=[{label:"\u5237\u65B0",key:"refresh"},{label:"\u65B0\u5EFA\u6587\u4EF6\u5939",key:"mkdir"},{label:"\u4E0A\u4F20\u6587\u4EF6",key:"upload_file"},{label:"\u4E0A\u4F20\u6587\u4EF6\u5939",key:"upload_directory"},{label:()=>f("span",{style:{color:"red"}},"\u5220\u9664"),key:"delete"}],E=v(!1),ee=()=>{E.value=!1},te=(s,u)=>{if(E.value=!1,u==="refresh")o(e.value);else if(u==="mkdir")d();else if(u==="upload_file"){let p=document.getElementsByClassName("n-upload-file-input");if(p.length==0)throw new RangeError("Couldn't find file input!");p[0].removeAttribute("webkitdirectory"),s()}else if(u==="upload_directory"){let p=document.getElementsByClassName("n-upload-file-input");if(p.length==0)throw new RangeError("Couldn't find file input!");p[0].setAttribute("webkitdirectory","webkitdirectory"),s()}else u==="delete"?n.selected_file_type==="directory"?_(n.selected_file_name):n.selected_file_type==="file"?A(n.selected_file_name):console.error("Unknown file type:"+n.selected_file_type):console.error("Unknown key: "+u)},ne=({file:s,onFinish:u,onError:p})=>{let g=s.file;if(g.webkitRelativePath!=""&&g.webkitRelativePath!=null){let le=g.webkitRelativePath.substring(0,g.webkitRelativePath.lastIndexOf("/")),P=F.join(e.value,le);K(P).then(()=>{G(g,P,u,p).then(()=>o(e.value))})}else G(g,e.value,u,p).then(()=>o(e.value))},$=v(!1),oe=()=>{C.value!=null&&(K(F.join(e.value,C.value)).then(()=>o(e.value)),C.value=null)},C=v(null);return(s,u)=>(x(),N("div",mt,[c(i(Fe),{class:"file_list",bordered:!1,columns:r,data:l.value,"row-props":a},null,8,["data"]),c(i(Se),{abstract:"",multiple:!0,"custom-request":ne},{default:m(()=>[c(i(Ne),{abstract:""},{default:m(({handleClick:p})=>[c(i(Ae),{placement:"bottom-start",trigger:"manual",x:B.value,y:H.value,options:Z,show:E.value,onClickoutside:ee,onSelect:g=>te(p,g)},null,8,["x","y","show","onSelect"])]),_:1})]),_:1}),c(i(De),{show:$.value,"onUpdate:show":u[1]||(u[1]=p=>$.value=p),preset:"dialog",title:"\u65B0\u5EFA\u6587\u4EF6\u5939","show-icon":!1,"negative-text":"\u53D6\u6D88","positive-text":"\u786E\u8BA4","on-positive-click":oe},{default:m(()=>[c(i(Be),{value:C.value,"onUpdate:value":u[0]||(u[0]=p=>C.value=p),type:"text",placeholder:"\u6587\u4EF6\u5939\u540D"},null,8,["value"])]),_:1},8,["show"])]))}});var wt=k(ht,[["__scopeId","data-v-8a957014"]]);const yt=[{path:"/:endpoint?",component:pt,beforeEnter:t=>{if(typeof t.params.endpoint!="string")throw console.error(t.params.endpoint),new Error("Unable to resolve endpoint.");L.endpoint=t.params.endpoint},children:[{path:"terminal",name:"terminal",component:vt,beforeEnter:()=>{L.component="terminal"}},{path:"file",name:"file",component:wt,beforeEnter:()=>{L.component="file"}},{path:":path(.*)*",redirect:{name:"terminal"}}]}],gt=$e({history:Le(),routes:yt}),bt=Me(),R=Oe(He);R.use(gt);R.use(bt);R.mount("#app");const L=h();