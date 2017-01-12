using System;

namespace ExplorerLib
{
    public class ExplorerEventFilter
    {
        public static ExplorerEventFilter NO_FILTER = new ExplorerEventFilter();

        public const int MAX_HOPS_INFINITE = -1;
        public const String TAG_FILTER_NONE = "";

        public int MaxHops;
        public String TagFilter;

        public ExplorerEventFilter()
        {
            MaxHops = MAX_HOPS_INFINITE;
            TagFilter = TAG_FILTER_NONE;
        }
    }
}