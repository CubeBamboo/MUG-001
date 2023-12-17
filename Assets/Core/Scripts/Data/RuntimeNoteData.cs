using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class RuntimeNoteData
    {
        public float hitTime;

        public RuntimeNoteData(float hitTime)
        {
            this.hitTime = hitTime;
        }
    }
}