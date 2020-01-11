using UnityEngine;

namespace GameLibrary{
	public class RuntimeInitialization : MonoBehaviour{
        [RuntimeInitializeOnLoadMethod]
		void RuntimeInitialize()
        {
            var go = new GameObject("GamePadManager", typeof(GamePad));
        }
	}
}