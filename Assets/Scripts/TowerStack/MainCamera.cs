using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Minigame.StackTower;

namespace Minigame.StackTower
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1;
        
        public void CameraMoveToOnBlock()
        {
            if (MovingBlock.lastBlock != null)
            {
                transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, transform.position.y + MovingBlock.lastBlock.transform.localScale.y, transform.position.z),
                speed);
            }
        }
    }
}