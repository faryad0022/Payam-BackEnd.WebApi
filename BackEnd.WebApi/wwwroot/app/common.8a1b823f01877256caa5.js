"use strict";(self.webpackChunkui=self.webpackChunkui||[]).push([[592],{2509:(i,s,g)=>{g.d(s,{S:()=>n});var o=g(1841),l=g(3018);let n=(()=>{class e{constructor(t){this.http=t}getBlogGroups(){return this.http.get("/SiteBlogs/get-bloggroups")}getLatestBlogs(){return this.http.get("/SiteBlogs/get-latest-blogs")}getBlogById(t){return this.http.get("/SiteBlogs/get-blog/"+t)}getAllBlogsByfIlterAndPaging(t){let r;return null===t.title&&(t.title=""),r=(new o.LE).set("pageId",t.pageId.toString()).set("title",t.title),this.http.get("/SiteBlogs/get-blogs",{params:r})}}return e.\u0275fac=function(t){return new(t||e)(l.\u0275\u0275inject(o.eN))},e.\u0275prov=l.\u0275\u0275defineInjectable({token:e,factory:e.\u0275fac,providedIn:"root"}),e})()}}]);