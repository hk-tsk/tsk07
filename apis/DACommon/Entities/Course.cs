using System;
using System.Collections.Generic;

namespace DACommon.Entities
{
    [TableName("tblCourse")]
    public class Course : BaseEntity
    {
        public Course()
        {
            this.IsNew = false;
            this.CategoryRowColumnsCount = 1;

        }
        [IsUnique]
        public string Name { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int CategoryRowColumnsCount { get; set; }
        public Boolean IsNew { get; set; }

        public string Img { get; set; }

        [Required]
        public string IntroInfo { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}