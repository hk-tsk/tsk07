using System;

namespace DA.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public Boolean IsNew { get; set; }

        public string Img { get; set; }

        public string IntroInfo { get; set; }

    }
}