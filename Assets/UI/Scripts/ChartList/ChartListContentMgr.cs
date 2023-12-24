using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ChartList
{
    public class ChartListContentMgr : MonoBehaviour
    {
        private string chartDirePath = "Resources/Chart";
        public GameObject chartCardPrefab;
        public Transform chartCardParent;
        private FileInfo[] allChartFile;

        private void Start()
        {
            RefreshChartList();
        }

        //read .chart file from directory
        private FileInfo[] GetAllChartFiles(string direPath)
        {
            string searchPath = string.Format($"{Application.dataPath}/{direPath}"); // make some process.
            List<FileInfo> ret = new List<FileInfo>();

            if (Directory.Exists(searchPath))
            {
                DirectoryInfo dir = new DirectoryInfo(searchPath);
                //all chart dirctories
                DirectoryInfo[] directories = dir.GetDirectories("*");

                //foreach all the directory under ".../Chart/"
                foreach (DirectoryInfo d in directories)
                {
                    var chartFile = d.GetFiles("*.chart");
                    //if it's a valid chartFile.
                    if (chartFile.Length != 0)
                    {
                        //get the chartInfo
                        ret.Add(chartFile[0]);
                    }
                }

            }
            else Debug.LogWarning("Directory not Exists: " + searchPath);

            return ret.ToArray();
        }

        private void RefreshChartList()
        {
            //get all the chartFile in ChartDirectory, and create a card Prefab for every single Chart.
            allChartFile = GetAllChartFiles(chartDirePath);

            foreach (var chartFile in allChartFile)
            {
                var text = chartFile.OpenText().ReadToEnd();
                var chart = LevelEditor.ChartUtility.FileToChart(text);

                //create and update message
                var go = Instantiate(chartCardPrefab, chartCardParent);
                go.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = chart.songName;
                go.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnClickButton(chartFile)) ;
            }
        }

        private void OnClickButton(FileInfo chartFile)
        {
            Core.GameManager.Instance.selectChartFile = chartFile;
            Framework.SceneTransitionController.LoadScene(Common.Constant.CHART_PLAYER_SCENE_INDEX);
            //Debug.Log($"ClickButton: {chartFile}");
        }
    }
}