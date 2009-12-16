using System;
using System.Windows;

namespace Longgong.mm.app.DPs
{
    /// <summary>
    /// Holds attached properties that can be used for Button types
    /// </summary>
    public class ButtonProps
    {
        #region ImageUrl

        /// <summary>
        /// ImageUrl Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty ImageUrlProperty =
            DependencyProperty.RegisterAttached("ImageUrl",
                typeof(String), typeof(ButtonProps),
                    new FrameworkPropertyMetadata((String)String.Empty));

        /// <summary>
        /// Gets the ImageUrl property.  
        /// </summary>
        public static String GetImageUrl(DependencyObject d)
        {
            return (String)d.GetValue(ImageUrlProperty);
        }

        /// <summary>
        /// Sets the ImageUrl property.  
        /// </summary>
        public static void SetImageUrl(DependencyObject d, String value)
        {
            d.SetValue(ImageUrlProperty, value);
        }

        #endregion
    }
}