using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.TwoZeroFourEight
{
    public class Seat : MonoBehaviour
    {
        public Block block { get; private set; }

        public void SetBlock(Block newBlock)
        {
            if(newBlock != null)
            {
                block = newBlock;
                block.transform.SetParent(this.transform);
                block.transform.localPosition = Vector3.zero;
            }
            else
            {
                block = newBlock;
            }
        }

        public void DestroyBlock()
        {
            Destroy(block.gameObject);
            block = null;
        }
    }
}