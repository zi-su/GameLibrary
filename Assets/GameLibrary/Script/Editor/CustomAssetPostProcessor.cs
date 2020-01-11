using System;
using UnityEngine;
using UnityEditor;
namespace GameLibrary{
    public class CustomAssetPostProcessor : AssetPostprocessor{

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
    }
}