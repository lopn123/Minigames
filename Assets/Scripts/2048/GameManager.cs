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
        private List<Seat> emptySeatList = new List<Seat>();

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
            int num = seats.Length - 1;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    seatArray[i, j] = seats[num--];
                    //Debug.LogFormat("seatArray[{0}, {1}] = {2}", i, j, seats[num + 1].name);
                }
            }

            SpawnBlock();
        }

        // Update is called once per frame
        void Update()
        {
            CheckGameResult();
            DragScreen();
        }

        private void CheckGameResult()
        {
            if(emptySeatList.Count <= 0)
            {
                GameOver();
            }
        }

        private void DragScreen()
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveBlock(MoveDirection.Up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveBlock(MoveDirection.Down);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveBlock(MoveDirection.Left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveBlock(MoveDirection.Right);
            }
        }

        private void SpawnBlock()
        {
            emptySeatList.Clear();

            foreach (var seat in seatArray)
            {
                if (seat.block == null)
                {
                    emptySeatList.Add(seat);
                }
            }
            
            if(emptySeatList.Count > 0)
            {
                emptySeatList.Shuffle();

                Block newBlock = Instantiate(blockPrefab, emptySeatList[0].transform);

                newBlock.SetNum(BlockSetRandomNum());

                emptySeatList[0].SetBlock(newBlock);
                emptySeatList.RemoveAt(0);
            }
            else
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            Debug.Log("Game Over");
        }

        private int BlockSetRandomNum()
        {
            List<int> divisor = new List<int>();
            
            for (int i = 2; i <= spawnBlockMaxNum; i *= 2)
            {
                if (i % i == 0)
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
                    for(int i = 3; i > 0; i--)
                    {
                        for(int j = 0; j < 4; j++)
                        {
                            if (seatArray[i, j].block != null)
                            {
                                if (seatArray[i - 1, j].block == null) //위 블럭이 비어있다면, 현재 블럭의 숫자 위로 이동.
                                {
                                    seatArray[i - 1, j].SetBlock(seatArray[i, j].block);
                                    seatArray[i, j].SetBlock(null);
                                }
                                else //위 블럭과 현재 블럭의 숫자가 같다면 합침.
                                {
                                    if (seatArray[i, j].block.num == seatArray[i - 1, j].block.num)
                                    {
                                        seatArray[i - 1, j].block.SetNum(seatArray[i - 1, j].block.num * 2);
                                        seatArray[i, j].DestroyBlock();
                                    }
                                }
                            }
                        }
                    }
                    break;
                case MoveDirection.Down:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (seatArray[i, j].block != null)
                            {
                                if (seatArray[i + 1, j].block == null)
                                {
                                    seatArray[i + 1, j].SetBlock(seatArray[i, j].block);
                                    seatArray[i, j].SetBlock(null);
                                }
                                else
                                {
                                    if (seatArray[i, j].block.num == seatArray[i + 1, j].block.num)
                                    {
                                        seatArray[i + 1, j].block.SetNum(seatArray[i + 1, j].block.num * 2);
                                        seatArray[i, j].DestroyBlock();
                                    }
                                }
                            }
                        }
                    }
                    break;
                case MoveDirection.Left:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 3; j > 0; j--)
                        {
                            if (seatArray[i, j].block != null)
                            {
                                if (seatArray[i, j - 1].block == null)
                                {
                                    seatArray[i, j - 1].SetBlock(seatArray[i, j].block);
                                    seatArray[i, j].SetBlock(null);
                                }
                                else
                                {
                                    if (seatArray[i, j].block.num == seatArray[i, j - 1].block.num)
                                    {
                                        seatArray[i, j - 1].block.SetNum(seatArray[i, j - 1].block.num * 2);
                                        seatArray[i, j].DestroyBlock();
                                    }
                                }
                            }
                        }
                    }
                    break;
                case MoveDirection.Right:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (seatArray[i, j].block != null)
                            {
                                if (seatArray[i, j + 1].block == null)
                                {
                                    seatArray[i, j + 1].SetBlock(seatArray[i, j].block);
                                    seatArray[i, j].SetBlock(null);
                                }
                                else
                                {
                                    if (seatArray[i, j].block.num == seatArray[i, j + 1].block.num)
                                    {
                                        seatArray[i, j + 1].block.SetNum(seatArray[i, j + 1].block.num * 2);
                                        seatArray[i, j].DestroyBlock();
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            SpawnBlock();
        }
    }
}