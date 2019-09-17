using System;

namespace SharedDA.Entities
{
    public class Course : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Title { get; set; }

        public Boolean IsNew { get; set; }

        public string Img { get; set; }

        [Required]
        public string IntroInfo { get; set; }

    }
}