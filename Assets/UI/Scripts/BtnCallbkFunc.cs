using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UILogic
{
    public class BtnCallbkFunc : MonoBehaviour
    {
        #region GameEndPanel

        public void Restart()
        {
            Debug.Log("Restart");
        }

        public void ReturnToSelect()
        {
            Debug.Log("ReturnToSelect");
        }

        #endregion

        #region ChartSelect

        public void ReturnToTitle()
        {
            Debug.Log("ReturnToTitle");

        }

        #endregion
    }
}