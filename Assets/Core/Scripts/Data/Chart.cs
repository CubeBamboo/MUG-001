using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class Chart
    {
        public string musicFileName;
        public StoredNoteData[] notesArray;
        
        public string songName, artist;
        public string BGFileName;
        public float bpm;
        public float offset;

        public Chart(string songName, string musicFileName, StoredNoteData[] notesArray)
        {
            this.songName = songName;
            this.musicFileName = musicFileName;
            this.notesArray = notesArray;
        }

        public Chart(string songName, string musicFileName, StoredNoteData[] notesArray, float bpm, float offset)
        {
            this.songName = songName;
            this.bpm = bpm;
            this.offset = offset;

            this.musicFileName = musicFileName;
            this.notesArray = notesArray;
        }
    }
}