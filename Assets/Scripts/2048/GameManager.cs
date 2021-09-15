using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private Seat[,] seatArray;
        private List<Seat> emptySeatList = new List<Seat>();

        [SerializeField]
        private int seatWidth = 4;
        [SerializeField]
        private int seatHeight = 4;

        [SerializeField]
        private Block blockPrefab;

        [SerializeField]
        [Tooltip("생성되는 블럭이 가질 수 있는 최대값")]
        private int spawnBlockMaxNum = 2;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            seatArray = new Seat[seatHeight, seatWidth];

            var seats = new List<Seat>();
            Seat seat;

            for (int i = 0; i < seatArray.Length; i++)
            {
                seat = GameObject.Find(string.Format("Seat ({0})", i)).GetComponent<Seat>();
                seats.Add(seat);
            }
            
            int num = 0;

            for (int i = 0; i < seatHeight; i++)
            {
                for (int j = 0; j < seatWidth; j++)
                {
                    seatArray[i, j] = seats[num++];
                    //Debug.LogFormat("seatArray[{0}, {1}] = {2}", i, j, seats[num - 1].name);
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
            int height = seatHeight - 1;
            int width = seatWidth - 1;
            bool isAction = false;

            switch(direction)
            {
                case MoveDirection.Up:
                    for(int i = height; i > 0; i--)
                    {
                        for(int j = 0; j < seatWidth; j++)
                        {
                            if (seatArray[i, j].block != null) //빈 자리가 아니라면 실행
                            {
                                if (seatArray[i - 1, j].block == null) //위가 빈 자리라면, 현재 블럭 위로 이동.
                                {
                                    seatArray[i - 1, j].SetBlock(seatArray[i, j].block);
                                    seatArray[i, j].SetBlock(null);

                                    if(i != height) isAction = true;
                                }
                                else //위 블럭과 현재 블럭의 숫자가 같다면 합침.
                                {
                                    if (seatArray[i, j].block.num == seatArray[i - 1, j].block.num)
                                    {
                                        seatArray[i - 1, j].block.SetNum(seatArray[i - 1, j].block.num * 2);
                                        seatArray[i, j].DestroyBlock();

                                        if(i != height) isAction = true;
                                    }
                                }
                                if(isAction) //아래 블럭 존재 시, 위로 이동 (정렬)
                                {
                                    if (seatArray[i + 1, j].block != null)
                                    {
                                        seatArray[i, j].SetBlock(seatArray[i + 1, j].block);
                                        seatArray[i + 1, j].SetBlock(null);
                                    }

                                    isAction = false;
                                }
                            }
                        }
                    }
                    break;
                case MoveDirection.Down:
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < seatWidth; j++)
                        {
                            if (seatArray[i, j].block != null)
                            {
                                if (seatArray[i + 1, j].block == null)
                                {
                                    seatArray[i + 1, j].SetBlock(seatArray[i, j].block);
                                    seatArray[i, j].SetBlock(null);

                                    if(i != 0) isAction = true;
                                }
                                else
                                {
                                    if (seatArray[i, j].block.num == seatArray[i + 1, j].block.num)
                                    {
                                        seatArray[i + 1, j].block.SetNum(seatArray[i + 1, j].block.num * 2);
                                        seatArray[i, j].DestroyBlock();

                                        if (i != 0) isAction = true;
                                    }
                                }

                                if (isAction)
                                {
                                    if (seatArray[i - 1, j].block != null)
                                    {
                                        seatArray[i, j].SetBlock(seatArray[i - 1, j].block);
                                        seatArray[i - 1, j].SetBlock(null);
                                    }

                                    isAction = false;
                                }
                            }
                        }
                    }
                    break;
                case MoveDirection.Left:
                    for (int i = 0; i < seatHeight; i++)
                    {
                        for (int j = width; j > 0; j--)
                        {
                            if (seatArray[i, j].block != null)
                            {
                                if (seatArray[i, j - 1].block == null)
                                {
                                    seatArray[i, j - 1].SetBlock(seatArray[i, j].block);
                                    seatArray[i, j].SetBlock(null);

                                    if (j != width) isAction = true;
                                }
                                else
                                {
                                    if (seatArray[i, j].block.num == seatArray[i, j - 1].block.num)
                                    {
                                        seatArray[i, j - 1].block.SetNum(seatArray[i, j - 1].block.num * 2);
                                        seatArray[i, j].DestroyBlock();

                                        if (j != width) isAction = true;
                                    }
                                }

                                if (isAction)
                                {
                                    if (seatArray[i, j + 1].block != null)
                                    {
                                        seatArray[i, j].SetBlock(seatArray[i, j + 1].block);
                                        seatArray[i, j + 1].SetBlock(null);
                                    }

                                    isAction = false;
                                }
                            }
                        }
                    }
                    break;
                case MoveDirection.Right:
                    for (int i = 0; i < seatHeight; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (seatArray[i, j].block != null)
                            {
                                if (seatArray[i, j + 1].block == null)
                                {
                                    seatArray[i, j + 1].SetBlock(seatArray[i, j].block);
                                    seatArray[i, j].SetBlock(null);

                                    if (j != 0) isAction = true;
                                }
                                else
                                {
                                    if (seatArray[i, j].block.num == seatArray[i, j + 1].block.num)
                                    {
                                        seatArray[i, j + 1].block.SetNum(seatArray[i, j + 1].block.num * 2);
                                        seatArray[i, j].DestroyBlock();

                                        if (j != 0) isAction = true;
                                    }
                                }

                                if (isAction)
                                {
                                    if (seatArray[i, j - 1].block != null)
                                    {
                                        seatArray[i, j].SetBlock(seatArray[i, j - 1].block);
                                        seatArray[i, j - 1].SetBlock(null);
                                    }

                                    isAction = false;
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