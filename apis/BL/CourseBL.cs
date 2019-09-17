using BL.Entities;
using DA;
using DACommon;
using System.Linq;

namespace BL
{
    public class CourseBL : BaseBL<Course, DACommon.Entities.Course>
    {

        public CourseBL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            currentRepository = new CourseRepository(unitOfWork);
        }

        public Course Get(string name)
        {
            return MapToBLEntity(currentRepository.GetQuery()
                .Where(c => c.Name == name).FirstOrDefault());

        }
    }
}