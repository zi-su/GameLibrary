using UnityEngine;
using UnityEditor;
namespace GameLibrary{
	public class CreateScriptableObject : Editor{
		[MenuItem("Assets/Scriptable/AssetReferenceTable")]
        static void CreateScriptable()
        {
            var so =  ScriptableObject.CreateInstance<AssetReferenceTable>();
            var path = System.IO.Path.Combine(AssetDatabase.GetAssetPath(Selection.activeObject), "NewScriptable.asset");
                
            AssetDatabase.CreateAsset(so, path);
        }
	}
}