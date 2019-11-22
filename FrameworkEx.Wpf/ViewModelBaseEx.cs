using System;
using System.Linq.Expressions;
using GalaSoft.MvvmLight;

namespace FrameworkEx.Wpf
{
    public abstract class ViewModelBaseViktor : ViewModelBase
    {
        public void Set<T>(bool condition, Action setValueToField, Expression<Func<T>> notify)
        {
            if (condition)
            {
                setValueToField();
                RaisePropertyChanged(notify);
            }
        }
        public void Set2<T, T2>(bool condition, Action setValueToField, Expression<Func<T>> notify, Expression<Func<T2>> notify2)
        {
            if (condition)
            {
                setValueToField();
                RaisePropertyChanged(notify);
                RaisePropertyChanged(notify2);
            }
        }
    }
}