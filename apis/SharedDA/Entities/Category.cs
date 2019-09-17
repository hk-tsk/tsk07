namespace SharedDA.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string Title { get; set; }

        [ReferenceEntity(typeof(Course))]
        public Course Course { get; set; }

    }
}
