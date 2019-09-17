using Common;
using DACommon.Entities;
using DAEF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAEFC
{
    public class LearningSiteDBContextSeed
    {
        public static async Task Init(string connectionString)
        {

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<LearningSiteDBContext>()
             .UseSqlServer(connectionString)
             ;
                LearningSiteDBContext context = new LearningSiteDBContext(optionsBuilder.Options);

                if (!context.Courses.Any())
                {
                    context.AddRange(AddCourses());
                    await context.SaveChangesAsync();

                    context.AddRange(AddReactCategories(1));
                    context.AddRange(AddNodeCategories(2));
                    context.AddRange(AddVueCategories(3));

                    await context.SaveChangesAsync();

                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }

        private static List<Course> AddCourses()
        {
            return new List<Course>()
            {
                 new Course()
                {
                    Name = "React",
                    Title = "آموزش React",
                    IntroInfo = "در این بخش، توضیحات کاملی از ایحاد تا اجرا یک برنامه React داده می شود. به امکانات redux  و route و ... نیز پرداخته می شود.",
                    Img = "React.svg",
                    IsNew = true,
                },
                  new Course()
                {
                    Name = "NodeJs",
                    Title = "آموزش NodeJs",
                    IntroInfo = "در این بخش در مورد نحوه نصب برنامه NodeJs توضیح داده خواهد شد.",
                    Img = "NodeJs.svg",
                    IsNew = false,
                },new Course()
                {
                    Name = "Vue",
                    Title = "آموزش Vue",
                    IntroInfo = "در این بخش، توضیحات کاملی از ایحاد تا اجرا یک برنامه Vue داده می شود. به امکانات vuex  و route و ... نیز پرداخته می شود.",
                    Img = "VueJs.svg",
                    IsNew = false,
                    CategoryRowColumnsCount=2,

                }
            };

        }

        private static List<Category> AddReactCategories(long courseId)
        {
            return new List<Category>()
            {
                CreateCategory(courseId,"CreateReactProject","ایجاد پروژه React","CreateReactProject.gif", Enums.ImagePosition.Left),
                CreateCategory(courseId,"CreateReactRoute","ایجاد Route","CreateReactRoute.svg",Enums.ImagePosition.Right),
            };
        }

        private static List<Category> AddVueCategories(long courseId)
        {
            return new List<Category>()
            {
                CreateCategory(courseId,"CreateVueProject","ایجاد پروژه Vue","CreateVueProject.gif"),
                CreateCategory(courseId,"CreateVueRoute","ایجاد Route در Vue","CreateVueRoute.svg"),
                CreateCategory(courseId,"ApplyVuex","اعمال vuex در پروژه","ApplyVuex.jpg"),
            };
        }

        private static List<Category> AddNodeCategories(long courseId)
        {
            return new List<Category>()
            {
                 CreateCategory(courseId,"Installation","نصب برنامه NodeJs","InstallNode.jpg"),
            };

        }

        private static Category CreateCategory(long courseId, string name, string title, string imgUrl = null, Enums.ImagePosition imageposition = Enums.ImagePosition.Top, string desc = null)
        {
            return new Category()
            {
                Name = name,
                Title = title,
                ImageUrl = imgUrl,
                Description = desc,
                CourseId = courseId,
                ImagePosition = (int)imageposition,
            };
        }
    }
}
