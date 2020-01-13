using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
namespace GameLibrary{
    public class AssetReferenceTable : ScriptableObject {
        [SerializeField] protected List<AssetReference> assetReferences = new List<AssetReference>();

        public AssetReference GetAssetReference<T>(T id) where T : System.Enum
        {
            int index = (int)(System.Object)id;
            if (index < 0 || index > assetReferences.Count)
            {
                return null;
            }
            var assetref = assetReferences[index];
            return assetref;
        }
    }
}