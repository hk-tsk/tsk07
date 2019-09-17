using DACommon;
using DACommon.Entities;

namespace DA
{
    public class CourseRepository : BaseRepository<Course>
    {
        public CourseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

    }
}