using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class ChartInfo
    {
        public string musicFileName;
        public StoredNoteData[] notesArray; //TODO: switch to RuntimeNoteData[]
        
        public string songName, artist;
        public string BGFileName;
        public float bpm;
        public float offset;

        public ChartInfo(string songName, string musicFileName, StoredNoteData[] notesArray)
        {
            this.songName = songName;
            this.musicFileName = musicFileName;
            this.notesArray = notesArray;
        }

        public ChartInfo(string songName, string musicFileName, StoredNoteData[] notesArray, float bpm, float offset)
        {
            this.songName = songName;
            this.bpm = bpm;
            this.offset = offset;

            this.musicFileName = musicFileName;
            this.notesArray = notesArray;
        }
    }
}