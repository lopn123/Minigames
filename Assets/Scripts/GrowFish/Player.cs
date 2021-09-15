using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.GrowFish
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private int speed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy")) //물고기
            {
                float enemyLevel = collision.gameObject.GetComponent<Fish>().level;

                if (transform.localScale.x > enemyLevel)
                {
                    collision.gameObject.GetComponent<Fish>().Delete();

                    float plusScale = enemyLevel / 10;
                    transform.localScale = new Vector2(transform.localScale.x + plusScale, transform.localScale.y + plusScale);
                    GameManager.instance.AddScore((int)enemyLevel * 10);
                }
                else if (transform.localScale.x < enemyLevel)
                {
                    GameManager.instance.GameOver();
                }
            }
        }

        private void Move()
        {
            // Rotation 0 0 0 맞추면 Translate(x, value, z) value -> x
            if (Input.GetKey(KeyCode.LeftArrow) && Camera.main.WorldToScreenPoint(transform.position).x > 0)
            {
                transform.Translate(0, speed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow) && Camera.main.WorldToScreenPoint(transform.position).x < Screen.width)
            {
                transform.Translate(0, -speed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow) && Camera.main.WorldToScreenPoint(transform.position).y < Screen.height)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow) && Camera.main.WorldToScreenPoint(transform.position).y > 0)
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
        }
    }
}