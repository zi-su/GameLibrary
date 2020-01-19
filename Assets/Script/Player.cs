using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameLibrary.GamePad.GamePadProxy gamePad;
    Transform trans;
    [SerializeField] float speedRate = 1.0f;
    [SerializeField] CharacterController characterController = null;
    // Start is called before the first frame update
    void Start()
    {
        gamePad = GameLibrary.GamePad.Instance.CreateProxy();
        trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        var ls = gamePad.GetStick(GameLibrary.GamePad.StickType.Left) * speedRate;
        characterController.SimpleMove(new Vector3(ls.x, 0.0f, ls.y));
    }

    public void Warp(Vector3 pos)
    {
        characterController.enabled = false;
        transform.localPosition = pos;
        characterController.enabled = true;
    }
}
