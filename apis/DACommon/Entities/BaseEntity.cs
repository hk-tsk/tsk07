using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DACommon.Entities
{
    public abstract class BaseEntity : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        private List<string> changedProps = new List<string>();
        protected BaseEntity()
        {
            PropertyChanging += BaseEntity_PropertyChanging;
            PropertyChanged += BaseEntity_PropertyChanged;
            this.ConcurrencyStamp = Guid.NewGuid().ToString();
        }

        private void BaseEntity_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
           // throw new NotImplementedException();
        }

        [Required]
        public long Id { get; set; }

        [Required]
        [ConcurrencyToken]
        public string ConcurrencyStamp { get; set; }

        private void BaseEntity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (!changedProps.Contains(e.PropertyName))
            {
                changedProps.Add(e.PropertyName);
            }
        }
    }
}