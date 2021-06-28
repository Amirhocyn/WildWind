using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WildWind.Core
{

    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {

        private static T _instance;

        public static T Instance
        {

            get
            {

                if (_instance == null)
                {

                    _instance = GameObject.FindObjectOfType(typeof(T)) as T;

                    if (_instance == null)
                    {

                        _instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();

                    }

                }
                return _instance;
            }

        }

        private void Awake()
        {

            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                if (_instance != this)
                {
                    DestroyImmediate(this);
                    return;
                }

            }

            DontDestroyOnLoad(gameObject);

        }

    }

}
