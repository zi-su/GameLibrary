using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _text = null;

    GameLibrary.GamePad.GamePadProxy proxy;
    System.Action closeAction;

    public void SetCloseAction(System.Action action)
    {
        closeAction = action;
    }
    public void SetText(string text)
    {
        _text.text = text;
    }

    // Start is called before the first frame update
    void Start()
    {
        proxy = GameLibrary.GamePad.Instance.CreateProxy();
    }

    // Update is called once per frame
    void Update()
    {
        if (proxy.IsTrigger(GameLibrary.GamePad.ButtonType.Right))
        {
            Close();
        }
    }

    void Close()
    {
        closeAction?.Invoke();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameLibrary.GamePad.Instance.RemoveProxy(proxy);
    }
}
