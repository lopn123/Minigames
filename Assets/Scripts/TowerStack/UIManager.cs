using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Minigame.StackTower;

namespace Minigame.StackTower
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        [SerializeField]
        private Text text_score;

        [HideInInspector]
        public int score = 0;
        [HideInInspector]
        public bool isGameOver = false;

        private void Awake()
        {
            instance = this;
            score = 0;
            isGameOver = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(CheckGameOver())
            {
                GameOver();
            }
        }

        public void ShowScoreText()
        {
            text_score.enabled = true;
        }

        public void HideScoreText()
        {
            text_score.enabled = false;
        }

        public void SetScore(int Score, Color? color = null)
        {
            score = Score;
            text_score.text = "" + score;

            if (color != null)
            {
                text_score.color = color.Value;
            }
        }

        private bool CheckGameOver()
        {
            return isGameOver;
        }

        private void GameOver()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}