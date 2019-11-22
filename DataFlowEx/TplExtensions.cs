using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FrameworkEx
{
    public static class TplExtensions
    {
        public static void Post<T>(this ITargetBlock<T> block, T item, TimeSpan delay)
        {
            Observable.Timer(delay).Subscribe(_ => new Task(() => block.Post(item)).Start());
        }
    }
}