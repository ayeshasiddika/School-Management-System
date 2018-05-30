using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS_R33.API.Models
{
    public class StudentMarkVM
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int ExamId { get; set; }
        public int ResultId { get; set; }
        public float Mark { get; set; }

    }
    public class ResultVM
    {
        public int? ResultId { get; set; }
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public float Mark { get; set; }
    }
}