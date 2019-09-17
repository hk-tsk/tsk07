using BL.Entities;
using DA;
using DACommon;
using System;
using System.Linq;

namespace BL
{
    public class CategoryBL : BaseBL<Category, DACommon.Entities.Category>
    {
        private readonly CourseBL courseBl;
        public CategoryBL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            currentRepository = new CategoryRepository(unitOfWork);
            courseBl = new CourseBL(unitOfWork);
        }

        private CategoryRepository categoryRepository
        {
            get { return (CategoryRepository)currentRepository; }
        }
        public override void Add(Category entity)
        {
            throw new Exception("Not Used");
        }

        public void AddCategory(Category entity, string courseName)
        {
            Course course = courseBl.Get(courseName);
            DACommon.Entities.Category newItem = MapToDAEntity(entity);
            newItem.Course = MapToDAEntity<Course, DACommon.Entities.Course>(course);
            newItem.CourseId = newItem.Course.Id;
            categoryRepository.Insert(newItem);
            categoryRepository.SaveChanges();
        }
        public Category[] GetCategories(string courseName)
        {

            return categoryRepository.GetCategory(courseName)
               .Select(c => MapToBLEntity(c)).ToArray();

        }

        public Category[] GetCategories()
        {

            return categoryRepository.GetAllWithCourse()
               .Select(c => MapToBLEntity(c)).ToArray();

        }
    }
}
