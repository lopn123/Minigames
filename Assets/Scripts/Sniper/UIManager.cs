using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Minigame.Sniper;

namespace Minigame.Sniper
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject gameEndPanel;

        #region Text
        [SerializeField]
        private Text text_gameResult;
        [SerializeField]
        private Text text_starCount; //юс╫ц
        #endregion

        private void Awake()
        {
            if (gameEndPanel.activeSelf)
                gameEndPanel.SetActive(false);
        }

        public void ShowEndPanel(bool isClear)
        {
            Time.timeScale = 0;

            if(isClear)
            {
                text_gameResult.text = "Clear!";
                text_starCount.text = "Star Count : " + GameManager.instance.starCount;
            }
            else
            {
                text_gameResult.text = "Failed..";
                text_starCount.enabled = false;
            }
            gameEndPanel.SetActive(true);
        }

        public void ClickNextStage()
        {

        }

        public void ClickReStart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ClickChangeWeapone()
        {

        }
    }
}