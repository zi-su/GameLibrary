using UnityEngine;
using UnityEditor;
namespace GameLibrary{
	public class CheckDependency : Editor{
		[MenuItem("Assets/GameLibrary/CheckDependency")]
        static void Check()
        {
            var obj = Selection.activeObject;
            var path = AssetDatabase.GetAssetPath(obj);
            var dependencies = AssetDatabase.GetDependencies(path);
            string s = "";
            foreach(var d in dependencies)
            {
                s += d + System.Environment.NewLine;
            }
            Debug.Log(s);
        }
	}
}