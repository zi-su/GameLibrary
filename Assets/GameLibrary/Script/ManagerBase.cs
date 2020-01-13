using UnityEngine;
using UnityEngine.ResourceManagement;
using System.Collections.Generic;
namespace GameLibrary{

    /// <summary>
    /// マネージャー基底クラス
    /// スタート時読み込みのリストと関数定義
    /// </summary>
	public class ManagerBase : MonoBehaviour{
        protected List<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle> handles = new List<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>();

        protected virtual void Start()
        {
            StartCoroutine(CoStartLoad());
        }

        protected virtual System.Collections.IEnumerator CoStartLoad()
        {
            yield return null;
        }

        protected bool IsLoading()
        {
            bool ret = true;
            foreach(var h in handles)
            {
                ret &= h.IsDone;
            }
            return ret;
        }
    }
}