using UnityEngine;
using UnityEngine.AddressableAssets;
namespace GameLibrary{
	public class SceneTransitionPoint : MonoBehaviour{
        [SerializeField] AssetReference sceneAssetRef = null;

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Debug.Log(hit.gameObject.name);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                //シーン遷移に通知
                GamePad.Instance.EnableInput(false);
                UIManager.Instance.Fade().FadeOut(Color.black, completeAction: () =>
                {
                    var h = sceneAssetRef.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Single);
                    h.Completed += H_Completed;
                });
            }
        }

        private void H_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
        {
            GamePad.Instance.EnableInput(true);
        }

        private void OnDestroy()
        {
            if (sceneAssetRef.IsValid())
            {
                sceneAssetRef.ReleaseAsset();
            }
        }
    }
}