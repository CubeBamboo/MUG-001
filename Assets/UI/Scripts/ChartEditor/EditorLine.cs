using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class EditorLine : MonoBehaviour
    {
        public LineRenderer Line { get; private set; }
        public Data.StoredNoteData positionData;

        private void Awake()
        {
            Line = GetComponent<LineRenderer>();
        }

        public float getOffsetScale()
        {
            return positionData.bar + ((float)positionData.index / positionData.divide);
        }
    }
}