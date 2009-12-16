using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace Mavis.Core
{
    public class EditableObject : ValidatableObject, IEditableObject
    {
        private Dictionary<string, object> _savedState;

        #region Implementation of IEditableObject

        public virtual void BeginEdit()
        {
            OnBeginEdit();
            _savedState = GetFiledValues();
        }


        public virtual void EndEdit()
        {
            OnEndEdit();
            _savedState = null;
        }

        public virtual void CancelEdit()
        {
            OnCancelEdit();
            RestoreFieldValues(_savedState);
            _savedState = null;
        }



        protected virtual void OnBeginEdit()
        {

        }

        protected virtual void OnEndEdit()
        {

        }

        protected virtual void OnCancelEdit()
        {

        }

        /// <summary>
        /// clone the object.
        /// NOTE: this is for cloning the fields that could be edited by the UI,
        /// NOTE: so we just clone the "public" and "non_indexed" and "non_inherited" properties
        /// </summary>
        /// <returns></returns>
        protected virtual Dictionary<string, object> GetFiledValues()
        {
            return
                GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Select(fi  => new { Key = fi.Name, Value = fi.GetValue(this, null) })
                    .ToDictionary(k => k.Key, k => k.Value);
        }

        /// <summary>
        /// restore the object from the cloned object.
        /// </summary>
        /// <param name="fieldValues"></param>
        protected virtual void RestoreFieldValues(Dictionary<string, object> fieldValues)
        {
            foreach (var fieldInfo in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                object value;
                if (fieldValues.TryGetValue(fieldInfo.Name, out value))
                {
                    fieldInfo.SetValue(this, value, null);
                    //GetType().GetProperties(BindingFlags.Public)
                }
                else
                {
                    Debug.WriteLine("Failed to restore field " + fieldInfo.Name + " from cloned values, field not found in the Disctionary");
                }
            }
        }

        #endregion
    }
}