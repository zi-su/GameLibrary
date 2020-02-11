using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using DG.Tweening;
namespace GameLibrary{
	public class ScrollMover : MonoBehaviour{
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] VerticalLayoutGroup verticalLayoutGroup;
        [SerializeField] HorizontalLayoutGroup horizontalLayoutGroup;

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
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                MoveIndex(4);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                MoveIndex(0);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                MoveIndex(content.childCount - 1);
            }
        }
        
        bool OutAreaVertical(int index)
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

        bool OutAreaHorizontal(int index)
        {
            var child = content.GetChild(index) as RectTransform;
            float left = child.localPosition.x - child.sizeDelta.x * child.pivot.x + content.localPosition.x;
            float right = child.localPosition.x + child.sizeDelta.x * child.pivot.x + content.localPosition.x;
            if (left < 0 || right > scrollTrans.sizeDelta.x)
            {
                return true;
            }
            return false;
        }

        void MoveIndex(int index)
        {
            if(horizontalLayoutGroup != null)
            {
                if (OutAreaHorizontal(index))
                {
                    var now = content.GetChild(this.index) as RectTransform;
                    var child = content.GetChild(index) as RectTransform;
                    if (index < this.index)
                    {
                        //左端に映るように移動
                        float left = child.localPosition.x - child.sizeDelta.x * child.pivot.x - horizontalLayoutGroup.spacing + content.localPosition.x;
                        float n = left / (content.sizeDelta.x - scrollTrans.sizeDelta.x);
                        n = scrollRect.horizontalNormalizedPosition + n;
                        n = Mathf.Clamp01(n);
                        DOTween.To(() => scrollRect.horizontalNormalizedPosition, x => scrollRect.horizontalNormalizedPosition = x, n, 0.2f);
                        this.index = index;
                    }
                    else
                    {
                        //右端に映るように移動
                        float right = child.localPosition.x + child.sizeDelta.x * child.pivot.x + horizontalLayoutGroup.spacing + content.localPosition.x;
                        right = right - scrollTrans.sizeDelta.x;
                        float n = right / (content.sizeDelta.x - scrollTrans.sizeDelta.x);
                        n = scrollRect.horizontalNormalizedPosition + (n);
                        n = Mathf.Clamp01(n);
                        DOTween.To(() => scrollRect.horizontalNormalizedPosition, x => scrollRect.horizontalNormalizedPosition = x, n, 0.2f);
                        this.index = index;
                    }
                }
            }
            else if(verticalLayoutGroup != null)
            {
                if (OutAreaVertical(index))
                {
                    var child = content.GetChild(index) as RectTransform;
                    if (index < this.index)
                    {
                        //上端に映るように移動
                        float top = child.localPosition.y + child.sizeDelta.y * child.pivot.y + verticalLayoutGroup.spacing + content.localPosition.y;
                        float n = top / (content.sizeDelta.y - scrollTrans.sizeDelta.y);
                        n = scrollRect.verticalNormalizedPosition+ n;
                        n = Mathf.Clamp01(n);
                        DOTween.To(() => scrollRect.verticalNormalizedPosition, x => scrollRect.verticalNormalizedPosition = x, n, 0.2f);
                        this.index = index;
                    }
                    else
                    {
                        //下端に映るように移動
                        float down = child.localPosition.y - child.sizeDelta.y * child.pivot.y - verticalLayoutGroup.spacing + content.localPosition.y;
                        down = down + scrollTrans.sizeDelta.y;
                        float n = down / (content.sizeDelta.y - scrollTrans.sizeDelta.y);
                        n = scrollRect.verticalNormalizedPosition + n;
                        n = Mathf.Clamp01(n);
                        DOTween.To(() => scrollRect.verticalNormalizedPosition, x => scrollRect.verticalNormalizedPosition = x, n, 0.2f);
                        this.index = index;
                    }
                }
            }
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
            if (OutAreaVertical(index))
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
            if (OutAreaVertical(index))
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

        public void MoveLeft()
        {
            var now = content.GetChild(index) as RectTransform;
            {
                index--;
                if (index < 0)
                {
                    index = content.childCount - 1;
                }
            }
            if (OutAreaHorizontal(index))
            {
                //現在位置、次の位置の差分だけ移動
                var child = content.GetChild(index) as RectTransform;
                float diff = child.localPosition.x - now.localPosition.x;
                float ndiff = diff / (content.sizeDelta.x - scrollTrans.sizeDelta.x);
                float n = scrollRect.horizontalNormalizedPosition + ndiff;
                if (index == 0) n = 0.0f;
                n = Mathf.Clamp01(n);
                DOTween.To(() => scrollRect.horizontalNormalizedPosition, x => scrollRect.horizontalNormalizedPosition = x, n, 0.2f);
            }
        }

        public void MoveRight()
        {
            var now = content.GetChild(index) as RectTransform;
            {
                index++;
                if (index > content.childCount - 1)
                {
                    index = 0;
                }
            }
            if (OutAreaHorizontal(index))
            {
                //現在位置、次の位置の差分だけ移動
                var child = content.GetChild(index) as RectTransform;
                float diff = child.localPosition.x - now.localPosition.x;
                float ndiff = diff / (content.sizeDelta.x - scrollTrans.sizeDelta.x);
                float n = scrollRect.horizontalNormalizedPosition + ndiff;
                n = Mathf.Clamp01(n);
                DOTween.To(() => scrollRect.horizontalNormalizedPosition, x => scrollRect.horizontalNormalizedPosition = x, n, 0.2f);
            }
        }
	}
}