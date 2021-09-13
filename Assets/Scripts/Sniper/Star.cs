using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Minigame.Sniper;

namespace Minigame.Sniper
{
    public class Star : MonoBehaviour
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
                //TODO :: 애니메이션
                GameManager.instance.starCount++;
                Debug.Log("[Star] 현재 획득한 별 개수 : " + GameManager.instance.starCount);
                Destroy(this.gameObject);
            }
        }
    }
}