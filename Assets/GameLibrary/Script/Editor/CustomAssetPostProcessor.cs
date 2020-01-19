using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
namespace GameLibrary{
    public class CustomAssetPostProcessor : AssetPostprocessor {

        void OnPreprocessTexture()
        {
            var importer = assetImporter as TextureImporter;
            if (importer.assetPath.Contains("GameLibaray/Data/UI"))
            {
                importer.textureType = TextureImporterType.Sprite;
            }
        }
        void OnPostprocessTexture(Texture2D texture)
        {
            if (assetImporter.assetPath.Contains("GameLibaray/Data/UI"))
            {
                int w = texture.width % 4;
                int h = texture.height % 4;
                if (w != 0 || h != 0)
                {
                    string n = System.IO.Path.GetFileName(assetImporter.assetPath);
                    string s = n + "は無圧縮テクスチャです" + System.Environment.NewLine;
                    if (w != 0) s += "width:" + w.ToString() + "ピクセル増やしてください" + System.Environment.NewLine;
                    if (h != 0) s += "height:" + h.ToString() + "ピクセル増やしてください" + System.Environment.NewLine;
                    Debug.Assert(false, s);
                    return;
                }
            }
        }

        void OnPostprocessSprite(Sprite sprite)
        {
        }

        void OnPreprocessModel()
        {
            var importer = assetImporter as ModelImporter;
        }

        void OnPostprocessModel(GameObject model)
        {
            var mesh = model.GetComponentsInChildren<MeshFilter>();
            foreach (var m in mesh)
            {
                List<Vector3> vertices = new List<Vector3>();
                var sharedMesh = m.sharedMesh;
                Debug.Log(m.name + ":Vertices=" + sharedMesh.vertexCount);
                if(sharedMesh.vertexCount > 10)
                {
                    AssertEditorWindow.Show("hogehoge");
                }
                for (int i = 0; i < sharedMesh.subMeshCount; i++)
                {
                    var tris = sharedMesh.GetTriangles(i);
                    Debug.Log(m.name + "Submesh=" + i.ToString() + " Triangles=" + tris.Length);
                }
                Debug.Log(m.name+":Normals="+ sharedMesh.normals.Length);
                if (sharedMesh.normals.Length > 10)
                {
                    AssertEditorWindow.Show("normals");
                }
            }
        }
    }
}