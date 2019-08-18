using System;

namespace ThreadProfilerLogger
{
    public readonly struct ThreadProfilerWithName
    {
        public readonly string name;
        public readonly ThreadProfiler threadProfiler;

        public ThreadProfilerWithName(string name, ThreadProfiler threadProfiler)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.threadProfiler = threadProfiler ?? throw new ArgumentNullException(nameof(threadProfiler));
        }
    }
}
