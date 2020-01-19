using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameLibrary.AssetReferenceTable referenceTable;
    // Start is called before the first frame update
    void Start()
    {
        //Playerオブジェクトが存在してない場合に生成する
        var p = GameObject.FindGameObjectWithTag("Player");
        if(p == null)
        {
            var assetRef = referenceTable.GetAssetReference(AssetReferencePlayer.ID.PLAYER);
            var ope = assetRef.InstantiateAsync();
            ope.Completed += Ope_Completed;
        }
        else
        {
            p.GetComponent<Player>().Warp(transform.localPosition);
        }
    }

    private void Ope_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        if(obj.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            obj.Result.transform.position = transform.position;
            DontDestroyOnLoad(obj.Result);
        }
    }
}
