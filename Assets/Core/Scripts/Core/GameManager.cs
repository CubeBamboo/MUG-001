using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core
{
    public class GameManager : Framework.MonoSingletons<GameManager>
    {
        public FileInfo selectChartFile;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
            if(selectChartFile == null) selectChartFile = new FileInfo(Application.dataPath + "/" + Common.Constant.DEFAULT_CHART_PATH);
#endif
        }

        public void OnGameEnd()
        {
            UIManager.Instance.gameEndPanel.SetActive(true);
            //Playing.OneGameDataMgr.Instance.GetJudgeData(); 把数据发送到gameEndPanel
        }
    }
}