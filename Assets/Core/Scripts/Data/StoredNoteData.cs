using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class StoredNoteData
    {
        public uint index, divide; //divide 1 beat into ${divide} parts. the note is in the No.${index}.
        public uint bar; //a bar of music

        public StoredNoteData(uint index, uint divide, uint bar)
        {
            this.bar = bar;
            this.index = index;
            this.divide = divide;
        }
    }
}