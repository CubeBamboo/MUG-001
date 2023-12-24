using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class MonoSingletons<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        //public static T Instance
        //{
        //    get
        //    {
        //        //if(instance == null)
        //        //{
        //        //    //instance = FindObjectOfType<T>();
        //        //    //var go = new GameObject("MonoSingleton:" + typeof(T).ToString());
        //        //    //instance = go.AddComponent<T>();
        //        //    Debug.Log("Create a new Singleton");
        //        //}
                
        //        return instance;
        //    }
        //    private set
        //    {
        //        instance = value;
        //    }
        //}

        protected virtual void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
        }
    }
}