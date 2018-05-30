using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using SMS_R33.API.Models;

namespace SMS_R33.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.EnableCors();
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Class>("Classes");
            builder.EntitySet<Student>("Students");
            builder.EntitySet<Admission>("Admissions");
            builder.EntitySet<Exam>("Exams");
            builder.EntitySet<ExamTerm>("ExamTerms");
            builder.EntitySet<ExamYear>("ExamYears");
            builder.EntitySet<Subject>("Subjects");
            builder.EntitySet<Teacher>("Teachers");
            builder.EntitySet<Result>("Results");


            ////bound action
            var smvm = builder.Entity<Student>().Collection.Action("GetStudentMark").ReturnsCollection<StudentMarkVM>();
            smvm.Parameter<int>("ExamId");
            smvm.Parameter<int>("ClassId");

            var cusroll = builder.Entity<Student>().Collection.Action("NextRoll");
            cusroll.Parameter<int>("ClassId");
            cusroll.Returns<int>();

            var rvm = builder.Entity<Result>().Collection.Action("ResultSave");
            rvm.CollectionParameter<ResultVM>("results");


            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
