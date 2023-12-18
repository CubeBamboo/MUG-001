using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChartList
{
    public class ChartListMgr : MonoBehaviour
    {
        private string chartDicPath = "Assets/Resources/Chart";
        public GameObject chartCardPrefab;
        public Transform chartCardParent;

        private void Start()
        {
            InitChartList();
        }

        private void InitChartList()
        {
            //��ȡ����Ŀ¼�����е����棬Ϊÿһ��note����һ��card Prefab
            /*TODO: some code... */

            Data.Chart[] chartArr = new Data.Chart[10];
            foreach (Data.Chart chart in chartArr)
            {
                var go = Instantiate(chartCardPrefab, chartCardParent);
                go.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = chart.songName;
                //go.GetComponent<UnityEngine.UI.Button>().onClick += 
            }
        }
    }
}