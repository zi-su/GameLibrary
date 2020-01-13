using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement;
using UnityEngine.AddressableAssets;
namespace GameLibrary{

    /// <summary>
    /// UIマネージャ
    /// 常駐する各インスタンスを起動時に生成
    /// </summary>
	public class UIManager : ManagerBase{

        static UIManager instance;
        Fade fade = null;
        
        public Fade Fade()
        {
            return fade;
        }

        static public UIManager Instance()
        {
            return instance;
        }

        /// <summary>
        /// 常駐インスタンスの読み込み
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator CoStartLoad()
        {
            yield return new WaitWhile(()=> GameLibrary.AssetRefrenceUI.GetReferenceTable() == null);
            var refTable = GameLibrary.AssetRefrenceUI.GetReferenceTable();
            //フェード生成
            var fadeRef = refTable.GetAssetReference(AssetRefrenceUI.ID.FADE);
            var fadeOpe = fadeRef.InstantiateAsync();
            handles.Add(fadeOpe);
            fadeOpe.Completed += FadeOpe_Completed;

            //全ロード待ち
            yield return new WaitWhile(() =>
            {
                return IsLoading() == true;
            });
            
        }

        private void FadeOpe_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                fade = obj.Result.GetComponent<Fade>();
                DontDestroyOnLoad(obj.Result);
            }
        }

        /// <summary>
        /// 起動時インスタンス化
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        static void RuntimeLoad()
        {
            var go = new GameObject("UIManager", typeof(UIManager));
            DontDestroyOnLoad(go);
            instance = go.GetComponent<UIManager>();
        }
    }
}