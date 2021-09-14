using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.GrowFish
{
    enum MoveDirection
    {
        Left,
        Right
    }

    public class Fish : MonoBehaviour
    {
        private MoveDirection moveDirection;

        public int minSpeed;
        public int maxSpeed;
        protected int speed;

        [SerializeField]
        protected float spawnLocationX;
        protected Vector2 screenPos;

        [HideInInspector]
        public int level;
        
        private float spawnPosX;

        private void OnEnable()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            IsOutCam();
        }

        private void Init()
        {
            SetPos();
            SetSpeed();
            SetLevel();
        }

        private void SetPos()
        {
            moveDirection = (MoveDirection)UnityEngine.Random.Range(0, Enum.GetValues(typeof(MoveDirection)).Length + 1);
            float y = UnityEngine.Random.Range(0, Screen.height);

            if (moveDirection == MoveDirection.Left)
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width + spawnLocationX, y));
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
            else
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector2(-spawnLocationX, y));
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
            screenPos = Camera.main.WorldToScreenPoint(transform.position);
            spawnPosX = Mathf.Abs(screenPos.x);
        }

        private void SetSpeed()
        {
            speed = UnityEngine.Random.Range(minSpeed, maxSpeed + 1);
        }

        private void Move()
        {
            if (moveDirection == MoveDirection.Left)
            {
                transform.Translate(0, speed * Time.deltaTime, 0); // Rotation 0 0 0 ¸ÂÃß¸é Translate(x, value, z) value -> x
            }
            else
            {
                transform.Translate(0, -speed * Time.deltaTime, 0);
            }
        }

        private void SetLevel()
        {
            level = UnityEngine.Random.Range(1, 6); //1 ~ 5
            
            transform.localScale = new Vector2(level, level);

            switch(level)
            {
                case 1:
                    GetComponent<SpriteRenderer>().color = Color.white;
                    break;
                case 2:
                    GetComponent<SpriteRenderer>().color = Color.gray;
                    break;
                case 3:
                    GetComponent<SpriteRenderer>().color = Color.black;
                    break;
                case 4:
                    GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case 5:
                    GetComponent<SpriteRenderer>().color = Color.magenta;
                    break;
            }
        }

        private void IsOutCam()
        {
            screenPos = Camera.main.WorldToScreenPoint(transform.position);
            
            if (screenPos.x > Screen.width + spawnPosX || screenPos.x < -spawnPosX)
            {
                Delete();
            }
        }

        public void Delete()
        {
            ObjectPool.instance.ReturnObject(this.gameObject);
        }
    }
}