﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Title : MonoBehaviour
{
    GameLibrary.GamePad.GamePadProxy proxy;

    [SerializeField] GameLibrary.Button[] buttons = new GameLibrary.Button[2];
    GameLibrary.Button selectedButton;

    [SerializeField] GameLibrary.AssetReferenceTable _refTable = null;

    UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> handle;
    bool isFinish = false;
    // Start is called before the first frame update
    void Start()
    {
        proxy = GameLibrary.GamePad.Instance.CreateProxy();
        selectedButton = buttons[0];
        selectedButton.Select();

        buttons[0].SetClickAction(() =>
        {
            handle = Addressables.InstantiateAsync(_refTable.GetAssetReference(GameLibrary.AssetRefrenceUI.ID.DIALOG));
            handle.Completed += Ope_Completed;
        });
    }

    private void Ope_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        var dialog = obj.Result.GetComponent<Dialog>();
        dialog.SetText("This is Dialog");
        dialog.SetCloseAction(()=>
        {
            var from = Color.white;
            from.a = 0.0f;
            GameLibrary.UIManager.Instance.Fade().FadeOut(Color.white, completeAction:()=>
            {
                Addressables.LoadSceneAsync("Map001");
            });
            
        });
    }


    // Update is called once per frame
    void Update()
    {
        if (isFinish)
        {
            return;
        }

        if (proxy.IsTrigger(GameLibrary.GamePad.ButtonType.KeyLeft))
        {
            selectedButton.Deselect();
            selectedButton = buttons[0];
            selectedButton.Select();
        }
        else if (proxy.IsTrigger(GameLibrary.GamePad.ButtonType.KeyRight))
        {
            selectedButton.Deselect();
            selectedButton = buttons[1];
            selectedButton.Select();
        }
        else if (proxy.IsTrigger(GameLibrary.GamePad.ButtonType.Right))
        {
            isFinish = true;
            selectedButton.Click();
        }
    }

    private void OnDestroy()
    {
        GameLibrary.GamePad.Instance.RemoveProxy(proxy);
    }
}
