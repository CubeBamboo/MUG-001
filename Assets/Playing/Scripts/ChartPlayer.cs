using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using Unity.VisualScripting;

namespace Playing
{
    public class ChartPlayer : Framework.MonoSingletons<ChartPlayer>
    {
        [Header("Setting")]

        public float missRange;     //s
        public float greatRange;    //s
        public float perfectRange;  //s
        
        public float noteSpeed;
        public float globalOffset;

        public Vector2 noteTargetPos;
        public Vector2 noteStartPos;

        //public bool isChartStart;
        public bool autoPlay;

        private bool isChartEnd;

        public Vector2 noteMoveDir => noteTargetPos - noteStartPos;

        [Header("Chart")]
        public string chartPath = "Assets/Resources/Chart/sample1";

        //for playing chart
        private Chart chartOnPlaying;
        private RuntimeNoteData[] allRuntimeNotes;
        private uint nextNoteIndex;

        [Header("System")]
        public TrackMgr track;
        public JudgeEffect judgeEffect;
        public float RunningTime { get; private set; }

        //audio
        private AudioSource musicSource;
        private AudioSource hitKeySource;
        public AudioClip hitKeyClip;

        #region UnityEventFunc

        protected override void Awake()
        {
            base.Awake();
            musicSource = gameObject.AddComponent<AudioSource>();
            hitKeySource = gameObject.AddComponent<AudioSource>();
        }

        private void Start()
        {
            //Init chart and all the note
            chartOnPlaying = GetChart(chartPath);
            allRuntimeNotes = LevelEditor.ChartUtility.StoredNoteToRuntime(chartOnPlaying.notesArray, chartOnPlaying.bpm, chartOnPlaying.offset);
            Debug.Assert(allRuntimeNotes != null);

            //Init Game and music
            nextNoteIndex = 0;
            InitMusic(chartOnPlaying.musicFileName);
            hitKeySource.clip = hitKeyClip;
            OnChartStart();
            Debug.Log("GameInit");
        }

        private void FixedUpdate()
        {
            if (isChartEnd)
                return;

            GameMainUpdate();
            
        }

        private void Update()
        {
            DebugUpdate();
        }

        private void OnGUI()
        {
            GUIStyle leftTopStyle = new GUIStyle();
            leftTopStyle.normal.background = null;
            leftTopStyle.normal.textColor = Color.white;
            leftTopStyle.fontSize = 40;
            string runningDataStr = "RunningTime: " + RunningTime.ToString("0.00") + "\n"
                + $"musicSource.time = {musicSource.time}";
            GUI.Label(new Rect(25, 25, 300, 90), runningDataStr, leftTopStyle);

            //string noteDataStr = "NoteList:\n";
            //if (allRuntimeNotes != null) {
            //    for(int i = 0; i < allRuntimeNotes.Length; i++) {
            //        noteDataStr += $"note[{i}] : {allRuntimeNotes[i].hitTime}s\n";
            //    }
            //} else {
            //    noteDataStr += "No Note Found!";
            //}
            //GUI.Box(new Rect(25, 120, 500, 800), noteDataStr, leftTopStyle);

            GUIStyle leftBottomStyle = new GUIStyle(leftTopStyle);
            leftBottomStyle.alignment = TextAnchor.LowerLeft;
            string keyTipsStr = "vKey: RunningTime += 5s\n"
                + "bKey: give PerfectJudge\n"
                + "nKey: Reload Scene\n";
            GUI.Label(new Rect(25, 60, 1920, 1080-50), keyTipsStr, leftBottomStyle);
        }

        #endregion

        #region ChartInitFunc

        private void GameMainUpdate()
        {
            //if (!isChartStart)
            //    return;

            //runningTime maintain
            RunningTime += Time.fixedDeltaTime;

            //play the chart
            PlayNextNoteDetectUpdate();

            //auto Play
            if (autoPlay) AutoPlayUpdate();
        }

        private void InitMusic(string musicFileName)
        {
            var music = Resources.Load<AudioClip>("Audio/" + musicFileName);
            musicSource.clip = music;
        }

        private void OnChartStart()
        {
            musicSource.Play();
            //isChartStart = true;
        }

        private Chart GetChart(string filePath)
        {
            string json = null;
            using (StreamReader reader = new StreamReader(filePath))
            {
                json = reader.ReadToEnd();
                reader.Close();
            }
            return LevelEditor.ChartUtility.fileToChart(json);
        }

        #endregion

        #region ChartPlayingFunc

        private bool CanPlayNextNote()
        {
            if (nextNoteIndex >= allRuntimeNotes.Length)
            {
                OnChartEnd();
                return false;
            }
            
            if(allRuntimeNotes[nextNoteIndex].hitTime - RunningTime <= noteStartPos.x / noteSpeed)
                return true;
            else return false;
        }

        private RuntimeNoteData MoveToNextNote()
        {
            if(nextNoteIndex < allRuntimeNotes.Length)
            {
                return allRuntimeNotes[nextNoteIndex++];
            }
            return null;
        }

        //Detect if we need play nextNote.
        private void PlayNextNoteDetectUpdate()
        {
            if(CanPlayNextNote())
            {
                //Debug.Log("PlayNextNote!");
                PlaySingleNote(MoveToNextNote());
            }
        }

        //play a new note
        private void PlaySingleNote(RuntimeNoteData note)
        {
            //Debug.Log("PlaySingleNote(): note=" + note);
            track.CreateNewNote(note);
        }

        //callback function on player hitting the track (key).
        public void OnHitKey()
        {
            track.OnHitKey();
            hitKeySource.Play();
        }

        public void GiveJudge(HitJudge judge)
        {
            judgeEffect.PlayJudge(judge);
        }

        #endregion

        #region ChartEndFunc

        private void OnChartEnd()
        {
            Debug.Log("Chart Play End.");
            isChartEnd = true;
        }

        #endregion

        #region DebugFunc

        private void AutoPlayUpdate()
        {
            if (track.activeNote != null && track.activeNote.noteData.hitTime - RunningTime < 0.01f)
            {
                OnHitKey();
            }
        }

        private void DebugUpdate()
        {
            var keyboard = UnityEngine.InputSystem.Keyboard.current;
            if (keyboard.dKey.wasPressedThisFrame || keyboard.fKey.wasPressedThisFrame)
            {
                OnHitKey();
            }

            if (keyboard.vKey.wasPressedThisFrame)
            {
                RunningTime += 5f;
                musicSource.time += 5f;
                //Debug.Log("musicSource.timeSamples = " + musicSource.timeSamples);
            }

            if (keyboard.bKey.wasPressedThisFrame)
            {
                GiveJudge(HitJudge.PERFECT);
            }

            if (keyboard.nKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        #endregion

    }
}