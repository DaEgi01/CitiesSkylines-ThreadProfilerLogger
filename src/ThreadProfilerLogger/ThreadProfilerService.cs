using ColossalFramework;
using System.Collections.Generic;

namespace ThreadProfilerLogger
{
    public class ThreadProfilerService : Singleton<ThreadProfilerService>
    {
        public ThreadProfilerService()
        {
            AddAllThreadProfilers();
        }

        private void AddAllThreadProfilers()
        {
            AddProfilerIfNotNull("AudioManager", AudioManager.instance?.GetSimulationProfiler());
            AddProfilerIfNotNull("BuildingManager", BuildingManager.instance?.GetSimulationProfiler());
            AddProfilerIfNotNull("CitizenManager", CitizenManager.instance?.GetSimulationProfiler());
            AddProfilerIfNotNull("CoverageManager", CoverageManager.instance?.GetSimulationProfiler());
            AddProfilerIfNotNull("DisasterManager", DisasterManager.instance?.GetSimulationProfiler());
            AddProfilerIfNotNull("DistrictManager", DistrictManager.instance?.GetSimulationProfiler());
            AddProfilerIfNotNull("EconomyManager", EconomyManager.instance?.GetSimulationProfiler());
            AddProfilerIfNotNull("EffectManager", EffectManager.instance?.GetSimulationProfiler());
            AddProfilerIfNotNull("ElectricityManager", ElectricityManager.instance?.GetSimulationProfiler());
            AddProfilerIfNotNull("EventManager", EventManager.instance?.GetSimulationProfiler());
            AddProfilerIfNotNull("GameAreaManager", GameAreaManager.instance?.GetSimulationProfiler());
            AddProfilerIfNotNull("GuideManager", GuideManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("ImmaterialResourceManager", ImmaterialResourceManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("InfoManager", InfoManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("InstanceManager", InstanceManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("MessageManager", MessageManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("NaturalResourceManager", NaturalResourceManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("NetManager", NetManager.instance.GetSimulationProfiler());
            var pathfinds = PathManager.instance.gameObject.GetComponents<PathFind>();
            for (int i = 0; i < pathfinds.Length; i++)
            {
                var pathFindProfiler = pathfinds[i]?.m_pathfindProfiler;
                if (pathFindProfiler != null)
                {
                    AddProfilerIfNotNull("PathFind_" + i.ToString(), pathFindProfiler);
                }
            }
            AddProfilerIfNotNull("PathManager", PathManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("PropManager", PropManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("RenderManager", RenderManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("SimulationManager", SimulationManager.instance.m_simulationProfiler);
            AddProfilerIfNotNull("StatisticsManager", StatisticsManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("TerrainManager", TerrainManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("WaterSimulation", TerrainManager.instance?.WaterSimulation?.m_waterProfiler);
            AddProfilerIfNotNull("ToolManager", ToolManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("TransferManager", TransferManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("TransportManager", TransportManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("TreeManager", TreeManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("UnlockManager", UnlockManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("VehicleManager", VehicleManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("WaterManager", WaterManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("WeatherManager", WeatherManager.instance.GetSimulationProfiler());
            AddProfilerIfNotNull("ZoneManager", ZoneManager.instance.GetSimulationProfiler());
        }

        private void AddProfilerIfNotNull(string profilerName, ThreadProfiler threadProfiler)
        {
            if (threadProfiler != null)
            {
                ProfilerItems.Add(new ThreadProfilerWithName(profilerName, threadProfiler));
            }
        }

        public List<ThreadProfilerWithName> ProfilerItems { get; } = new List<ThreadProfilerWithName>(38);
        public bool IsRunning { get; private set; } = false;

        public void BeginProfilers()
        {
            for (int i = 0; i < ProfilerItems.Count; i++)
            {
                ProfilerItems[i].threadProfiler.BeginStep();
            }

            IsRunning = true;
        }

        public void ContinueProfilers()
        {
            for (int i = 0; i < ProfilerItems.Count; i++)
            {
                ProfilerItems[i].threadProfiler.ContinueStep();
            }

            IsRunning = true;
        }

        public void PauseProfilers()
        {
            for (int i = 0; i < ProfilerItems.Count; i++)
            {
                ProfilerItems[i].threadProfiler.PauseStep();
            }

            IsRunning = false;
        }

        public void EndProfilers()
        {
            for (int i = 0; i < ProfilerItems.Count; i++)
            {
                ProfilerItems[i].threadProfiler.EndStep();
            }

            IsRunning = false;
        }
    }
}
