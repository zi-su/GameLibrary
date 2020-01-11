using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameLibrary.GamePad.GamePadProxy proxy;
    // Start is called before the first frame update
    void Start()
    {
        proxy = GameLibrary.GamePad.Instance.CreateProxy();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(proxy.IsTrigger(GameLibrary.GamePad.ButtonType.Right));
    }
}
