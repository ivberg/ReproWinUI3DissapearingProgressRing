using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ReproWinUI3DissapearingProgressRing
{
    public enum Status
    {
        Completed,
        Failed,
    }

    public class ItemInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Properties


        /// <summary>
        /// Trace status
        /// </summary>
        private Status status;
        public Status Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(IsActive));
            }
        }

        ItemAnalysis itemAnalysis;
        public ItemAnalysis ItemAnalysis
        {
            get
            {
                return itemAnalysis;
            }

            set
            {
                itemAnalysis = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsActive
        {
            get
            {
                if (status == Status.Completed || status == Status.Failed)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Is the item being viewed by the user
        /// </summary>
        private bool isSelected = false;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

    }
}
