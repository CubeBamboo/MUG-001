using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    [UnityEditor.MenuItem("ChartEdit/WriteToFile")]
    public static void chartWriteToFile()
    {
        var data = LevelEditor.ChartUtility.chartToFile(new Chart("3R2 - The Truth Never Spoken", new StoredNoteData[] { new StoredNoteData(0, 4, 0), new StoredNoteData(2, 4, 0), new StoredNoteData(0, 4, 1), new StoredNoteData(0, 4, 2), new StoredNoteData(0, 4, 4), new StoredNoteData(0, 4, 5), new StoredNoteData(0, 4, 6), new StoredNoteData(0, 4, 7), new StoredNoteData(0, 4, 8), new StoredNoteData(0, 4, 12), new StoredNoteData(0, 4, 16), new StoredNoteData(0, 4, 20), new StoredNoteData(0, 4, 28), new StoredNoteData(2, 4, 29), new StoredNoteData(0, 4, 31), new StoredNoteData(0, 4, 32), new StoredNoteData(0, 4, 34), new StoredNoteData(0, 4, 36), }, "rabiribi", 153, 881));
        string path = "Assets/Resources/Chart/sample1";
        LevelEditor.ChartUtility.WriteToFile(path, data);
    }
}
