using UnityEngine;
using UnityEditor;

namespace GameLibrary{
	
    [CustomEditor(typeof(ScreenSpaceCanvasSortOrder))]
    public class CustomScreenSpaceCanvasSortOrder : Editor
    {
        public override void OnInspectorGUI()
        {
            var sortorder = target as ScreenSpaceCanvasSortOrder;
            base.OnInspectorGUI();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("SetSortOrder"))
            {
                sortorder.SetOrder();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}