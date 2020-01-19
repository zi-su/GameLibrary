using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLibrary{
    public class GamePad : MonoBehaviour {
        bool isEnable = true;
        public enum ButtonType
        {
            Left,
            Right,
            Up,
            Down,
            KeyLeft,
            KeyRight,
            KeyUp,
            KeyDown,
        }

        public enum StickType
        {
            Left,
            Right,
        }

        class GamePadData{
            public Vector2 leftStick;
            public Vector2 rightStick;

            public bool buttonLeft;
            public bool buttonRight;
            public bool buttonUp;
            public bool buttonDown;

            public bool keyLeft;
            public bool keyRight;
            public bool keyUp;
            public bool keyDown;

            public void Clear()
            {
                leftStick = Vector2.zero;
                rightStick = Vector2.zero;
                buttonLeft = false;
                buttonRight = false;
                buttonUp = false;
                buttonDown = false;
                keyLeft = false;
                keyRight = false;
                keyUp = false;
                keyDown = false;

            }

            public void Backup(GamePadData current)
            {
                leftStick = current.leftStick;
                rightStick = current.rightStick;
                buttonLeft = current.buttonLeft;
                buttonRight = current.buttonRight;
                buttonUp = current.buttonUp;
                buttonDown = current.buttonDown;

                keyLeft = current.keyLeft;
                keyRight = current.keyRight;
                keyUp = current.keyUp;
                keyDown = current.keyDown;

            }
            public void Update()
            {
                leftStick.x = Input.GetAxis("LeftHorizontal");
                leftStick.y = Input.GetAxis("LeftVertical");

                rightStick.x = Input.GetAxis("RightHorizontal");
                rightStick.y = Input.GetAxis("RightVertical");

                buttonLeft = Input.GetButton("ButtonLeft");
                buttonRight = Input.GetButton("ButtonRight");
                buttonUp = Input.GetButton("ButtonUp");
                buttonDown = Input.GetButton("ButtonDown");

                keyLeft = Input.GetAxis("KeyHorizontal") < 0.0f;
                keyRight = Input.GetAxis("KeyHorizontal") > 0.0f;
                keyUp = Input.GetAxis("KeyVertical") < 0.0f;
                keyDown = Input.GetAxis("KeyVertical") > 0.0f;
            }
        }

        public class GamePadProxy
        {
            GamePadData prev = new GamePadData();
            GamePadData current = new GamePadData();

            public void Clear()
            {
                prev.Clear();
                current.Clear();
            }

            public void Backup()
            {
                prev.Backup(current);
            }
            public void Update()
            {
                current.Update();
            }

            public bool IsTrigger(ButtonType bt)
            {
                bool ret = false;
                switch (bt)
                {
                    case ButtonType.Left:
                        ret = current.buttonLeft && !prev.buttonLeft;
                        break;
                    case ButtonType.Right:
                        ret = current.buttonRight && !prev.buttonRight;
                        break;
                    case ButtonType.Up:
                        ret = current.buttonUp && !prev.buttonUp;
                        break;
                    case ButtonType.Down:
                        ret = current.buttonDown && !prev.buttonDown;
                        break;
                    case ButtonType.KeyLeft:
                        ret = current.keyLeft && !prev.keyLeft;
                        break;
                    case ButtonType.KeyRight:
                        ret = current.keyRight && !prev.keyRight;
                        break;
                    case ButtonType.KeyUp:
                        ret = current.keyUp && !prev.keyUp;
                        break;
                    case ButtonType.KeyDown:
                        ret = current.keyDown && !prev.keyDown;
                        break;
                    default:
                        break;
                }
                return ret;
            }

            public Vector2 GetStick(StickType st)
            {
                Vector2 ret = Vector2.zero;
                switch (st)
                {
                    case StickType.Left:
                        ret = current.leftStick;
                        break;
                    case StickType.Right:
                        ret = current.rightStick;
                        break;
                    default:
                        break;
                }
                return ret;
            }
        }

        List<GamePadProxy> gamePadProxies = new List<GamePadProxy>();
        private void Update()
        {
            if(gamePadProxies.Count > 0)
            {
                var p = gamePadProxies[gamePadProxies.Count - 1];
                if (isEnable)
                {
                    p.Backup();
                    p.Update();
                }
                else
                {
                    p.Clear();
                }
            }
        }

        public GamePadProxy CreateProxy()
        {
            var p = new GamePadProxy();
            foreach(var g in gamePadProxies)
            {
                g.Clear();
            }
            gamePadProxies.Add(p);
            return p;
        }

        public void RemoveProxy(GamePadProxy p)
        {
            gamePadProxies.Remove(p);
        }

        public void EnableInput(bool enable)
        {
            isEnable = enable;
        }

        [RuntimeInitializeOnLoadMethod]
        static void RuntimeInitialize()
        {
            var go = new GameObject("GamePadManager", typeof(GamePad));
            instance = go.GetComponent<GamePad>();
            DontDestroyOnLoad(go);
        }


        static GamePad instance = null;
        static public GamePad Instance
        {
            get
            {
                return instance;
            }
        }
    }
}