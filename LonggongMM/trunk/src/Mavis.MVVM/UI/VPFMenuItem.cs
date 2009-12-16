using System;
using System.Collections.Generic;

namespace Mavis.MVVM
{
    public class VPFMenuItem
    {
        public string Text { get; set; }
        public string IconUrl { get; set; }
        public List<VPFMenuItem> Children { get; private set; }
        public DelegateCommand Command { get; set; }

        public string Key { get; private set; }

        public VPFMenuItem(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            Key = key;
            Children = new List<VPFMenuItem>();
        }
    }
}