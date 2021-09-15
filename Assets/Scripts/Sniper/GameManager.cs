using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Sniper
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField]
        private UIManager uiManager;

        #region Bullet
        public Queue<GameObject> bullets = new Queue<GameObject>();
        [SerializeField]
        private Transform bulletStorage;
        [SerializeField]
        private GameObject bulletPrefab;
        [SerializeField]
        private int bulletCount;
        #endregion

        [HideInInspector]
        public int enemyCount;
        [HideInInspector]
        public int starCount;
        private bool isGameEnd;

        private void Awake()
        {
            instance = this;

            Init();
        }
        
        // Update is called once per frame
        void Update()
        {
            Fire();

            if (!isGameEnd && CheckEnd())
            {
                GameEnd();
            }
        }

        private void Init()
        {
            isGameEnd = false;

            for (int i = 0; i < bulletCount; i++)
            {
                var bullet = Instantiate(bulletPrefab, bulletStorage);
                bullets.Enqueue(bullet);
            }
            uiManager.SetBulletCountText(bulletCount);

            if(GameObject.FindObjectsOfType<Enemy>() != null)
            {
                enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;
            }

            starCount = 0;
            
            Time.timeScale = 1;
        }

        private void Fire()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if(bullets.Count > 0)
                {
                    GameObject bullet = bullets.Dequeue();
                    bullet.GetComponent<Bullet>().mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    bullet.SetActive(true);
                    uiManager.SetBulletCountText(bullets.Count);
                }
            }
        }

        private bool CheckEnd()
        {
            return (bullets.Count <= 0 && GameObject.FindGameObjectsWithTag("Bullet").Length <= 0) || enemyCount <= 0;
        }

        private void GameEnd()
        {
            isGameEnd = true;

            if(enemyCount <= 0)
            {
                uiManager.ShowEndPanel(true);
            }
            else
            {
                uiManager.ShowEndPanel(false);
            }
        }
    }
}