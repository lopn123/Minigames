using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Minigame.GrowFish
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject gameEndPanel;

        [SerializeField]
        private Text text_score;

        private void Awake()
        {
            if (gameEndPanel.activeSelf)
                gameEndPanel.SetActive(false);
        }

        public void SetScore(int score, Color? color = null)
        {
            text_score.text = "" + score;

            if (color != null)
            {
                text_score.color = color.Value;
            }
        }

        public void ShowEndPanel()
        {
            Time.timeScale = 0;
            
            gameEndPanel.SetActive(true);
        }

        public void ClickReStart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}