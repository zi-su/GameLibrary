using UnityEngine;
using UnityEditor;
namespace GameLibrary{
	public class AssertEditorWindow : EditorWindow{
        string myString = "Hello World";
        bool groupEnabled;
        bool myBool = true;
        float myFloat = 1.23f;
        static int count = 0;
        static float diff = 20.0f;
        // Add menu named "My Window" to the Window menu
        static public void Show(string assertMessage)
        {
            // Get existing open window or if none, make a new one:
            var window = CreateInstance<AssertEditorWindow>();
            window.titleContent = new GUIContent("AssertWindow");
            window.myString = assertMessage;
            //window.position = new Rect(Screen.width / 2.0f, Screen.height / 2.0f, 400, 400);
            Debug.Log(window.position);
            window.Show();
            window.position = new Rect(Screen.width / 2.0f + count * diff, Screen.height / 2.0f + count * diff, 400, 400);
            count++;
        }

        void OnGUI()
        {
            GUI.backgroundColor = Color.red;
            EditorGUILayout.HelpBox(myString, MessageType.Error);
        }

        private void OnDestroy()
        {
            AssertEditorWindow.count--;
        }
    }
}