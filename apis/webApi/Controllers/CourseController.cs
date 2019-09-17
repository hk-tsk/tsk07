using BL;
using BL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DACommon;
using System.Collections.Generic;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly CourseBL courseBl;
        private readonly CategoryBL categoryBl;

        public CourseController(IUnitOfWork unitOfWork)
        {
            courseBl = new CourseBL(unitOfWork);
            categoryBl = new CategoryBL(unitOfWork);
        }

        // GET: api/Course
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return courseBl.GetAll();
        }

        // GET: api/Course/5/Category
        [HttpGet("{name}/Category", Name = "GetCategories")]
        public IEnumerable<Category> Get(string name)
        {
            if (string.IsNullOrEmpty(name))
                return categoryBl.GetCategories();
            else
                return categoryBl.GetCategories(name);
        }


        //// GET: api/Course/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Course
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Course/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
