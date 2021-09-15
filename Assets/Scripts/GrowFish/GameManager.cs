using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minigame.GrowFish
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField]
        private UIManager uiManager;

        [HideInInspector]
        public int score;
        [SerializeField]
        private int fishStartSpawnCount = 5;
        [SerializeField]
        private float fishSpawnTime = 3;

        private WaitForSeconds wait;

        private void Awake()
        {
            instance = this;

            Init();
        }

        private void Start()
        {
            StartCoroutine(SpawnFishCoroutine());

            for(int i = 0; i < fishStartSpawnCount; i++)
            {
                SpawnFish();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Init()
        {
            score = 0;

            wait = new WaitForSeconds(fishSpawnTime);
            Time.timeScale = 1;
        }

        public void SpawnFish()
        {
            ObjectPool.instance.SpawnObject();
        }

        private IEnumerator SpawnFishCoroutine()
        {
            while(true)
            {
                yield return wait;
                ObjectPool.instance.SpawnObject();
            }
        }

        public void AddScore(int Score)
        {
            score += Score;
            uiManager.SetScore(score);
        }

        public void GameOver()
        {
            uiManager.ShowEndPanel();
        }
    }
}