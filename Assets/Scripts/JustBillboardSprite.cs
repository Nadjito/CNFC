using System;
using UnityEngine;
    public class JustBillboardSprite : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }