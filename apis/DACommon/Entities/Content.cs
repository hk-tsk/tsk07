namespace DACommon.Entities
{
    [TableName("tblContent")]
    public class Content : BaseEntity
    {
        public string ContentType { get; set; }

        public string ContentText { get; set; }

        [ReferenceEntity(nameof(Content.CategoryId))]
        public Category Category { get; set; }

        [Required]
        public long CategoryId { get; set; }

    }
}
