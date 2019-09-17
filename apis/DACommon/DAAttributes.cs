using System;

namespace DACommon
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableName : Attribute
    {
        public TableName(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
    public class Required : Attribute
    {
    }

    public class IsUnique : Attribute
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
        public ReferenceEntity(string name)
        {
            this.Name = name;
        }
        public string Name { get; }

    }

    public class ReferenceEntityCollection : Attribute
    {
    }
}
