using BL;
using BL.Entities;
using Microsoft.AspNetCore.Mvc;
using DACommon;
using System.Collections.Generic;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly ContentBL contentBl;

        public ContentController(IUnitOfWork unitOfWork)
        {
            contentBl = new ContentBL(unitOfWork);
        }
   
        [HttpGet("{coursename}/{category}", Name = "GetContent")]
        public IEnumerable<Content> Get(string coursename, string category)
        {
            return contentBl.GetContent(coursename, category);
        }
    }
}