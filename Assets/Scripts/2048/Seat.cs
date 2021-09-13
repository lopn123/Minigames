using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.TwoZeroFourEight
{
    public class Seat : MonoBehaviour
    {
        public Block block;

        private void SetBlock(Block newBlock)
        {
            block = newBlock;
        }
    }
}