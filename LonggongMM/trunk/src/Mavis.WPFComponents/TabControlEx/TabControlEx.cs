using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Mavis.WPFComponents
{
    /// <summary>
    /// The standard WPF TabControl is quite bad in the fact that it only
    /// even contains the current TabItem in the VisualTree, so if you
    /// have complex views it takes a while to re-create the view each tab
    /// selection change.Which makes the standard TabControl very sticky to
    /// work with. This class along with its associated ControlTemplate
    /// allow all TabItems to remain in the VisualTree without it being Sticky.
    /// It does this by keeping all TabItem content in the VisualTree but
    /// hides all inactive TabItem content, and only keeps the active TabItem
    /// content shown.
    /// </summary>
    [TemplatePart(Name = "PART_ItemsHolder", Type = typeof (Panel))]
    public class TabControlEx : TabControl
    {
        #region Data

        private Panel _itemsHolder;

        #endregion

        #region Ctor

        public TabControlEx()
        {
            // this is necessary so that we get the initial databound selected item
            ItemContainerGenerator.StatusChanged += ItemContainerGeneratorStatusChanged;
            Loaded += TabControlExLoaded;
        }

        void TabControlExLoaded(object sender, RoutedEventArgs e)
        {
            UpdateSelectedItem();
        }

        #endregion

        #region Public/Protected Methods

        /// <summary>
        /// get the ItemsHolder and generate any children
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _itemsHolder = GetTemplateChild("PART_ItemsHolder") as Panel;
            UpdateSelectedItem();
        }

        /// <summary>
        /// when the items change we remove any generated panel children and add any new ones as necessary
        /// </summary>
        /// <param name="e"></param>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if (_itemsHolder == null)
            {
                return;
            }

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    _itemsHolder.Children.Clear();
                    break;

                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (object item in e.OldItems)
                        {
                            ContentPresenter cp = FindChildContentPresenter(item);
                            if (cp != null)
                            {
                                _itemsHolder.Children.Remove(cp);
                            }
                        }
                    }

                    // don't do anything with new items because we don't want to
                    // create visuals that aren't being shown

                    UpdateSelectedItem();
                    break;

                case NotifyCollectionChangedAction.Replace:
                    throw new NotImplementedException("Replace not implemented yet");
            }
        }

        /// <summary>
        /// update the visible child in the ItemsHolder
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            UpdateSelectedItem();
        }

        /// <summary>
        /// copied from TabControl; wish it were protected in that class instead of private
        /// </summary>
        /// <returns></returns>
        protected TabItem GetSelectedTabItem()
        {
            object selectedItem = SelectedItem;
            if (selectedItem == null)
            {
                return null;
            }
            var item = selectedItem as TabItem ?? ItemContainerGenerator.ContainerFromIndex(SelectedIndex) as TabItem;
            return item;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// if containers are done, generate the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemContainerGeneratorStatusChanged(object sender, EventArgs e)
        {
            if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                ItemContainerGenerator.StatusChanged -= ItemContainerGeneratorStatusChanged;
                UpdateSelectedItem();
            }
        }

        /// <summary>
        /// generate a ContentPresenter for the selected item
        /// </summary>
        private void UpdateSelectedItem()
        {
            if (_itemsHolder == null)
            {
                return;
            }

            // generate a ContentPresenter if necessary
            TabItem item = GetSelectedTabItem();
            if (item != null)
            {
                CreateChildContentPresenter(item);
            }

            // show the right child
            foreach (ContentPresenter child in _itemsHolder.Children)
            {
                child.Visibility = (((TabItem) child.Tag).IsSelected) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// create the child ContentPresenter for the given item (could be data or a TabItem)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private ContentPresenter CreateChildContentPresenter(object item)
        {
            if (item == null)
            {
                return null;
            }

            ContentPresenter cp = FindChildContentPresenter(item);

            if (cp != null)
            {
                return cp;
            }

            // the actual child to be added.  cp.Tag is a reference to the TabItem
            cp = new ContentPresenter
                     {
                         Content = (item is TabItem) ? (item as TabItem).Content : item,
                         ContentTemplate = SelectedContentTemplate,
                         ContentTemplateSelector = SelectedContentTemplateSelector,
                         ContentStringFormat = SelectedContentStringFormat,
                         Visibility = Visibility.Collapsed,
                         Tag = (item is TabItem) ? item : (ItemContainerGenerator.ContainerFromItem(item))
                     };
            _itemsHolder.Children.Add(cp);
            return cp;
        }

        /// <summary>
        /// Find the CP for the given object.  data could be a TabItem or a piece of data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ContentPresenter FindChildContentPresenter(object data)
        {
            if (data is TabItem)
            {
                data = (data as TabItem).Content;
            }

            if (data == null)
            {
                return null;
            }

            if (_itemsHolder == null)
            {
                return null;
            }

            foreach (ContentPresenter cp in _itemsHolder.Children)
            {
                if (cp.Content == data)
                {
                    return cp;
                }
            }

            return null;
        }

        #endregion
    }
}