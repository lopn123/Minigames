using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Fishing
{
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField]
        private int startSpawnCount;
        [SerializeField]
        private int spawnTime;

        private IEnumerator spawnCoroutine;
        private WaitForSeconds waitSpawnTime;

        private void Awake()
        {
            spawnCoroutine = AutoSpawn();
            waitSpawnTime = new WaitForSeconds(spawnTime);
        }

        public void SpawnStart()
        {
            for (int i = 0; i < startSpawnCount; i++)
            {
                Spawn();
            }

            StartCoroutine(spawnCoroutine);
        }

        public void Spawn()
        {
            ObjectPool.instance.SpawnObject();
        }

        public void StopCoroutine()
        {
            StopCoroutine(spawnCoroutine);
        }

        private IEnumerator AutoSpawn()
        {
            while(true)
            {
                yield return waitSpawnTime;
                Spawn();
            }
        }
    }
}