using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class FrontClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] RectTransform rect;
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pos = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
            if(Screen.width == 1280)
            {
                pos *= 1920.0f / 1280.0f;
            }
            rect.anchoredPosition = pos;
            Debug.Log(gameObject.name);
        }
    }

}
