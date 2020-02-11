using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using DG.Tweening;
namespace GameLibrary{
	public class ScrollMover : MonoBehaviour{
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] VerticalLayoutGroup verticalLayoutGroup;
        RectTransform content;
        RectTransform scrollTrans;
        int index = 0;

        
        private void Start()
        {
            content = scrollRect.content as RectTransform;
            scrollTrans = scrollRect.transform as RectTransform;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveDown();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveUp();
            }
        }
        
        bool OutArea(int index)
        {
            var child = content.GetChild(index) as RectTransform;
            float top = child.localPosition.y + child.sizeDelta.y * child.pivot.y + content.localPosition.y;
            float bottom = child.localPosition.y - child.sizeDelta.y * child.pivot.y + content.localPosition.y;
            if(top > 0 || Mathf.Abs(bottom) > scrollTrans.sizeDelta.y)
            {
                return true;
            }
            return false;
        }

        public void MoveDown()
        {
            var now = content.GetChild(index) as RectTransform;
            {
                index++;
                if (index > content.childCount - 1)
                {
                    index = 0;
                }
            }
            if (OutArea(index))
            {
                //現在位置、次の位置の差分だけ移動
                var child = content.GetChild(index) as RectTransform;
                float diff = child.localPosition.y - now.localPosition.y;
                float ndiff = diff / (content.sizeDelta.y - scrollTrans.sizeDelta.y);
                float n = scrollRect.verticalNormalizedPosition + ndiff;
                //if (index == content.childCount - 1) n = 0.0f;
                n = Mathf.Clamp01(n);
                DOTween.To(() => scrollRect.verticalNormalizedPosition, x =>scrollRect.verticalNormalizedPosition= x, n, 0.2f);
            }
        }

        public void MoveUp()
        {
            var now = content.GetChild(index) as RectTransform;
            {
                index--;
                if (index < 0)
                {
                    index = content.childCount - 1;
                }
            }
            if (OutArea(index))
            {
                //現在位置、次の位置の差分だけ移動
                var child = content.GetChild(index) as RectTransform;
                float diff = child.localPosition.y - now.localPosition.y;
                float ndiff = diff / (content.sizeDelta.y - scrollTrans.sizeDelta.y);
                float n = scrollRect.verticalNormalizedPosition + ndiff;
                if (index == 0) n = 1.0f;
                n = Mathf.Clamp01(n);
                DOTween.To(() => scrollRect.verticalNormalizedPosition, x => scrollRect.verticalNormalizedPosition = x, n, 0.2f);
            }
        }
	}
}