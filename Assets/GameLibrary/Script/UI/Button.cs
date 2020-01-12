﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameLibrary{
	public class Button : MonoBehaviour{
        [SerializeField] Image frame = null;
        System.Action clickAction = null;

        private void Start()
        {
            frame.enabled = false;
        }

        public void SetClickAction(System.Action action)
        {
            clickAction = action;
        }
        public void Select()
        {
            frame.enabled = true;
        }

        public void Deselect()
        {
            frame.enabled = false;
        }

        public void Click()
        {
            clickAction?.Invoke();
        }

        private void OnDestroy()
        {
            clickAction = null;
        }
    }
}