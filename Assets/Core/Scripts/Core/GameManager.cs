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
            //TODO: ת����������ȥ
            gameEndPanel.SetActive(true);
            //Playing.OneGameDataMgr.Instance.GetJudgeData(); �����ݷ��͵�gameEndPanel
            //TODO: ת������������
        }
    }
}