using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playing
{
    public class OneGameDataMgr : Framework.MonoSingletons<OneGameDataMgr>
    {
        private int perfectCnt, greatCnt, missCnt;

        private void Start()
        {
            GameInit();

            ChartPlayer.Instance.onPerfectJudge += () => { AddJudge(HitJudge.PERFECT); };
            ChartPlayer.Instance.onGreatJudge += () => { AddJudge(HitJudge.GREAT); };
            ChartPlayer.Instance.onMissJudge += () => { AddJudge(HitJudge.MISS); };
        }

        private void OnDestroy()
        {
            ChartPlayer.Instance.onPerfectJudge -= () => { AddJudge(HitJudge.PERFECT); };
            ChartPlayer.Instance.onGreatJudge -= () => { AddJudge(HitJudge.GREAT); };
            ChartPlayer.Instance.onMissJudge -= () => { AddJudge(HitJudge.MISS); };
        }

        private void GameInit()
        {
            perfectCnt = 0; greatCnt = 0; missCnt = 0 ;
        }

        public void AddJudge(HitJudge judge)
        {
            switch(judge)
            {
                case HitJudge.PERFECT:
                    perfectCnt++; break;
                case HitJudge.GREAT:
                    greatCnt++; break;
                case HitJudge.MISS:
                    missCnt++; break;
                default:
                    Debug.LogError("Judge Error");
                    break;
            }
        }

        public (int, int, int) GetJudgeData()
        {
            return (perfectCnt, greatCnt, missCnt);
        }

    }
}