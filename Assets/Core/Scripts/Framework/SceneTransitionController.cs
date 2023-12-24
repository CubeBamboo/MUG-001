using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Framework
{
    public class SceneTransitionController : MonoSingletons<SceneTransitionController>
    {
        public GameObject enterPanel, exitPanel;
        private bool isLoading;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            enterPanel = transform.Find("EnterPanel").gameObject;
            exitPanel = transform.Find("ExitPanel").gameObject;
        }

        public static void SceneTransition()
        {
            if(!Instance)
            {
                return;
            }
        }

        public static void LoadScene(int sceneBuildIndex)
        {
            if(!Instance)
            {
                SceneManager.LoadSceneAsync(sceneBuildIndex);
                return;
            }

            if (Instance.isLoading) return;
            Instance.StartCoroutine(P_LoadScene(sceneBuildIndex));
        }

        private static IEnumerator P_LoadScene(int sceneBuildIndex)
        {
            //Debug.Log("Scene Transition");
            Instance.isLoading = true;
            Instance.exitPanel.gameObject.SetActive(false);
            Instance.enterPanel.gameObject.SetActive(true);

            var asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);
            asyncLoad.allowSceneActivation = false;

            yield return new WaitForSeconds(1f); //TODO: wait for anim end.
            if(!asyncLoad.isDone) yield return null;
            
            asyncLoad.allowSceneActivation = true;
            Instance.exitPanel.gameObject.SetActive(true);
            Instance.enterPanel.gameObject.SetActive(false);

            Instance.isLoading = false;
            yield return new WaitForSeconds(1f);
            Instance.exitPanel.gameObject.SetActive(false);
        }
    }
}