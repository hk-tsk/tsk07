using BL.Entities;
using DA;
using DACommon;
using System.Linq;

namespace BL
{
    public class ContentBL : BaseBL<Content, DACommon.Entities.Content>
    {

        public ContentBL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            currentRepository = new ContentRepository(unitOfWork);
        }

        public Content[] GetContent(string courseName, string category)
        {
            return ((ContentRepository)currentRepository).GetContent(courseName, category)
               .Select(c => MapToBLEntity(c)).ToArray();
        }
    }
}
