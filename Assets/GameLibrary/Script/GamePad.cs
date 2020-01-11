using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLibrary{
    public class GamePad : MonoBehaviour {
        
        public enum ButtonType
        {
            Left,
            Right,
            Up,
            Down
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
            public void Clear()
            {
                leftStick = Vector2.zero;
                rightStick = Vector2.zero;
                buttonLeft = false;
                buttonRight = false;
                buttonUp = false;
                buttonDown = false;
            }

            public void Backup(GamePadData current)
            {
                leftStick = current.leftStick;
                rightStick = current.rightStick;
                buttonLeft = current.buttonLeft;
                buttonRight = current.buttonRight;
                buttonUp = current.buttonUp;
                buttonDown = current.buttonDown;

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
                p.Backup();
                p.Update();
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

        [RuntimeInitializeOnLoadMethod]
        static void RuntimeInitialize()
        {
            var go = new GameObject("GamePadManager", typeof(GamePad));
            instance = go.GetComponent<GamePad>();
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