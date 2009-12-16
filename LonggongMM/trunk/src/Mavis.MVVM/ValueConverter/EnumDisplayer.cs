using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using System.Collections;
using System.Resources;
using System.Windows.Markup;
using Mavis.Core;

namespace Mavis.MVVM.ValueConverter
{
    [ContentProperty("OverriddenDisplayEntries")]
    public class EnumDisplayer: IValueConverter
    {
        private Type _type;
        private IDictionary _displayValues;
        private IDictionary _reverseValues;
        private List<EnumDisplayEntry> _overriddenDisplayEntries;

        public EnumDisplayer()
        {
            
        }

        protected IDictionary DisplayValues
        {
            get
            {
                if (_displayValues == null)
                {
                    PrepareDisplayNames();
                }
                return _displayValues;
            }
        }
        

        protected IDictionary ReverseValues
        {
            get
            {
                if (_reverseValues == null)
                {
                    PrepareDisplayNames();
                }
                return _reverseValues;
            }
        }

        public EnumDisplayer(Type type)
        {
            Type = type;
        }

        public Type Type
        {
            get { return _type; }
            set
            {
                if (!value.IsEnum)
                    throw new ArgumentException("Parameter is not an Enumermated Value", "value");
                _type = value;
            }
        }

        public List<EnumDisplayEntry> OverriddenDisplayEntries
        {
            get
            {
                if (_overriddenDisplayEntries == null)
                {
                    _overriddenDisplayEntries = new List<EnumDisplayEntry>();
                }
                return _overriddenDisplayEntries;
            }
        }

        public ReadOnlyCollection<string> DisplayNames
        {
            get
            {
                return new List<string>((IEnumerable<string>) DisplayValues.Values).AsReadOnly();
            }
        }

        private void PrepareDisplayNames()
        {
            Type displayValuesType =
                    typeof(Dictionary<,>).GetGenericTypeDefinition().MakeGenericType(_type, typeof(string));
            _displayValues = (IDictionary)Activator.CreateInstance(displayValuesType);
            _reverseValues = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>)
                                                                        .GetGenericTypeDefinition()
                                                                        .MakeGenericType(typeof(string), _type));

            var fields = _type.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                var displayStringAttributes =
                    (DisplayStringAttribute[])field.GetCustomAttributes(typeof(DisplayStringAttribute), false);
                string displayString = GetDisplayStringValue(displayStringAttributes);
                object enumValue = field.GetValue(null);

                if (displayString == null)
                    displayString = GetBackupDisplayStringValue(enumValue);
                if (displayString == null) continue;
                _displayValues.Add(enumValue, displayString);
                _reverseValues.Add(displayString, enumValue);
            }
        }

        private string GetBackupDisplayStringValue(object enumValue)
        {
            if (_overriddenDisplayEntries != null && _overriddenDisplayEntries.Count > 0)
            {
                var foundEntry = _overriddenDisplayEntries.Find(entry =>
                                                                    {
                                                                        var e = Enum.Parse(_type, entry.EnumValue);
                                                                        return enumValue.Equals(e);
                                                                    });
                if (foundEntry != null)
                {
                    return foundEntry.ExcludeFromDisplay ? null : foundEntry.DisplayString;
                }
            }
            return Enum.GetName(_type, enumValue);
        }

        private string GetDisplayStringValue(DisplayStringAttribute[] attributes)
        {
            if (attributes == null || attributes.Length == 0)
                return null;

            var dsa = attributes[0];
            return ! string.IsNullOrEmpty(dsa.ResourceKey) ? new ResourceManager(_type).GetString(dsa.ResourceKey) : dsa.Value;
        }

        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DisplayValues[value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ReverseValues[value];
        }

        #endregion
    }

    public class EnumDisplayEntry
    {
        public string EnumValue { get; set; }
        public string DisplayString { get; set; }
        public bool ExcludeFromDisplay { get; set; }
    }
}