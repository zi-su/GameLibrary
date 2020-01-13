using UnityEngine;
using UnityEngine.Profiling;
namespace GameLibrary{
	public class Profiler : MonoBehaviour{
        GUIStyle style = new GUIStyle();
        string monoHeapSizeFormat = "MonoHeapSize:{0}";
        string monoUsedSizeFormat = "MonoUsedSize:{0}";
        string gpuAllocatedSizeFormat = "GraphicsMemory:{0}";
        string totalReserverdSizeFormat = "totalReserverdSize:{0}";
        string totalAllocatedSizeFormat = "totalAllocatedSize:{0}";

        private void Start()
        {
            style.fontSize = 40;
        }
        static public void BeginSample(string name)
        {
            UnityEngine.Profiling.Profiler.BeginSample(name);
        }

        static public void EndSample(string name)
        {
            UnityEngine.Profiling.Profiler.EndSample();
        }

        private void OnGUI()
        {
            long monoHeapSize = UnityEngine.Profiling.Profiler.GetMonoHeapSizeLong();
            long monoUsedSize = UnityEngine.Profiling.Profiler.GetMonoUsedSizeLong();
            long totalReserverdSize = UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong();
            long totalAllocatedSize = UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong();
            long gpuAllocatedSize= UnityEngine.Profiling.Profiler.GetAllocatedMemoryForGraphicsDriver();
            
            //GUI.BeginGroup(new Rect(Screen.width - width, 200, width, 200));
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            GUILayout.Label(string.Format(monoHeapSizeFormat, monoHeapSize),style);
            GUILayout.Label(string.Format(monoUsedSizeFormat, monoUsedSize),style);
            GUILayout.Label(string.Format(gpuAllocatedSizeFormat, gpuAllocatedSize), style);
            GUILayout.Label(string.Format(totalReserverdSizeFormat, totalReserverdSize),style);
            GUILayout.Label(string.Format(totalAllocatedSizeFormat, totalAllocatedSize),style);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            //GUI.EndGroup();
        }

        [RuntimeInitializeOnLoadMethod]
        static void RuntimeInitialize()
        {
            var go = new GameObject("Profiler", typeof(Profiler));
            DontDestroyOnLoad(go);
        }
    }
}