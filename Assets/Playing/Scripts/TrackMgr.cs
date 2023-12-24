using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playing
{
    /// <summary> Manage all the notes in this track. (note's position or hit...) </summary>
    public class TrackMgr : MonoBehaviour
    {
        private List<Note> allNoteGameObj;
        public Note activeNote { get; private set; }
        private float noteMovePerFrame => ChartPlayer.Instance.noteSpeed / 50f;

        //[Header("Component")]
        //public TMPro.TextMeshProUGUI text;

        [Header("Settings")]
        public GameObject notePrefab;

        public bool IsEmpty => allNoteGameObj.Count == 0;

        private void Awake()
        {
            allNoteGameObj = new List<Note>();
        }

        private void FixedUpdate()
        {
            //NoteNumUpdate(activeNote);
            NoteMoveUpadte();
            if (activeNote != null) DetectNote();
        }

        /// <summary> create a new Note in track </summary>
        public void CreateNewNote(RuntimeNoteData noteData)
        {
            //create a note.
            var note = Instantiate(notePrefab, new Vector2((noteData.hitTime-ChartPlayer.Instance.RunningTime) * ChartPlayer.Instance.noteSpeed, 0), Quaternion.identity, transform).GetComponent<Note>();
            allNoteGameObj.Add(note);
            note.noteData = noteData; //init

            //maintain activeNote.
            if (activeNote == null) SwitchActiveNote();
        }

        public void DeleteNoteAndSwitch(Note note)
        {
            if (allNoteGameObj.Contains(note)) allNoteGameObj.Remove(note);
            else Debug.LogWarning("Note not Found in deleting note.");
            Destroy(note.gameObject);

            SwitchActiveNote();
        }

        private void SwitchActiveNote()
        {
            activeNote = allNoteGameObj.Count == 0 ? null : allNoteGameObj[0];
        }

        public void OnHitKey()
        {
            if (activeNote == null)
                return;

            //already entered the Judge Range.
            var hitInterval = Mathf.Abs(activeNote.noteData.hitTime - ChartPlayer.Instance.RunningTime);
            if (hitInterval < ChartPlayer.Instance.missRange)
            {
                if (hitInterval < ChartPlayer.Instance.perfectRange) ChartPlayer.Instance.GiveJudge(HitJudge.PERFECT);
                else if (hitInterval < ChartPlayer.Instance.greatRange) ChartPlayer.Instance.GiveJudge(HitJudge.GREAT);
                else ChartPlayer.Instance.GiveJudge(HitJudge.MISS);

                DeleteNoteAndSwitch(activeNote);
                return;
            }

            //Debug.Log("not hit anything...");
            //else do nothing.
        }

        //to Detect if the active note can get a late Miss Judge.
        private void DetectNote()
        {
            if (activeNote.noteData.hitTime - ChartPlayer.Instance.RunningTime < -ChartPlayer.Instance.missRange)
            {
                ChartPlayer.Instance.GiveJudge(HitJudge.MISS);
                DeleteNoteAndSwitch(activeNote);
            }
            //else do nothing.
        }

        //update note move data
        private void NoteMoveUpadte()
        {
            foreach (var note in allNoteGameObj)
            {
                note.transform.Translate(Vector2.left * noteMovePerFrame);
            }
        }

        //private void NoteNumUpdate(Note note)
        //{
        //    return;

        //    if(note == null)
        //    {
        //        text.text = null;
        //        return;
        //    }

        //    text.text = (note.noteData.hitTime - ChartPlayer.Instance.RunningTime).ToString("0.00");
        //}
    }
    
}