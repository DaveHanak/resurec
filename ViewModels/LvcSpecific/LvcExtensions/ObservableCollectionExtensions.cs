using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.ViewModels.CustomLvc.LvcExtensions
{
    public static class ObservableCollectionExtensions
    {
        public static void Update60s(this ObservableCollection<float> collection, float value)
        {
            collection.Add(value);
            if (collection.Count > 61)
            {
                collection.RemoveAt(0);
            }
        }

        public static void Clear60s(this ObservableCollection<float> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                collection[i] = 0;
            }
        }
    }
}
