using System;

#if blPart
namespace BL.Entities
#else
namespace webApi.Entities
#endif
{
    public partial class Course
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }
        public int CategoryRowColumnsCount { get; set; }
        public Boolean IsNew { get; set; }

        public string Img { get; set; }

        public string IntroInfo { get; set; }

    }
}
