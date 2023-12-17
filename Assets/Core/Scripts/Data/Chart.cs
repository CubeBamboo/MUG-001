using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class Chart
    {
        public string musicFileName;
        public StoredNoteData[] notesArray;
        
        public string songName;
        public float bpm;
        public float offset;

        public Chart(string musicFileName, StoredNoteData[] notesArray)
        {
            this.musicFileName = musicFileName;
            this.notesArray = notesArray;
        }

        public Chart(string musicFileName, StoredNoteData[] notesArray, string songName, float bpm, float offset)
        {
            this.songName = songName;
            this.bpm = bpm;
            this.offset = offset;

            this.musicFileName = musicFileName;
            this.notesArray = notesArray;
        }
    }
}