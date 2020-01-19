using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBase : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void Start()
    {
        var ui = GameLibrary.UIManager.Instance;
        var f = ui.Fade();
        if (f != null)
        {
            f.FadeIn();
        }
    }
}
