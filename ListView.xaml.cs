using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ReproWinUI3DissapearingProgressRing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListView : Page
    {
        public ObservableCollection<ItemInfo> Items = new ObservableCollection<ItemInfo>();

        public ListView()
        {
            this.InitializeComponent();

            MyListview.ItemsSource = Items;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Items.Add(new ItemInfo());
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (Items.Count > 0)
            {
                Items.RemoveAt(Items.Count - 1);
            }
        }

        private async void Analyze_Click(object sender, RoutedEventArgs e)
        {
            var analyzeCmd = sender as AppBarButton;
            var itemInfo = analyzeCmd.Tag as ItemInfo;
            itemInfo.ItemAnalysis = new ItemAnalysis(itemInfo);

            await itemInfo.ItemAnalysis.Analyze();
        }

        public static Visibility ShouldShowOptions(bool isSelected, bool isActive)
        {
            if (isSelected && !isActive)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        private void MyListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
            {
                foreach (var removedItem in e.RemovedItems)
                {
                    var previousItemInfo = removedItem as ItemInfo;
                    if (previousItemInfo != null)
                    {
                        previousItemInfo.IsSelected = false;
                    }
                }
            }

            var itemInfo = MyListview.SelectedItem as ItemInfo;
            if (itemInfo != null)
            {
                itemInfo.IsSelected = true;
            }
        }
    }
}
