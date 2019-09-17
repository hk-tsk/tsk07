using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SharedDA.Entities
{
    public abstract class BaseEntity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<string> changedProps = new List<string>();
        protected BaseEntity()
        {
            PropertyChanged += BaseEntity_PropertyChanged;
            this.ConcurencyStamp = Guid.NewGuid().ToString();
        }

        [Required]
        public long Id { get; set; }

        [ConcurrencyToken]
        public string ConcurencyStamp { get; set; }

        private void BaseEntity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (!changedProps.Contains(e.PropertyName))
            {
                changedProps.Add(e.PropertyName);
            }
        }
    }
}