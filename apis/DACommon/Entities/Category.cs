using Common;
using System.Collections.Generic;

namespace DACommon.Entities
{
    [TableName("tblCategory")]
    public class Category : BaseEntity
    {
        public Category()
        {
            this.ImagePosition = (int)Enums.ImagePosition.Top;
            this.ImageTheme = (int)Enums.ImageTheme.None;
        }

        [IsUnique]
        public string Name { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int ImagePosition { get; set; }
        [Required]
        public int ImageTheme { get; set; }
        public string ImageUrl { get; set; }

        [ReferenceEntity(nameof(CourseId))]
        public Course Course { get; set; }

        [Required]
        public long CourseId { get; set; }

        public ICollection<Content> Contents { get; set; }
    }
}
