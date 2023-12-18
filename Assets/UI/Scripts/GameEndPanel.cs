using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playing;
using TMPro;

namespace UILogic
{
    public class GameEndPanel : MonoBehaviour
    {
        private TextMeshProUGUI songNameTMP;
        private TextMeshProUGUI judge1CntTMP, judge2CntTMP, judge3CntTMP;

        private void Start()
        {
            InitVariable();
            InitData();
        }

        private void InitVariable()
        {
            //TODO: find fail?
            songNameTMP = transform.Find("SongTitle").GetComponent<TextMeshProUGUI>();
            var result = transform.Find("Result");
            judge1CntTMP = result.transform.Find("CntText1").GetComponent<TextMeshProUGUI>();
            judge2CntTMP = result.transform.Find("CntText2").GetComponent<TextMeshProUGUI>();
            judge3CntTMP = result.transform.Find("CntText3").GetComponent<TextMeshProUGUI>();
        }

        private void InitData()
        {
            //judgeText
            var judgeData = OneGameDataMgr.Instance.GetJudgeData();
            judge1CntTMP.text = judgeData.Item1.ToString(); judge2CntTMP.text = judgeData.Item2.ToString(); judge3CntTMP.text = judgeData.Item3.ToString();

            //songMetaData
            var chart = ChartPlayer.Instance.chartOnPlaying;
            songNameTMP.text = chart.songName;

            //TODO: image

            //Debug.Log("init Data");
        }
    }
}