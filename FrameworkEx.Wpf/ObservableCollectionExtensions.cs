using System.Collections.ObjectModel;

namespace FrameworkEx.Wpf
{
    public static class ObservableCollectionExtensions
    {
        public static void Set<T>(this ObservableCollection<T> src, T[] value)
        {
            src.Clear();
            foreach (var x in value) src.Add(x);
        }
    }
}