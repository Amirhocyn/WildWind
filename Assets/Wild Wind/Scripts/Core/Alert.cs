using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WildWind.Core
{

    public class Alert : MonoBehaviourMaster<Alert>
    {

        public static Transform alertCenter;
        private Renderer Renderer;
        [SerializeField]
        private GameObject alertUI;
        Canvas canvas;

        public override void Start()
        {

            base.Start();

            Renderer = GetComponent<Renderer>();
            canvas = FindObjectOfType<Canvas>();
            alertUI = Instantiate(alertUI, canvas.transform);

        }

        public override void Update()
        {

            base.Update();

            UpdateAlert();

        }

        private void UpdateAlert()
        {
            if (!Renderer.isVisible)
            {

                alertUI.SetActive(true);
                Vector3 dir = transform.position - alertCenter.position;
                dir = dir.normalized;
                Vector2 line = new Vector2(dir.x, dir.z);
                Debug.DrawLine(alertCenter.position, alertCenter.position + dir);
                Vector2 position;
                if (Mathf.Abs(line.x) / Camera.main.aspect < Mathf.Abs(line.y))
                {

                    float scale = (canvas.GetComponent<RectTransform>().sizeDelta.y / 2) / Mathf.Abs(line.y);
                    position = scale * line;

                }
                else
                {

                    float scale = (canvas.GetComponent<RectTransform>().sizeDelta.x / 2) / Mathf.Abs(line.x);
                    position = scale * line;

                }

                alertUI.GetComponent<RectTransform>().localPosition = position;

            }
            else
                alertUI.SetActive(false);
        }

        public override void OnDestroy()
        {

            base.OnDestroy();

            Destroy(alertUI);

        }

    }

}
