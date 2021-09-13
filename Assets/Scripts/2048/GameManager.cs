using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.TwoZeroFourEight
{
    enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public class GameManager : MonoBehaviour
    {
        private Seat[,] seatArray = new Seat[4, 4];

        [SerializeField]
        private Block blockPrefab;

        [SerializeField]
        [Tooltip("생성되는 블럭이 가질 수 있는 최대값 (짝수)")]
        private int spawnBlockMaxNum = 2;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            var seats = GameObject.FindObjectsOfType<Seat>();
            int num = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    seatArray[i, j] = seats[num++];
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SpawnBlock()
        {
            int firstIndex = UnityEngine.Random.Range(0, 4);
            int secondIndex = UnityEngine.Random.Range(0, 4);

            Block newBlock = Instantiate(blockPrefab, seatArray[firstIndex, secondIndex].transform);
            
            newBlock.num = BlockSetRandomNum();

            seatArray[firstIndex, secondIndex].block = newBlock;
        }

        private int BlockSetRandomNum()
        {
            List<int> divisor = new List<int>();

            for (int i = 1; i <= spawnBlockMaxNum; i++)
            {
                if (i % 2 == 0)
                {
                    divisor.Add(i);
                }
            }

            int num = UnityEngine.Random.Range(0, divisor.Count);
            return divisor[num];
        }

        private void MoveBlock(MoveDirection direction)
        {
            switch(direction)
            {
                case MoveDirection.Up:
                    for(int i = 0; i < 3; i++)
                    {
                        for(int j = 0; j < 4; j++)
                        {
                            if(seatArray[i, j].block == null)
                            {
                                seatArray[i, j].block = seatArray[i + 1, j].block;
                                seatArray[i + 1, j].block = null;
                            }
                            else
                            {
                                if (seatArray[i, j].block.num == seatArray[i + 1, j].block.num)
                                {
                                    seatArray[i, j].block.num *= 2;
                                    seatArray[i + 1, j].block = null;
                                }
                            }
                        }
                    }
                    break;
                case MoveDirection.Down:
                    for (int i = 3; i > 0; i--)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (seatArray[i, j].block == null)
                            {
                                seatArray[i, j].block = seatArray[i - 1, j].block;
                                seatArray[i - 1, j].block = null;
                            }
                            else
                            {
                                if (seatArray[i, j].block.num == seatArray[i - 1, j].block.num)
                                {
                                    seatArray[i, j].block.num *= 2;
                                    seatArray[i - 1, j].block = null;
                                }
                            }
                        }
                    }
                    break;
                case MoveDirection.Left:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (seatArray[i, j].block == null)
                            {
                                seatArray[i, j].block = seatArray[i, j + 1].block;
                                seatArray[i, j + 1].block = null;
                            }
                            else
                            {
                                if (seatArray[i, j].block.num == seatArray[i, j + 1].block.num)
                                {
                                    seatArray[i, j].block.num *= 2;
                                    seatArray[i, j + 1].block = null;
                                }
                            }
                        }
                    }
                    break;
                case MoveDirection.Right:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 3; j > 0; j--)
                        {
                            if (seatArray[i, j].block == null)
                            {
                                seatArray[i, j].block = seatArray[i, j - 1].block;
                                seatArray[i, j - 1].block = null;
                            }
                            else
                            {
                                if (seatArray[i, j].block.num == seatArray[i, j - 1].block.num)
                                {
                                    seatArray[i, j].block.num *= 2;
                                    seatArray[i, j - 1].block = null;
                                }
                            }
                        }
                    }
                    break;
            }
        }


    }
}