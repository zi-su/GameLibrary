using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map001 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var ui = GameLibrary.UIManager.Instance();
        var f = ui.Fade();
        if(f != null)
        {
            f.FadeIn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
