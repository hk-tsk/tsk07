using DACommon;
using DACommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DA
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Category> GetAllWithCourse()
        {
            Expression<Func<Category, Course>> include = x => x.Course;
            return Query<Course>(include).ToList();
        }

        public IEnumerable<Category> GetCategory(string courseName)
        {
            return GetQuery().Where(c => c.Course.Name == courseName).ToList();
        }
    }
}
