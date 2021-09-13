using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Minigame.Sniper;

namespace Minigame.Sniper
{
    public class Enemy : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Bullet"))
            {
                //TODO :: 사망 애니메이션
                GameManager.instance.enemyCount--;
                Debug.Log("[Enemy] 현재 남은 적 수 : " + GameManager.instance.enemyCount);
                Destroy(this.gameObject);
            }
        }
    }
}