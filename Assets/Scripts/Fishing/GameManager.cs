using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Fishing
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private FishSpawner fishSpawner;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            fishSpawner.SpawnStart();
        }

        private void Init()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}