namespace SharedDA.Entities
{
    public class Content : BaseEntity
    {
        public string ContentType { get; set; }

        public string ContentText { get; set; }

        [ReferenceEntity(typeof(Category))]
        public Category Category { get; set; }

    }
}
