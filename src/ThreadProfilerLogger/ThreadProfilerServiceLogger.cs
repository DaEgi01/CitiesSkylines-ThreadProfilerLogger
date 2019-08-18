using ICities;
using System.IO;
using System.Text;
using UnityEngine;

namespace ThreadProfilerLogger
{
    public class ThreadProfilerServiceLogger : ThreadingExtensionBase
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();
        private readonly string logFileName = "profile.log";

        private StreamWriter streamWriter;
        private long onUpdateIndex = 0;

        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            if (Input.GetKeyUp(KeyCode.L))
            {
                if (ThreadProfilerService.instance.IsRunning)
                {
                    EndProfiling();
                }
                else
                {
                    StartProfiling();
                }
            }

            if (!ThreadProfilerService.instance.IsRunning)
            {
                return;
            }

            var profilerItems = ThreadProfilerService.instance.ProfilerItems;
            for (int i = 0; i < profilerItems.Count; i++)
            {
                stringBuilder.Append(profilerItems[i].name);
                stringBuilder.Append(";");
                stringBuilder.Append(onUpdateIndex.ToString());
                stringBuilder.Append(";");
                stringBuilder.Append(profilerItems[i].threadProfiler.m_lastStepDuration.ToString());
                stringBuilder.Append(";");
                stringBuilder.Append(profilerItems[i].threadProfiler.m_averageStepDuration.ToString());
                stringBuilder.Append(";");
                stringBuilder.Append(profilerItems[i].threadProfiler.m_peakStepDuration.ToString());
                stringBuilder.Append(";");

                streamWriter.WriteLine(stringBuilder.ToString());

                stringBuilder.Length = 0;
            }

            onUpdateIndex++;
        }

        private void StartProfiling()
        {
            streamWriter = new StreamWriter(logFileName, append: true);
            if (!File.Exists(logFileName) || new FileInfo(logFileName).Length == 0)
            {
                streamWriter.WriteLine("Name;Index;Last step duration;Average step duration;Peak step duration");
            }
            ThreadProfilerService.instance.BeginProfilers();
        }

        private void EndProfiling()
        {
            streamWriter.Dispose();
            streamWriter = null;
            ThreadProfilerService.instance.EndProfilers();
        }
    }
}
