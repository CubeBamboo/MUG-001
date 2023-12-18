using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GameManager : Framework.MonoSingletons<GameManager>
    {
        public GameObject gameEndPanel;
        
        public void OnGameEnd()
        {
            //TODO: 转场动画暗下去
            gameEndPanel.SetActive(true);
            //Playing.OneGameDataMgr.Instance.GetJudgeData(); 把数据发送到gameEndPanel
            //TODO: 转场动画亮起来
        }
    }
}