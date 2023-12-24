using Data;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace LevelEditor
{
    public class ChartEditorRenderer : MonoBehaviour
    {
        public GameObject linePrefab;
        public ChartInfo chartOnPlaying { get; private set; }

        public int selectDivide = 4;
        public int gridDistance = 1; //distance per quarter note.
        
        public float RunningTime { get; private set; }

        private void FixedUpdate()
        {
            RunningTime += Time.fixedDeltaTime;
            Refresh(RunningTime);
        }

        private void OnGUI()
        {
            GUIStyle leftTopStyle = new GUIStyle();
            leftTopStyle.normal.background = null;
            leftTopStyle.normal.textColor = Color.white;
            leftTopStyle.fontSize = 40;
            string runningDataStr = "RunningTime: " + RunningTime.ToString("0.00") + "\n"
                + $"musicSource.time = ";
            GUI.Label(new Rect(25, 25, 300, 90), runningDataStr, leftTopStyle);
        }

        private void Refresh(float time)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }


            for(int i = 1; i < 10; i++)
            {
                //CreateLine(Vector2.zero + new Vector2(gridDistance * i, 0)); // 1/1
                //CreateLine(Vector2.zero + new Vector2(gridDistance * i, 0)); // 1/2
                //CreateLine(Vector2.zero + new Vector2(gridDistance * i, 0)); // 1/4
            }
            //
            //float lastWhiteLineTime = time;
            //InitLine(null, Vector3.zero + gridDistance, )
        }

        private LineRenderer CreateLine(Vector3 position, Data.StoredNoteData posData)
        {
            var line = Instantiate(linePrefab).GetComponent<LineRenderer>();
            line.transform.SetParent(transform, false);
            line.transform.localPosition = position;



            return line;
        }

        private void SetLineColor(LineRenderer line, Color setColor)
        {
            var newColor = new Gradient();
            newColor.colorKeys = new GradientColorKey[] { new GradientColorKey(setColor, 0) };
            line.colorGradient = newColor;
        }
    }
}