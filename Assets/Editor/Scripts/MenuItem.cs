using Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    [UnityEditor.MenuItem("ChartEdit/WriteToFile")]
    public static void chartWriteToFile()
    {
        var chart = new ChartInfo("3R2 - The Truth Never Spoken", "3R2 - The Truth Never Spoken.mp3",
            new StoredNoteData[] { new StoredNoteData(0, 4, 0), new StoredNoteData(2, 4, 0), new StoredNoteData(0, 4, 1), new StoredNoteData(0, 4, 2), new StoredNoteData(0, 4, 4), new StoredNoteData(0, 4, 5), new StoredNoteData(0, 4, 6), new StoredNoteData(0, 4, 7), new StoredNoteData(0, 4, 8), new StoredNoteData(0, 4, 12), new StoredNoteData(0, 4, 16), new StoredNoteData(0, 4, 20), new StoredNoteData(0, 4, 28), new StoredNoteData(2, 4, 29), new StoredNoteData(0, 4, 31), new StoredNoteData(0, 4, 32), new StoredNoteData(0, 4, 34), new StoredNoteData(0, 4, 36), },
            150, 881);
        chart.artist = "3R2";

        var data = LevelEditor.ChartUtility.ChartToFile(chart);
        //string filePath = $"Assets/Resources/Chart/3R2 - The Truth Never Spoken/sample1.chart";
        string filePath = $"Assets/Resources/Chart/Lena Raine - Joy of Remembrance/sample1.chart";
        LevelEditor.ChartUtility.WriteToFile(filePath, data);
    }

    [UnityEditor.MenuItem("ChartEdit/ModifyChartInfo")]
    public static void ModifyChartInfo()
    {
        string songName = "BlackBird";
        string artist = "Kobaryo";
        float bpm = 140;

        string direPath = "BlackBird/sample1.chart";
        FileInfo chartFilePath = new FileInfo($"{Application.dataPath}/Resources/Chart/{direPath}");
        ChartInfo chart = LevelEditor.ChartUtility.FileToChart(LevelEditor.ChartUtility.ReadTextFromFile(chartFilePath.FullName));
        chart.songName = songName;
        chart.artist = artist;
        chart.bpm = bpm;

        LevelEditor.ChartUtility.WriteToFile(chartFilePath.FullName, LevelEditor.ChartUtility.ChartToFile(chart));
        Debug.Log("Modify Successfully");
    }

}
