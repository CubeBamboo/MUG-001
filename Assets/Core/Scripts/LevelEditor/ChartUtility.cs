using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Data;

namespace LevelEditor
{
    public static class ChartUtility
    {
        public static string chartToFile(Chart chart)
        {
            var json = JsonUtility.ToJson(chart);
            return json;
        }

        public static Chart fileToChart(string json)
        {
            var chart = JsonUtility.FromJson<Chart>(json); //TODO: convert fail? json is null?
            return chart;
        }

        public static void WriteToFile(string filePath, string stringData)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(stringData);
                streamWriter.Close();
                Debug.Log("Write To File Successfully.");
            }
            //streamWriter.Flush();
        }

        public static RuntimeNoteData[] StoredNoteToRuntime(StoredNoteData[] storedNotes, float bpm, float offset = 0)
        {
            RuntimeNoteData[] res = new RuntimeNoteData[storedNotes.Length];
            for (int i = 0; i < storedNotes.Length; i++)
            {
                res[i] = new RuntimeNoteData(0f);
            }

            float globalOffset = Playing.ChartPlayer.Instance.globalOffset;
            float barInterval = 60f / bpm;
            for (int i = 0; i < storedNotes.Length; i++)
            {
                res[i].hitTime = barInterval * ((float)storedNotes[i].bar + (float)storedNotes[i].index / (float)storedNotes[i].divide);
                res[i].hitTime += (offset + globalOffset) * 0.001f;
                //Debug.Log($"res[{i}].hitTime = {res[i].hitTime}");
            }

            return res;
        }
    }
}