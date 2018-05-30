using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SMS_R33.API.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        [Required(ErrorMessage = "You Need Class name"), StringLength(20)]
        public string ClassName { get; set; }
        //Navigation Area
        public virtual IList<Student> Students { get; set; }
    }
    public class Student
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Student name is Required"), StringLength(50)]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "Roll No is Required")]

        public int ClassRoll { get; set; }
        //FK
        [Required, ForeignKey("Class")]
        public int ClassId { get; set; }
        //Navigation Area
        public virtual Class Class { get; set; }
        public virtual IList<Result> Results { get; set; }
    }
    public class Subject
    {
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Subject name is Required"), StringLength(50)]

        public string SubjectName { get; set; }
    }
    public class ExamYear
    {
        public int ExamYearId { get; set; }
        [Required]
        public int ExamYearDate { get; set; }
    }
    public class ExamTerm
    {
        public int ExamTermId { get; set; }
        [Required(ErrorMessage = "Exam term is Required"), StringLength(50)]
        public string ExamTermName { get; set; }
    }
    public class Exam
    {
        public int ExamId { get; set; }
        [Required, DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ExamDate { get; set; }
        //FK
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        [ForeignKey("ExamYear")]
        public int ExamYearId { get; set; }
        [ForeignKey("ExamTerm")]
        public int ExamTermId { get; set; }
        //Navigation area
        public virtual Class Class { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ExamYear ExamYear { get; set; }
        public virtual ExamTerm ExamTerm { get; set; }

    }
    public class Result
    {
        public int ResultId { get; set; }
        [Required, ForeignKey("Exam")]
        public int ExamId { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        [ForeignKey("ExamYear")]
        public int ExamYearId { get; set; }
        [ForeignKey("ExamTerm")]
        public int ExamTermId { get; set; }
        //Navigation Area
        public virtual Exam Exam { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ExamYear ExamYear { get; set; }
        public virtual ExamTerm ExamTerm { get; set; }
        [Required(ErrorMessage = "Mark is Required"), Column(TypeName = "float")]
        public float Mark { get; set; }
    }



    public class Admission
    {
        public int AdmissionId { get; set; }
        [Required(ErrorMessage = "Student's Name is required"), StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Father's Name is required"), StringLength(50)]
        public string FathersName { get; set; }
        [Required(ErrorMessage = "Mother's Name is required"), StringLength(50)]
        public string MothersName { get; set; }
        [Required(ErrorMessage = "Current Address is required"), StringLength(250)]
        public string CurrentAddress { get; set; }
        [Required(ErrorMessage = "Permanent Address is required"), StringLength(250)]
        public string PermanentAddress { get; set; }
        [Required(ErrorMessage = "Phone Number is required"), StringLength(20)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email Address is required"), StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Date fo Birth is required"), DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "You must select a gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "You must select a class"), StringLength(50)]
        public string ApplyClass { get; set; }

    }
    public class Teacher
    {
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Teacher name is Required"), StringLength(50)]
        public string TeacherName { get; set; }
        public string Photo { get; set; }

        [Required(ErrorMessage = "Designation is Required"), StringLength(50)]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Joining Date is required"), DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime JoiningDate { get; set; }
        [Required(ErrorMessage = "Ecademic Qualification is Required"), StringLength(50)]
        public string EcademicQualification { get; set; }
        [StringLength(50)]
        public string CellNo { get; set; }

    }
    public class SchoolDbContext : DbContext
    {

        public SchoolDbContext()
        {

            Database.SetInitializer(new SchoolDbInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(builder);
        }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<ExamYear> ExamYears { get; set; }
        public DbSet<ExamTerm> ExamTerms { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Teacher> Teachers { get; set; }


    }
    //Seed Method start
    public class SchoolDbInitializer : DropCreateDatabaseIfModelChanges<SchoolDbContext>
    {
        protected override void Seed(SchoolDbContext context)
        {
            //FOR CLASS TABLE
            Class C6 = new Class { ClassName = "Six" };
            Class C7 = new Class { ClassName = "Seven" };
            Class C8 = new Class { ClassName = "Eight" };
            Class C9 = new Class { ClassName = "Nine" };
            Class C10 = new Class { ClassName = "Ten" };
            context.Classes.AddRange(new Class[] { C6, C7, C8, C9, C10 });

            //FOR STUDENT TABLE
            Student S1 = new Student { StudentName = "Sanowar Hossain", ClassRoll = 60001, Class = C6 };
            Student S2 = new Student { StudentName = "Zakaria Rana", ClassRoll = 60002, Class = C6 };
            Student S3 = new Student { StudentName = "Ayesha Siddika", ClassRoll = 60003, Class = C6 };
            Student S4 = new Student { StudentName = "Rabeul Islam", ClassRoll = 60004, Class = C6 };
            Student S5 = new Student { StudentName = "Shakhawat Hossain", ClassRoll = 70001, Class = C7 };
            Student S6 = new Student { StudentName = "Rafiqul Islam", ClassRoll = 70002, Class = C7 };
            Student S7 = new Student { StudentName = "Faysal Ahmed", ClassRoll = 70003, Class = C7 };
            Student S8 = new Student { StudentName = "Md Al-Amin Hossain", ClassRoll = 70004, Class = C7 };
            Student S9 = new Student { StudentName = "Md Abdul Mannan", ClassRoll = 80001, Class = C8 };
            Student S10 = new Student { StudentName = "Md Shohe", ClassRoll = 80002, Class = C8 };
            Student S11 = new Student { StudentName = "Nasrin Leena", ClassRoll = 80002, Class = C8 };
            Student S12 = new Student { StudentName = "Rabiul Awal", ClassRoll = 80004, Class = C8 };
            Student S13 = new Student { StudentName = "Md Amin Sharif", ClassRoll = 90001, Class = C9 };
            Student S14 = new Student { StudentName = "Tasira Afrin", ClassRoll = 90002, Class = C9 };
            Student S15 = new Student { StudentName = "Sijar Uddin", ClassRoll = 100001, Class = C10 };
            Student S16 = new Student { StudentName = "Razu AHmed", ClassRoll = 100002, Class = C10 };
            context.Students.AddRange(new Student[] { S1, S2, S3, S4, S5, S6, S7, S8, S9, S10, S11, S12, S13, S14, S15, S16 });

            //FOR TEACHER TABLE
            Teacher t1 = new Teacher { TeacherName = "Md. Rafiqul Islam", Designation = "Assistant Headmaster", JoiningDate = new DateTime(2015, 05, 05), EcademicQualification = "M.S.S , B.Ed", CellNo = "015256589" };
            Teacher t2 = new Teacher { TeacherName = "Faysal Ahmed", Designation = "senior Teacher", JoiningDate = new DateTime(2016, 01, 22), EcademicQualification = "B.B.A , M.B.A", CellNo = "0171212589" };
            Teacher t3 = new Teacher { TeacherName = "Ayesha Siddika", Designation = "senior Teacher", JoiningDate = new DateTime(2015, 08, 12), EcademicQualification = "M.A", CellNo = "019568547" };
            Teacher t4 = new Teacher { TeacherName = "Tasira Afrin", Designation = "senior Teacher", JoiningDate = new DateTime(2016, 08, 05), EcademicQualification = "M.S.C", CellNo = "01959874" };
            Teacher t5 = new Teacher { TeacherName = "Shakhawat Hossain", Designation = "senior Teacher", JoiningDate = new DateTime(2015, 02, 05), EcademicQualification = "M.S.S", CellNo = "015826500" };
            Teacher t6 = new Teacher { TeacherName = "Nasrin Leena", Designation = "Assistant Teacher", JoiningDate = new DateTime(2015, 11, 15), EcademicQualification = "B.A", CellNo = "01589555" };
            Teacher t7 = new Teacher { TeacherName = "Anowarul Azim", Designation = "Assistant Teacher", JoiningDate = new DateTime(2016, 02, 01), EcademicQualification = "M.A", CellNo = "01856569" };
            Teacher t8 = new Teacher { TeacherName = "Md Azman Ali", Designation = "Senior Assistant Teacher", JoiningDate = new DateTime(2015, 10, 10), EcademicQualification = "BSC (Bed)", CellNo = "015256589" };


            context.Teachers.AddRange(new Teacher[] { t1, t2, t3, t4, t5, t6, t7, t8 });
            //FOR SUBJECT TABLE
            Subject Sub1 = new Subject { SubjectName = "Bangla" };
            Subject Sub2 = new Subject { SubjectName = "English" };
            Subject Sub3 = new Subject { SubjectName = "Math" };
            Subject Sub4 = new Subject { SubjectName = "Gen. Knowledge" };
            Subject Sub5 = new Subject { SubjectName = "Gen. Science" };
            Subject Sub6 = new Subject { SubjectName = "Religion" };
            Subject Sub7 = new Subject { SubjectName = "Accounting" };
            Subject Sub8 = new Subject { SubjectName = "Management" };
            context.Subjects.AddRange(new Subject[] { Sub1, Sub2, Sub3, Sub4, Sub5, Sub6, Sub7, Sub8 });

            //FOR EXAMYEAR TABLE
            ExamYear Ey1 = new ExamYear { ExamYearDate = new DateTime(2016, 05, 17).Year };
            ExamYear Ey2 = new ExamYear { ExamYearDate = new DateTime(2015, 05, 17).Year };
            ExamYear Ey3 = new ExamYear { ExamYearDate = new DateTime(2014, 05, 17).Year };
            context.ExamYears.AddRange(new ExamYear[] { Ey1, Ey2, Ey3 });

            //FOR EXAMTERM TABLE
            ExamTerm Et1 = new ExamTerm { ExamTermName = "1st Term" };
            ExamTerm Et2 = new ExamTerm { ExamTermName = "2nd Term" };
            ExamTerm Et3 = new ExamTerm { ExamTermName = "3rd Term" };
            context.ExamTerms.AddRange(new ExamTerm[] { Et1, Et2, Et3 });

            //FOR EXAM TABLE
            Exam E1 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 17), Subject = Sub1, Class = C6 };
            Exam E2 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 18), Subject = Sub2, Class = C6 };
            Exam E3 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 19), Subject = Sub3, Class = C6 };
            Exam E4 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 20), Subject = Sub4, Class = C6 };
            Exam E5 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 21), Subject = Sub5, Class = C6 };
            Exam E6 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 22), Subject = Sub6, Class = C6 };
                                                                                            
            Exam E7 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 17), Subject = Sub1, Class = C7 };
            Exam E8 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 18), Subject = Sub2, Class = C7 };
            Exam E9 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 19), Subject = Sub3, Class = C7 };
            Exam E10 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 20), Subject = Sub4, Class = C7 };
            Exam E11 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 21), Subject = Sub5, Class = C7 };
            Exam E12 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 22), Subject = Sub6, Class = C7 };
                                                                                             
            Exam E13 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 17), Subject = Sub1, Class = C8 };
            Exam E14 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 18), Subject = Sub2, Class = C8 };
            Exam E15 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 19), Subject = Sub3, Class = C8 };
            Exam E16 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 20), Subject = Sub4, Class = C8 };
            Exam E17 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 21), Subject = Sub5, Class = C8 };
            Exam E18 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 22), Subject = Sub6, Class = C8 };
                                                                                             
            Exam E19 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 17), Subject = Sub1, Class = C9 };
            Exam E20 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 18), Subject = Sub2, Class = C9 };
            Exam E21 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 19), Subject = Sub3, Class = C9 };
            Exam E22 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 20), Subject = Sub4, Class = C9 };
            Exam E23 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 21), Subject = Sub5, Class = C9 };
            Exam E24 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 22), Subject = Sub6, Class = C9 };
            Exam E25 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 22), Subject = Sub7, Class = C9 };
            Exam E26 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 22), Subject = Sub8, Class = C9 };
                                                                                             
            Exam E27 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 17), Subject = Sub1, Class = C10 };
            Exam E28 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 18), Subject = Sub2, Class = C10 };
            Exam E29 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 19), Subject = Sub3, Class = C10 };
            Exam E30 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 20), Subject = Sub4, Class = C10 };
            Exam E31 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 21), Subject = Sub5, Class = C10 };
            Exam E32 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 22), Subject = Sub6, Class = C10 };
            Exam E33 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 22), Subject = Sub7, Class = C10 };
            Exam E34 = new Exam { ExamTerm = Et1, ExamYear = Ey1, ExamDate = new DateTime(2017, 05, 22), Subject = Sub8, Class = C10 };



            context.Exams.AddRange(new Exam[] { E1, E2, E3, E4, E5, E6, E7, E8, E9, E10, 
                E11, E12, E13, E14, E15, E16, E17, E18, E19, E20, E21, E22, E23, E24, 
                E25, E26, E27, E28, E29, E30, E31, E32, E34 });

            //FOR RESULT TABLE
            //six
            Result R1 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S1, Exam = E1, Subject = Sub1, Mark = 75 };
            Result R2 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S1, Exam = E2, Subject = Sub2, Mark = 80 };
            Result R3 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S1, Exam = E3, Subject = Sub3, Mark = 69 };
            Result R4 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S1, Exam = E4, Subject = Sub4, Mark = 88 };
            Result R5 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S1, Exam = E5, Subject = Sub5, Mark = 78 };
            Result R6 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S1, Exam = E6, Subject = Sub6, Mark = 73 };

            Result R7 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S2, Exam = E1, Subject = Sub1, Mark = 65 };
            Result R8 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S2, Exam = E2, Subject = Sub2, Mark = 75 };
            Result R9 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S2, Exam = E3, Subject = Sub3, Mark = 52 };
            Result R10 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S2, Exam = E4, Subject = Sub4, Mark = 85 };
            Result R11 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S2, Exam = E5, Subject = Sub5, Mark = 74 };
            Result R12 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S2, Exam = E6, Subject = Sub6, Mark = 54 };

            Result R13 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S3, Exam = E1, Subject = Sub1, Mark = 56 };
            Result R14 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S3, Exam = E2, Subject = Sub2, Mark = 45 };
            Result R15 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S3, Exam = E3, Subject = Sub3, Mark = 52 };
            Result R16 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S3, Exam = E4, Subject = Sub4, Mark = 78 };
            Result R17 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S3, Exam = E5, Subject = Sub5, Mark = 68 };
            Result R18 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S3, Exam = E6, Subject = Sub6, Mark = 85 };

            Result R19 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S4, Exam = E1, Subject = Sub1, Mark = 75 };
            Result R20 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S4, Exam = E2, Subject = Sub2, Mark = 80 };
            Result R21 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S4, Exam = E3, Subject = Sub3, Mark = 69 };
            Result R22 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S4, Exam = E4, Subject = Sub4, Mark = 88 };
            Result R23 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S4, Exam = E5, Subject = Sub5, Mark = 78 };
            Result R24 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S4, Exam = E6, Subject = Sub6, Mark = 73 };

            //seven                 
            Result R25 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S5, Exam = E7, Subject = Sub1, Mark = 52 };
            Result R26 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S5, Exam = E8, Subject = Sub2, Mark = 63 };
            Result R27 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S5, Exam = E9, Subject = Sub3, Mark = 42 };
            Result R28 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S5, Exam = E10, Subject = Sub4, Mark = 36 };
            Result R29 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S5, Exam = E11, Subject = Sub5, Mark = 65 };
            Result R30 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S5, Exam = E12, Subject = Sub6, Mark = 41 };

            Result R31 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S6, Exam = E7, Subject = Sub1, Mark = 78 };
            Result R32 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S6, Exam = E8, Subject = Sub2, Mark = 65 };
            Result R33 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S6, Exam = E9, Subject = Sub3, Mark = 63 };
            Result R34 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S6, Exam = E10, Subject = Sub4, Mark = 79 };
            Result R35 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S6, Exam = E11, Subject = Sub5, Mark = 54 };
            Result R36 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S6, Exam = E12, Subject = Sub6, Mark = 63 };

            Result R37 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S7, Exam = E7, Subject = Sub1, Mark = 23 };
            Result R38 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S7, Exam = E8, Subject = Sub2, Mark = 41 };
            Result R39 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S7, Exam = E9, Subject = Sub3, Mark = 52 };
            Result R40 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S7, Exam = E10, Subject = Sub4, Mark = 53 };
            Result R41 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S7, Exam = E11, Subject = Sub5, Mark = 41 };
            Result R42 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S7, Exam = E12, Subject = Sub6, Mark = 56 };

            Result R43 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S8, Exam = E7, Subject = Sub1, Mark = 55 };
            Result R44 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S8, Exam = E8, Subject = Sub2, Mark = 41 };
            Result R45 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S8, Exam = E9, Subject = Sub3, Mark = 23 };
            Result R46 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S8, Exam = E10, Subject = Sub4, Mark = 25 };
            Result R47 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S8, Exam = E11, Subject = Sub5, Mark = 33 };
            Result R48 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S8, Exam = E12, Subject = Sub6, Mark = 32 };

            //Eight                 
            Result R49 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S9, Exam = E13, Subject = Sub1, Mark = 63 };
            Result R50 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S9, Exam = E14, Subject = Sub2, Mark = 77 };
            Result R51 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S9, Exam = E15, Subject = Sub3, Mark = 69 };
            Result R52 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S9, Exam = E16, Subject = Sub4, Mark = 88 };
            Result R53 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S9, Exam = E17, Subject = Sub5, Mark = 78 };
            Result R54 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S9, Exam = E18, Subject = Sub6, Mark = 53 };

            Result R55 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S10, Exam = E13, Subject = Sub1, Mark = 85 };
            Result R56 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S10, Exam = E14, Subject = Sub2, Mark = 75 };
            Result R57 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S10, Exam = E15, Subject = Sub3, Mark = 69 };
            Result R58 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S10, Exam = E16, Subject = Sub4, Mark = 58 };
            Result R59 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S10, Exam = E17, Subject = Sub5, Mark = 68 };
            Result R60 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S10, Exam = E18, Subject = Sub6, Mark = 83 };

            Result R61 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S11, Exam = E13, Subject = Sub1, Mark = 74 };
            Result R62 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S11, Exam = E14, Subject = Sub2, Mark = 56 };
            Result R63 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S11, Exam = E15, Subject = Sub3, Mark = 65 };
            Result R64 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S11, Exam = E16, Subject = Sub4, Mark = 78 };
            Result R65 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S11, Exam = E17, Subject = Sub5, Mark = 87 };
            Result R67 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S11, Exam = E18, Subject = Sub6, Mark = 74 };

            Result R68 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S12, Exam = E13, Subject = Sub1, Mark = 65 };
            Result R69 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S12, Exam = E14, Subject = Sub2, Mark = 69 };
            Result R70 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S12, Exam = E15, Subject = Sub3, Mark = 63 };
            Result R71 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S12, Exam = E16, Subject = Sub4, Mark = 25 };
            Result R72 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S12, Exam = E17, Subject = Sub5, Mark = 47 };
            Result R73 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S12, Exam = E18, Subject = Sub6, Mark = 87 };

            //Nine
            Result R74 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S13, Exam = E19, Subject = Sub1, Mark = 86 };
            Result R75 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S13, Exam = E20, Subject = Sub2, Mark = 80 };
            Result R76 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S13, Exam = E21, Subject = Sub3, Mark = 65 };
            Result R77 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S13, Exam = E22, Subject = Sub4, Mark = 88 };
            Result R78 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S13, Exam = E23, Subject = Sub5, Mark = 78 };
            Result R79 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S13, Exam = E24, Subject = Sub6, Mark = 58 };
            Result R80 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S13, Exam = E25, Subject = Sub7, Mark = 73 };
            Result R81 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S13, Exam = E26, Subject = Sub8, Mark = 56 };

            Result R82 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S14, Exam = E19, Subject = Sub1, Mark = 98 };
            Result R83 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S14, Exam = E20, Subject = Sub2, Mark = 58 };
            Result R84 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S14, Exam = E21, Subject = Sub3, Mark = 69 };
            Result R85 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S14, Exam = E22, Subject = Sub4, Mark = 88 };
            Result R86 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S14, Exam = E23, Subject = Sub5, Mark = 52 };
            Result R87 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S14, Exam = E24, Subject = Sub6, Mark = 73 };
            Result R88 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S14, Exam = E25, Subject = Sub7, Mark = 36 };
            Result R89 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S14, Exam = E26, Subject = Sub8, Mark = 73 };

            //Ten                    
            Result R90 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S15, Exam = E27, Subject = Sub1, Mark = 87 };
            Result R91 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S15, Exam = E28, Subject = Sub2, Mark = 80 };
            Result R92 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S15, Exam = E29, Subject = Sub3, Mark = 86 };
            Result R93 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S15, Exam = E30, Subject = Sub4, Mark = 88 };
            Result R94 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S15, Exam = E31, Subject = Sub5, Mark = 36 };
            Result R95 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S15, Exam = E32, Subject = Sub6, Mark = 78 };
            Result R96 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S15, Exam = E33, Subject = Sub7, Mark = 63 };
            Result R97 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S15, Exam = E34, Subject = Sub8, Mark = 58 };

            Result R98 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S16, Exam = E27, Subject = Sub1, Mark = 36 };
            Result R99 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S16, Exam = E28, Subject = Sub2, Mark = 45 };
            Result R100 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S16, Exam = E29, Subject = Sub3, Mark = 35 };
            Result R101 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S16, Exam = E30, Subject = Sub4, Mark = 47 };
            Result R102 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S16, Exam = E31, Subject = Sub5, Mark = 39 };
            Result R103 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S16, Exam = E32, Subject = Sub6, Mark = 47 };
            Result R104 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S16, Exam = E33, Subject = Sub7, Mark = 48 };
            Result R105 = new Result { ExamTerm = Et1, ExamYear = Ey1, Student = S16, Exam = E34, Subject = Sub8, Mark = 23 };

            //Ten  2015                  

            context.Results.AddRange(new Result[] { R1, R2, R3, R4, R5, R6, R7, R8, R9, R10, R11, R12, R13, R14, R15, R16, R17, R18, 
                R19, R20, R21, R22, R23, R24, R25, R26, R27, R28, R29, R30, R31, R32, R33, R34, R35, R36, R37, R38, R39
                , R40, R41, R42, R43, R44, R45, R46, R47, R48, R49, R50, R51, R52, R53, R54, R55, R56, R57, R58, R59, R60, R61, R62, 
                R63, R64, R65, R67, R68, R69, R70, R71, R72, R73, R74, R75, R76, R77, R78, R79, R80, R81, R82, R83, R84, R85, R86, R87, R88, R89
                , R90, R91, R92, R93, R94, R95, R96, R97, R98, R99, R100, R101, R102, R103, R104, R105});

            //FOR ADMISSION TABLE
            Admission a1 = new Admission
            {
                Name = "Abdul Kuddus",
                FathersName = "Siddik Mia",
                MothersName = "Shirina Khatun",
                CurrentAddress = "234/4, Maradia, Khilgaon, Dhaka",
                PermanentAddress = "Sonargaon, Narayongonj, Dhaka",
                PhoneNumber = "01937463958",
                Email = "raf@gmail.com",
                DOB = DateTime.Parse("2001-10-05"),
                Gender = "Male",
                ApplyClass = "Eight"
            };
            Admission a2 = new Admission
            {
                Name = "Alamin jamader",
                FathersName = "Yeasin Jamader",
                MothersName = "Nayena Aktar",
                CurrentAddress = "24/4, Rampura, Khilgaon, Dhaka",
                PermanentAddress = "Rupgonj, Narayongonj, Dhaka",
                PhoneNumber = "01985463958",
                Email = "guljar55@gmail.com",
                DOB = DateTime.Parse("2000-10-05"),
                Gender = "Male",
                ApplyClass = "Seven"
            };
            Admission a3 = new Admission
            {
                Name = "Sifat Khan",
                FathersName = "Jamal Hosain",
                MothersName = "Mahmuda Aktar",
                CurrentAddress = "34/4, Goran, Khilgaon, Dhaka",
                PermanentAddress = "Demra, Dhaka",
                PhoneNumber = "01937463958",
                Email = "roton@gmail.com",
                DOB = DateTime.Parse("2000-11-15"),
                Gender = "Male",
                ApplyClass = "Eight"
            };
            Admission a17 = new Admission { ApplyClass = "Six", Name = "Sohel Rana", FathersName = "Kamruzzaman", MothersName = "Kamrunnahar", CurrentAddress = "Jatrabari, Dhaka", PermanentAddress = "Comilla", PhoneNumber = "01911928045", Email = "s@s.com", Gender = "Male", DOB = new DateTime(2005, 05, 05) };
            Admission a18 = new Admission { ApplyClass = "Six", Name = "Alorani Akter", FathersName = "Bokul Mia", MothersName = "Lamia Khatun", CurrentAddress = "Nagarpur , Tangail", PermanentAddress = "Tangail", PhoneNumber = "01911746858", Email = "a@a.com", Gender = "Female", DOB = new DateTime(2006, 05, 28) };
            Admission a19 = new Admission { ApplyClass = "Eight", Name = "Sumon Mia", FathersName = "Monju Mia", MothersName = "Sufia Khatun", CurrentAddress = "Aitport, Dhaka", PermanentAddress = "Bhanga, Faridpur", PhoneNumber = "01681254690", Email = "sh@sh.com", Gender = "Male", DOB = new DateTime(2006, 02, 28) };
            Admission a4 = new Admission { ApplyClass = "Nine", Name = "Muslem Hasan", FathersName = "Monju Mia", MothersName = "Sufia Khatun", CurrentAddress = "Banani, Dhaka", PermanentAddress = "Bhanga, Faridpur", PhoneNumber = "01911014287", Email = "m@mm.com", Gender = "Male", DOB = new DateTime(2005, 09, 25) };
            Admission a5 = new Admission { ApplyClass = "Six", Name = "Aslam Hossain", FathersName = "Monju Mia", MothersName = "Sufia Khatun", CurrentAddress = "Gulshan, Dhaka", PermanentAddress = "Cox's Bazar", PhoneNumber = "01911523570", Email = "fs@sf.com", Gender = "Male", DOB = new DateTime(2005, 07, 14) };
            Admission a6 = new Admission { ApplyClass = "Six", Name = "Rasheda Akter", FathersName = "Akhtaruzzaman", MothersName = "Shekh Hasina", CurrentAddress = "Mohammadpur, Dhaka", PermanentAddress = "Madaripur", PhoneNumber = "01911154803", Email = "sppo@ms.com", Gender = "Female", DOB = new DateTime(2006, 01, 18) };
            Admission a7 = new Admission { ApplyClass = "Six", Name = "Imrul Kayes", FathersName = "Farid Uddin", MothersName = "Hayat-Un-Nessa", CurrentAddress = "Uttara, Dhaka", PermanentAddress = "Shatkhira", PhoneNumber = "01715412587", Email = "k@s.com", Gender = "Male", DOB = new DateTime(2006, 02, 21) };
            Admission a8 = new Admission { ApplyClass = "Seven", Name = "Najmul Hasan", FathersName = "Kamrul Hasan", MothersName = "Nabiron Begum", CurrentAddress = "Kawlar, Dhaka", PermanentAddress = "Manikganj", PhoneNumber = "01670147538", Email = "suy@s.com", Gender = "Male", DOB = new DateTime(2006, 05, 15) };
            Admission a9 = new Admission { ApplyClass = "Six", Name = "Talha Jubayer", FathersName = "Sohel Rana", MothersName = "Lamia Khatun", CurrentAddress = "Dhanmondi, Dhaka", PermanentAddress = "Jamalpur", PhoneNumber = "0155428754", Email = "ttre@s.com", Gender = "Male", DOB = new DateTime(2006, 11, 11) };
            Admission a10 = new Admission { ApplyClass = "Eight", Name = "Maksuda Doly", FathersName = "Mosharrof Hossain", MothersName = "Lutfun Nessa", CurrentAddress = "Motijhil, Dhaka", PermanentAddress = "Comilla", PhoneNumber = "01524889532", Email = "d@s.com", Gender = "Female", DOB = new DateTime(2006, 12, 25) };
            Admission a11 = new Admission { ApplyClass = "Seven", Name = "Murad Hossain", FathersName = "Hakim Uddin", MothersName = "Maksuda Begum", CurrentAddress = "Rampura, Dhaka", PermanentAddress = "Noakhali", PhoneNumber = "01712502241", Email = "po@s.com", Gender = "Male", DOB = new DateTime(2005, 04, 15) };
            Admission a12 = new Admission { ApplyClass = "Six", Name = "Shakil Ahmed", FathersName = "Dalil Uddin", MothersName = "Kamrun Nesa", CurrentAddress = "Mohammadpur, Dhaka", PermanentAddress = "Shirajgonj", PhoneNumber = "01725453618", Email = "shak@s.com", Gender = "Male", DOB = new DateTime(2005, 09, 05) };
            Admission a13 = new Admission { ApplyClass = "Nine", Name = "Shabbir Rahman", FathersName = "Kabir Hossain", MothersName = "Ayesha Begum", CurrentAddress = "Jatrabari, Dhaka", PermanentAddress = "Bogra", PhoneNumber = "01681220547", Email = "srre@s.com", Gender = "Male", DOB = new DateTime(2005, 09, 02) };
            Admission a14 = new Admission { ApplyClass = "Nine", Name = "Atab Ahmed", FathersName = "Halim Hasan", MothersName = "Lamia Khatun", CurrentAddress = "Banani, Dhaka", PermanentAddress = "Rajshahi", PhoneNumber = "01524157468", Email = "qwe@s.com", Gender = "Male", DOB = new DateTime(2007, 04, 14) };
            Admission a15 = new Admission { ApplyClass = "Nine", Name = "Khairul Baten", FathersName = "Musa Kazim", MothersName = "Kalima Akter", CurrentAddress = "UttarKhan, Dhaka", PermanentAddress = "Chandpur", PhoneNumber = "01714523698", Email = "kjh@s.com", Gender = "Male", DOB = new DateTime(2005, 08, 25) };
            Admission a16 = new Admission { ApplyClass = "Six", Name = "Mushfiqur Rahim", FathersName = "Baten Khan", MothersName = "Kamrunnahar", CurrentAddress = "Mirpir, Dhaka", PermanentAddress = "Bhola", PhoneNumber = "01911928045", Email = "bat@s.com", Gender = "Male", DOB = new DateTime(2006, 05, 09) };
            context.Admissions.AddRange(new Admission[] { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17, a18, a19 });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}