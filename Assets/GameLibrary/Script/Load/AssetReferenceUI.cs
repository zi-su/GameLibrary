using UnityEngine;
using UnityEngine.AddressableAssets;
namespace GameLibrary{
	public class AssetRefrenceUI{
		public enum ID
        {
            BUTTON,
            DIALOG,
            FADE,
        }

        static AssetReferenceTable refTable = null;
        static Fade fade = null;
        public Fade GetFade()
        {
            return fade;
        }

        static public AssetReferenceTable GetReferenceTable()
        {
            return refTable;
        }

        [RuntimeInitializeOnLoadMethod]
        static void RuntimeLoad()
        {
            var ope = Addressables.LoadAssetAsync<AssetReferenceTable>("AssetReferenceTableUI");
            ope.Completed += Ope_Completed;

        }

        private static void Ope_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<AssetReferenceTable> obj)
        {
            if(obj.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                refTable = obj.Result;
            }
        }
    }
}