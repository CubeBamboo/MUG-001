using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using System.Reflection;

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
        private FileInfo chartFile;

        //for playing chart
        public ChartInfo chartOnPlaying { get; private set; }
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

        private bool IsNoteListEmpty => nextNoteIndex >= allRuntimeNotes.Length;

        //Action
        public event System.Action onPerfectJudge, onGreatJudge, onMissJudge;

        #region UnityEventFunc

        protected override void Awake()
        {
            base.Awake();
            musicSource = gameObject.AddComponent<AudioSource>();
            hitKeySource = gameObject.AddComponent<AudioSource>();
        }

        private void Start()
        {
            chartFile = Core.GameManager.Instance.selectChartFile;
            
            //get and resolve the chart to get all the note.
            chartOnPlaying = LevelEditor.ChartUtility.GetActiveChart();
            allRuntimeNotes = LevelEditor.ChartUtility.StoredNoteToRuntime(chartOnPlaying.notesArray, chartOnPlaying.bpm, chartOnPlaying.offset); Debug.Assert(allRuntimeNotes != null);

            //Init Game and music
            nextNoteIndex = 0;
            InitMusic();
            hitKeySource.clip = hitKeyClip;

            //GameStart
            OnChartStart();
            Debug.Log("GameStart");
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
            PlayChartDetectUpdate();

            //auto Play
            if (autoPlay) AutoPlayUpdate();
        }

        private void InitMusic()
        {
            var music = LevelEditor.ChartUtility.GetAudioClip(new DirectoryInfo(chartFile.FullName), chartOnPlaying.musicFileName);
            musicSource.clip = music;
        }

        private void OnChartStart()
        {
            musicSource.Play();
            //isChartStart = true;
        }

        #endregion

        #region ChartPlayingFunc

        private bool CanPlayNextNote()
        {
            if (nextNoteIndex >= allRuntimeNotes.Length)
                return false;
            
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
        private void PlayChartDetectUpdate()
        {
            if(CanPlayNextNote())
            {
                //Debug.Log("PlayNextNote!");
                PlaySingleNote(MoveToNextNote());
            }
            else if(IsNoteListEmpty && track.IsEmpty)
            {
                OnChartEnd();
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
            //hit determine
            track.OnHitKey();
            //hit effect
            hitKeySource.Play();
        }

        public void GiveJudge(HitJudge judge)
        {
            judgeEffect.PlayJudge(judge);

            switch (judge)
            {
                case HitJudge.PERFECT:
                    onPerfectJudge?.Invoke();
                    break;
                case HitJudge.GREAT:
                    onGreatJudge?.Invoke();
                    break;
                case HitJudge.MISS:
                    onMissJudge?.Invoke();
                    break;
                default:
                    Debug.LogError("Judge Error");
                    break;
            }
        }

        #endregion

        #region ChartEndFunc

        private void OnChartEnd()
        {
            Debug.Log("Chart Play End.");
            isChartEnd = true;
            Core.GameManager.Instance.OnGameEnd();
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
                Framework.SceneTransitionController.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        #endregion

    }
}