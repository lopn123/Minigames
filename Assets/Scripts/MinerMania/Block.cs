using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.MinerMania
{
    public enum BlockType
    {
        NORMAL,
        UNBREAK
    }

    public abstract class Block : MonoBehaviour
    {
        protected BlockType blockType;
        [SerializeField]
        protected int hp;

        protected virtual void Update()
        {
            if(hp <= 0)
            {
                Destroy(gameObject);
            }
        }

        protected abstract void Init();
        public abstract void GetTouched();
    }
}