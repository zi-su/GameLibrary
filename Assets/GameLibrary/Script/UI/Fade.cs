using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
public class Fade : MonoBehaviour
{
    [SerializeField] Canvas canvas = null;
    [SerializeField] Image image = null;
    float time = 0.0f;
    float elapsTime = 0.0f;
    Color color;
    Color fromColor;
    Color toColor;
    System.Action completeAction;

    enum Mode
    {
        Wait,
        FadeIn,
        FadeOut,
    }
    Mode mode = Mode.Wait;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case Mode.Wait:
                break;
            case Mode.FadeIn:
                FadeProcess();
                break;
            case Mode.FadeOut:
                FadeProcess();
                break;
            default:
                break;
        }

    }

    void FadeProcess()
    {
        elapsTime += Time.deltaTime;
        CalcColor();
        if (elapsTime > time)
        {
            completeAction?.Invoke();
            completeAction = null;
            if(mode == Mode.FadeIn)
            {
                canvas.enabled = false;
            }
            mode = Mode.Wait;
        }
    }
    public void FadeIn(float time = 1.0f)
    {
        mode = Mode.FadeIn;
        this.time = time;
        this.elapsTime = 0.0f;
        Color c = fromColor;
        c.a = 0.0f;
        fromColor = toColor;
        toColor = c;
    }
    public void FadeIn(Color fromColor, Color toColor, float time = 1.0f, System.Action completeAction = null)
    {
        canvas.enabled = true;
        this.fromColor = fromColor;
        this.toColor = toColor;
        this.time = time;
        elapsTime = 0.0f;
        mode = Mode.FadeIn;
        this.completeAction = completeAction;
        CalcColor();
    }

    public void FadeOut(Color fromColor, Color toColor, float time = 1.0f, System.Action completeAction = null)
    {
        canvas.enabled = true;
        this.fromColor = fromColor;
        this.toColor = toColor;
        this.time = time;
        elapsTime = 0.0f;
        mode = Mode.FadeOut;
        this.completeAction = completeAction;
        CalcColor();
    }

    void CalcColor()
    {
        image.color = Color.Lerp(fromColor, toColor, elapsTime / time);
    }
}
