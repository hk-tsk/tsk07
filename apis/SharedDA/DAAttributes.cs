using System;

namespace SharedDA
{
    public class Required : Attribute
    {
    }

    public class ConcurrencyToken : Attribute
    {
    }

    public class MaxLength : Attribute
    {
        public int Count { get; set; }
    }

    public class LongText : Attribute
    {
    }

    public class ReferenceEntity : Attribute
    {
        public ReferenceEntity(Type entityType)
        {
            this.Name = entityType.Name + "Id";
        }
        public string Name { get; }

    }

    public class ReferenceEntityCollection : Attribute
    {
    }
}
