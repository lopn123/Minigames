using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Minigame.StackTower;

namespace Minigame.StackTower
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private MainCamera mainCam;

        private BlockSpanwer[] spawners;
        private BlockSpanwer currentSpawner;

        private int spawnerIndex;
        
        // Start is called before the first frame update
        private void Awake()
        {
            spawners = FindObjectsOfType<BlockSpanwer>();
        }

        // Update is called once per frame
        void Update()
        {
            ClickToBlockDown();
        }

        private void ClickToBlockDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    if (MovingBlock.currentBlock != null)
                    {
                        MovingBlock.currentBlock.Stop();
                        mainCam.CameraMoveToOnBlock();
                    }
                    if (MovingBlock.lastBlock != null)
                    {
                        spawnerIndex = spawnerIndex == 0 ? 1 : 0;
                        currentSpawner = spawners[spawnerIndex];

                        currentSpawner.SpawnBlock();
                    }
                }
            }
        }
    }
}