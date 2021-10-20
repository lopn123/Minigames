using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.MinerMania
{
    public class NormalBlock : Block
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

        }

        protected override void Init()
        {
            blockType = BlockType.NORMAL;
        }

        public override void GetTouched()
        {
            hp--;
        }
    }
}