using ICities;

namespace ThreadProfilerLogger
{
    public class Mod : LoadingExtensionBase, IUserMod
    {
        public string Name => "ThreadProfilerLogger";
        public string Description => "Logs ThreadProfiler values.";
    }
}
