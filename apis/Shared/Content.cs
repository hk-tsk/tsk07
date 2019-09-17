#if blPart
namespace BL.Entities
#else
namespace webApi.Entities
#endif
{
    public partial class Content
    {
        public string ContentType { get; set; }

        public string ContentText { get; set; }

    }
}
