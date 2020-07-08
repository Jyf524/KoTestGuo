using KOTest.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository.Constaint
{
    public partial class DataContent : DbContext
    {
        public DataContent()
            : base("StudentInfoDataBase")
        {
            
        }
        public DbSet<Users> UsersRepository { get; set; }
        public DbSet<Semester> SemesterRepository { get; set; }
        public DbSet<Class> ClassRepository { get; set; }
        public DbSet<Course> CourseRepository { get; set; }
        public DbSet<Teacher> TeacherRepository { get; set; }
        public DbSet<ChoiceCourse> ChoiceCourseRepository { get; set; }
        public DbSet<Score> ScoreRepository { get; set; }
    }
}
