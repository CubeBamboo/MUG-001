using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

namespace Playing
{
    public class JudgeEffect : MonoBehaviour
    {
        private TextMeshProUGUI judgeText;
        private Image judgePanelEffect;
        //private DG.Tweening.Core.TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> tween;

        private void Start()
        {
            InitGameObject();
        }

        private void InitGameObject()
        {
            var go = transform.Find(Common.Constant.JUDGE_TEXT_NAME);
            if (go == null) Debug.LogError("JudgeText not found");
            judgeText = go.GetComponent<TextMeshProUGUI>();

            go = transform.Find(Common.Constant.JUDGE_PANEL_EFFECT_NAME);
            if (go == null) Debug.LogError("JudgePanelEffect not found");
            judgePanelEffect = go.GetComponent<Image>();
        }

        public void PlayJudge(HitJudge judge)
        {
            judgeText.DOKill();
            judgePanelEffect.DOKill();

            judgeText.text = judge.ToString();
            judgeText.color = new Color(1, 1, 1, 1);
            judgeText.DOFade(endValue: 0f, duration: 1f);

            judgePanelEffect.DOColor(endValue: new Color(1, 1, 1, 0.1f), duration: 0.1f).OnComplete(
                () => { judgePanelEffect.DOColor(endValue: new Color(1, 1, 1, 0f), duration: 1f); });
        }
    }
}