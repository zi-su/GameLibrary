using UnityEngine;

namespace GameLibrary{
	public class ScreenSpaceCanvasSortOrder : MonoBehaviour{
        enum Order
        {
            Default = 0,
            Fade = 100,
        }

        [SerializeField] Order order = Order.Default;
        private void Start()
        {
            var c = GetComponent<Canvas>();
            c.sortingOrder = (int)order;
        }
        
        public void SetOrder()
        {
            var c = GetComponent<Canvas>();
            c.sortingOrder = (int)order;
        }
	}
}