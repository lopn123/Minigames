using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Minigame.StackTower;

namespace Minigame.StackTower
{
    public class BlockSpanwer : MonoBehaviour
    {
        [SerializeField]
        private MovingBlock blockPrefab;

        [SerializeField]
        private MoveDirection moveDir;

        public void SpawnBlock()
        {
            var block = Instantiate(blockPrefab);

            if (MovingBlock.lastBlock != null && MovingBlock.lastBlock.gameObject != GameObject.Find("StartBlock"))
            {
                float x = moveDir == MoveDirection.X ? transform.position.x : MovingBlock.lastBlock.transform.position.x;
                float z = moveDir == MoveDirection.Z ? transform.position.z : MovingBlock.lastBlock.transform.position.z;

                block.transform.position = new Vector3(x,
                MovingBlock.lastBlock.transform.position.y + blockPrefab.transform.localScale.y,
                z);
            }
            else
            {
                block.transform.position = transform.position;
            }

            block.moveDirection = moveDir;
        }
    }
}