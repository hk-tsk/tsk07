#if blPart
namespace BL.Entities
#else
namespace webApi.Entities
#endif
{
    public partial class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int ImagePosition { get; set; }
        public int ImageTheme { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public long CourseId { get; set; }

    }
}
