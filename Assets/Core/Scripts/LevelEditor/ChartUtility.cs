using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Data;

namespace LevelEditor
{
    public static class ChartUtility
    {
        public static string ChartToFile(ChartInfo chart)
        {
            var json = JsonUtility.ToJson(chart);
            return json;
        }

        public static ChartInfo FileToChart(string json)
        {
            var chart = JsonUtility.FromJson<ChartInfo>(json); //TODO: convert fail? json is null?
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

        public static string ReadTextFromFile(string filePath)
        {
            string text = null;
            using (StreamReader reader = new StreamReader(filePath))
            {
                text = reader.ReadToEnd();
                reader.Close();
            }

            return text;
        }

        public static AudioClip GetAudioClip(DirectoryInfo chartPath, string musicFileName)
        {
            var path = chartPath.Parent.ToString();
            //switch to relativly file path
            var audioAssetPath = path.Substring(path.IndexOf("Resources\\") + "Resources\\".Length);
            //delete the ".mp3" suffix
            if (musicFileName.LastIndexOf(".") != -1) musicFileName = musicFileName.Substring(0, musicFileName.LastIndexOf("."));
            audioAssetPath += "/" + musicFileName;
            audioAssetPath = audioAssetPath.Replace("\\", "/");
            //Debug.Log("load from: " + audioAssetPath);
            var music = Resources.Load<AudioClip>(audioAssetPath);
            return music;
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

        public static ChartInfo GetActiveChart()
        {
            if (Core.GameManager.Instance == null)
            {
                Debug.LogWarning("GameManager not Found");
                return null;
            }
            return FileToChart(ReadTextFromFile(Core.GameManager.Instance.selectChartFile.FullName));
        }
    }
}