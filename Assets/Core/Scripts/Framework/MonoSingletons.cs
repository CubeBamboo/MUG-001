using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class MonoSingletons<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        //TODO: recreate if not find.

        protected virtual void Awake()
        {
            if(Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}