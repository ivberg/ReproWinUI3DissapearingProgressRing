using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ReproWinUI3DissapearingProgressRing
{
    public class ItemAnalysis : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ItemInfo itemInfo;

        public ItemAnalysis(ItemInfo itemInfo)
        {
            this.itemInfo = itemInfo;
        }

        private bool isBeingAnalyzed;
        public bool IsBeingAnalyzed
        {
            get
            {
                return isBeingAnalyzed;
            }

            private set
            {
                isBeingAnalyzed = value;
                NotifyPropertyChanged();
            }
        }

        private int analysisProgress;
        public int AnalysisProgress
        {
            get
            {
                return analysisProgress;
            }

            private set
            {
                analysisProgress = value;
                NotifyPropertyChanged();
            }
        }

        public async Task Analyze()
        {
            IsBeingAnalyzed = true;
            AnalysisProgress = 5;

            try
            {
                var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();

                await Task.Run(async () =>
                {
                    dispatcherQueue.TryEnqueue(() =>
                    {
                        AnalysisProgress = 25;
                    });
                    await Task.Run(() => Thread.Sleep(2000));
                    dispatcherQueue.TryEnqueue(() =>
                    {
                        AnalysisProgress = 50;
                    });
                    await Task.Run(() => Thread.Sleep(2000));
                    dispatcherQueue.TryEnqueue(() =>
                    {
                        AnalysisProgress = 75;
                    });
                    await Task.Run(() => Thread.Sleep(2000));
                });

                AnalysisProgress = 100;
            }
            finally
            {
                IsBeingAnalyzed = false;
                AnalysisProgress = 0;
            }
        }
    }
}
