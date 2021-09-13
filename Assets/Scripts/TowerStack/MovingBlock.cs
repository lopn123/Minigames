using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minigame.StackTower
{
    public class MovingBlock : MonoBehaviour
    {
        public static MovingBlock currentBlock { get; private set; }
        public static MovingBlock lastBlock { get; private set; }

        public MoveDirection moveDirection;

        [SerializeField]
        private float speed = 1f;

        private Vector3 screenPos;

        private void OnEnable()
        {
            if (lastBlock == null)
                lastBlock = GameObject.Find("StartBlock").GetComponent<MovingBlock>();

            if (this.gameObject != GameObject.Find("StartBlock"))
                currentBlock = this;

            GetComponent<Renderer>().material.color = GetRandomColor();

            if (lastBlock.gameObject != GameObject.Find("StartBlock").gameObject)
            {
                transform.localScale = new Vector3(lastBlock.transform.localScale.x,
                lastBlock.transform.localScale.y,
                lastBlock.transform.localScale.z);
            }
        }

        // Update is called once per frame

        void Update()
        {
            Move();
            CheckOutCam();
        }

        private void Move()
        {
            if (moveDirection == MoveDirection.Z)
            {
                transform.position += transform.forward * Time.deltaTime * speed;
            }
            else
            {
                transform.position += transform.right * Time.deltaTime * speed;
            }
        }

        private void CheckOutCam()
        {
            screenPos = Camera.main.WorldToScreenPoint(transform.position);

            if(screenPos.x > Screen.width || screenPos.x < 0 || screenPos.y > Screen.height || screenPos.y < 0)
            {
                UIManager.instance.isGameOver = true;
            }
        }

        private Color GetRandomColor()
        {
            return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
        }

        internal void Stop()
        {
            speed = 0;
            float hangover = GetHangover();

            float max = moveDirection == MoveDirection.Z ? lastBlock.transform.localScale.z : lastBlock.transform.localScale.x;

            if (Mathf.Abs(hangover) >= max)
            {
                lastBlock = null;
                currentBlock = null;
                UIManager.instance.isGameOver = true;
            }

            if (lastBlock != null)
            {
                float direction = hangover > 0 ? 1f : -1f;

                if (moveDirection == MoveDirection.Z)
                {
                    SplitBlockOnZ(hangover, direction);
                }
                else
                {
                    SplitBlockOnX(hangover, direction);
                }

                lastBlock = this;
                UIManager.instance.SetScore(UIManager.instance.score + 1);
            }
        }

        private float GetHangover()
        {
            if (moveDirection == MoveDirection.Z)
            {
                return transform.position.z - lastBlock.transform.position.z;
            }
            else
            {
                return transform.position.x - lastBlock.transform.position.x;
            }
        }

        private void SplitBlockOnX(float hangover, float direction)
        {
            float newxSize = lastBlock.transform.localScale.x - Mathf.Abs(hangover);
            float fallingBlockSize = transform.localScale.x - newxSize;

            float newxPosition = lastBlock.transform.position.x + (hangover / 2);
            transform.localScale = new Vector3(newxSize, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(newxPosition, transform.position.y, transform.position.z);

            float blockEdge = transform.position.x + (newxSize / 2f * direction);
            float fallingBlockzPosition = blockEdge + fallingBlockSize / 2f * direction;

            SpawnDropBlock(fallingBlockzPosition, fallingBlockSize);
        }

        private void SplitBlockOnZ(float hangover, float direction)
        {
            float newzSize = lastBlock.transform.localScale.z - Mathf.Abs(hangover);
            float fallingBlockSize = transform.localScale.z - newzSize;

            float newzPosition = lastBlock.transform.position.z + (hangover / 2);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newzSize);
            transform.position = new Vector3(transform.position.x, transform.position.y, newzPosition);

            float blockEdge = transform.position.z + (newzSize / 2f * direction);
            float fallingBlockzPosition = blockEdge + fallingBlockSize / 2f * direction;

            SpawnDropBlock(fallingBlockzPosition, fallingBlockSize);
        }

        private void SpawnDropBlock(float fallingBlockzPosition, float fallingBlockSize)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            if (moveDirection == MoveDirection.Z)
            {
                cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
                cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockzPosition);
            }
            else
            {
                cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
                cube.transform.position = new Vector3(fallingBlockzPosition, transform.position.y, transform.position.z);
            }

            cube.AddComponent<Rigidbody>();
            cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

            Destroy(cube.gameObject, 1f);
        }
    }
}