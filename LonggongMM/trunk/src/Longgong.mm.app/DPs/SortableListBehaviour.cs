using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Longgong.mm.app.DPs
{
    /// <summary>
    ///This class provides utilities for use with WPF markup. The majority of
    ///the utilities are in the form of Attached <see cref="DependencyProperty">
    ///Dependency Properties</see> 
    /// </summary>
    public class SortableListBehaviour
    {
        #region Data

        public static readonly DependencyProperty GridSortablePrefixProperty;

        /// <summary>
        ///Set this Attached DependencyProperty on a ListView
        ///to enable sorting on its columns.
        /// </summary>
        public static readonly DependencyProperty IsGridSortableProperty;

        private static readonly DependencyPropertyKey LastSortDirectionPropertyKey;
        private static readonly DependencyPropertyKey LastSortedPropertyKey;
        public static readonly DependencyProperty SortValueProperty;

        #endregion

        #region Ctor

        static SortableListBehaviour()
        {
            IsGridSortableProperty =
                DependencyProperty.RegisterAttached(
                    "IsGridSortable",
                    typeof (Boolean),
                    typeof (SortableListBehaviour),
                    new PropertyMetadata(
                        new PropertyChangedCallback(
                            OnRegisterSortableGrid)));

            LastSortDirectionPropertyKey =
                DependencyProperty.RegisterAttachedReadOnly(
                    "LastSortDirection",
                    typeof (ListSortDirection),
                    typeof (SortableListBehaviour),
                    new PropertyMetadata());

            LastSortedPropertyKey =
                DependencyProperty.RegisterAttachedReadOnly(
                    "LastSorted",
                    typeof (GridViewColumnHeader),
                    typeof (SortableListBehaviour),
                    new PropertyMetadata());

            GridSortablePrefixProperty =
                DependencyProperty.RegisterAttached(
                    "GridSortablePrefix",
                    typeof (string),
                    typeof (SortableListBehaviour),
                    new PropertyMetadata(
                        new PropertyChangedCallback(
                            OnRegisterSortableGridPrefix)));

            SortValueProperty =
                DependencyProperty.RegisterAttached(
                    "SortValue",
                    typeof (string),
                    typeof (SortableListBehaviour),
                    new PropertyMetadata());
        }

        #endregion

        #region Attached Property Setters/Getters

        #region IsGridSortable

        public static Boolean GetIsGridSortable(DependencyObject obj)
        {
            return (Boolean) obj.GetValue(IsGridSortableProperty);
        }

        public static void SetIsGridSortable(DependencyObject obj, Boolean value)
        {
            obj.SetValue(IsGridSortableProperty, value);
        }

        #endregion

        #region GridSortablePrefix

        public static string GetGridSortablePrefix(DependencyObject obj)
        {
            return (string) obj.GetValue(GridSortablePrefixProperty);
        }

        public static void SetGridSortablePrefix(DependencyObject obj, string value)
        {
            obj.SetValue(GridSortablePrefixProperty, value);
        }

        #endregion

        #region LastSorted

        public static GridViewColumnHeader GetLastSorted(DependencyObject obj)
        {
            return (GridViewColumnHeader) obj.GetValue(LastSortedPropertyKey.DependencyProperty);
        }

        private static void SetLastSorted(DependencyObject obj, GridViewColumnHeader value)
        {
            obj.SetValue(LastSortedPropertyKey, value);
        }

        #endregion

        #region LastSortDirection

        public static ListSortDirection GetLastSortDirection(DependencyObject obj)
        {
            return (ListSortDirection) obj.GetValue(
                                           LastSortDirectionPropertyKey.DependencyProperty);
        }

        private static void SetLastSortDirection(DependencyObject obj,
                                                 ListSortDirection value)
        {
            obj.SetValue(LastSortDirectionPropertyKey, value);
        }

        #endregion

        #region SortValue

        public static String GetSortValue(DependencyObject obj)
        {
            return (String) obj.GetValue(SortValueProperty);
        }

        public static void SetSortValue(DependencyObject obj, String value)
        {
            obj.SetValue(SortValueProperty, value);
        }

        #endregion

        #endregion

        #region PropertyChangedHandlers

        private static void OnRegisterSortableGrid(DependencyObject sender,
                                                   DependencyPropertyChangedEventArgs args)
        {
            var grid = sender as ListView;
            if (grid != null)
            {
                RegisterSortableGridview(grid, args);
            }
        }

        private static void OnRegisterSortableGridPrefix(DependencyObject sender,
                                                         DependencyPropertyChangedEventArgs args)
        {
            var grid = sender as ListView;
            if (grid != null)
            {
                RegisterSortableGridviewPrefix(grid, args);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Stores a RoutedEventHandler for the GridView column header clicked
        /// </summary>
        private static readonly RoutedEventHandler
            GridViewColumnHeaderClickHandler = GridViewColumnHeaderClicked;

        /// <summary>
        /// Registers a ListView to allow sorting
        /// </summary>
        private static void RegisterSortableGridview(ListView grid,
                                                     DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is Boolean && (Boolean) args.NewValue)
            {
                grid.AddHandler(ButtonBase.ClickEvent,
                                GridViewColumnHeaderClickHandler);
            }
            else
            {
                grid.RemoveHandler(ButtonBase.ClickEvent,
                                   GridViewColumnHeaderClickHandler);
            }
        }

        /// <summary>
        /// Registers a ListView sort prefix
        /// </summary>
        private static void RegisterSortableGridviewPrefix(ListView grid,
                                                           DependencyPropertyChangedEventArgs args)
        {
            SetGridSortablePrefix(grid, (string) args.NewValue);
        }

        /// <summary>
        /// Actually applies a Sort to the ListView based on the column clicked
        /// using standard WPF SOrt decriptors
        /// </summary>
        private static void GridViewColumnHeaderClicked(object sender, RoutedEventArgs e)
        {
            var lv = sender as ListView;
            if (lv != null)
            {
                var header = e.OriginalSource as GridViewColumnHeader;
                if (header != null && header.Column != null)
                {
                    ListSortDirection sortDirection;
                    GridViewColumnHeader tmpHeader = GetLastSorted(lv);
                    if (tmpHeader != null)
                        tmpHeader.Column.HeaderTemplate = null;
                    if (header != tmpHeader)
                    {
                        sortDirection = ListSortDirection.Ascending;
                    }
                    else
                    {
                        ListSortDirection tmpDirection = GetLastSortDirection(lv);
                        if (tmpDirection == ListSortDirection.Ascending)
                            sortDirection = ListSortDirection.Descending;
                        else
                            sortDirection = ListSortDirection.Ascending;
                    }
                    SetLastSorted(lv, header);
                    SetLastSortDirection(lv, sortDirection);
                    string resourceTemplateName = "";
                    switch (sortDirection)
                    {
                        case ListSortDirection.Ascending:
                            resourceTemplateName = "HeaderTemplateSortAsc";
                            break;
                        case ListSortDirection.Descending:
                            resourceTemplateName = "HeaderTemplateSortDesc";
                            break;
                    }
                    var tmpTemplate =
                        lv.TryFindResource(resourceTemplateName) as DataTemplate;

                    if (tmpTemplate != null)
                    {
                        header.Column.HeaderTemplate = tmpTemplate;
                    }
                    Sort(lv);
                }
            }
        }

        private static void Sort(ListView lv)
        {
            Cursor oldCursor = lv.Cursor;
            lv.Cursor = Cursors.Wait;

            GridViewColumnHeader header = GetLastSorted(lv);
            var binding = (Binding) header.Column.DisplayMemberBinding;

            string headerProperty;

            if (binding != null)
            {
                headerProperty = binding.Path.Path;
            }
            else
            {
                headerProperty =
                    header.Column.GetValue(
                        SortValueProperty).ToString();
            }

            if (headerProperty.Length > 0)
            {
                ICollectionView dataView =
                    CollectionViewSource.GetDefaultView(lv.ItemsSource);

                if (dataView != null)
                {
                    dataView.SortDescriptions.Clear();
                    ListSortDirection direction = GetLastSortDirection(lv);

                    dataView.SortDescriptions.Add(
                        new SortDescription(headerProperty, direction));
                    dataView.Refresh();
                }
            }

            lv.Cursor = oldCursor;
        }

        #endregion
    }
}