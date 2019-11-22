using System;
using System.Collections.Generic;
using System.Linq;
using FrameworkEx.Aspects.Invokers;

namespace FrameworkEx
{
    public static class MergeUtils
    {
        public static MergeData<T> Merge<T, TKey>
        (
            ICollection<T> source,
            ICollection<T> update,
            Func<T, TKey> keySelector,
            bool getAddItems = true,
            bool getRemoveItems = true,
            bool getUpdateItems = true)
        {
            if (!getAddItems && !getRemoveItems && !getUpdateItems)
            {
                return new MergeData<T>(new T[0], new UpdateActionArgs<T>[0], new T[0]);
            }
            var set = new Dictionary<TKey, Match<T>>();
            foreach (var item in source)
            {
                var x = item;
                set.GetOrCreate(keySelector(x), () => new Match<T>()).Do(m =>
                {
                    m.Source = x; 
                    m.SourceSet = true;
                });
            }
            foreach (var item in update)
            {
                var x = item;
                set.GetOrCreate(keySelector(x), () => new Match<T>()).Do(m =>
                {
                    m.Update = x;
                    m.UpdateSet = true;
                });
            }

            var addItems = getAddItems ? set.Where(x => !x.Value.SourceSet).SelectArray(x => x.Value.Update) : new T[0];
            var removeItems = getRemoveItems ? set.Where(x => !x.Value.UpdateSet).SelectArray(x => x.Value.Source) : new T[0];
            var updateItems = getUpdateItems ? set.Where(x => x.Value.SourceSet && x.Value.UpdateSet).SelectArray(x => new UpdateActionArgs<T>(x.Value.Source, x.Value.Update)) : new UpdateActionArgs<T>[0];
            return new MergeData<T>(addItems, updateItems, removeItems);
        }

        public static MergeData<TSource, TUpdate> Merge<TSource, TUpdate, TKey>
        (
            ICollection<TSource> source,
            ICollection<TUpdate> update,
            Func<TSource, TKey> sourceKeySelector,
            Func<TUpdate, TKey> updateKeySelector,
            bool getAddItems = true,
            bool getRemoveItems = true,
            bool getUpdateItems = true)
        {
            var addItems = getAddItems ? update.Exclude(u => source.Any(s => sourceKeySelector(s).Equals(updateKeySelector(u)))).ToArray() : new TUpdate[0];
            var removeItems = getRemoveItems ? source.Exclude(s => update.Any(u => sourceKeySelector(s).Equals(updateKeySelector(u)))).ToArray() : new TSource[0];
            var updateItems = getUpdateItems ? source.Join(update, sourceKeySelector, updateKeySelector, (s, u) => new UpdateActionArgs<TSource, TUpdate>(s, u)).ToArray() : new UpdateActionArgs<TSource, TUpdate>[0];
            return new MergeData<TSource, TUpdate>(addItems, updateItems, removeItems);
        }

        public static void Apply<TSource, TUpdate>(this MergeData<TSource, TUpdate> src,
            Action<TUpdate[]> adds = null,
            Action<UpdateActionArgs<TSource, TUpdate>[]> updates = null,
            Action<TSource[]> removes = null)
        {
            removes.DoIfNotNull(a => a(src.Remove));
            adds.DoIfNotNull(a => a(src.Add));
            updates.DoIfNotNull(a => a(src.Update));
        }

        private class Match<T>
        {
            public T Source;
            public bool SourceSet;
            public T Update;
            public bool UpdateSet;
        }
    }
    public class MergeData<T>
    {
        public T[] Add { get; private set; }
        public UpdateActionArgs<T>[] Update { get; private set; }
        public T[] Remove { get; private set; }

        public MergeData(T[] add, UpdateActionArgs<T>[] update, T[] remove)
        {
            Add = add;
            Update = update;
            Remove = remove;
        }
    }
    public class MergeData<TSource, TUpdate>
    {
        public TUpdate[] Add { get; private set; }
        public UpdateActionArgs<TSource, TUpdate>[] Update { get; private set; }
        public TSource[] Remove { get; private set; }

        public MergeData(TUpdate[] add, UpdateActionArgs<TSource, TUpdate>[] update, TSource[] remove)
        {
            Add = add;
            Update = update;
            Remove = remove;
        }
    }
    public class UpdateActionArgs<T>
    {
        public T Source { get; private set; }
        public T Update { get; private set; }

        public UpdateActionArgs(T source, T update)
        {
            Source = source;
            Update = update;
        }
    }

    public class UpdateActionArgs<TSource, TUpdate>
    {
        public TSource Source { get; private set; }
        public TUpdate Update { get; private set; }

        public UpdateActionArgs(TSource source, TUpdate update)
        {
            Source = source;
            Update = update;
        }
    }
}