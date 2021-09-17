using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Minigame.Fishing
{
    public enum TurnState
    {
        READY_TURN,
        FISHING_TURN
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        [SerializeField]
        private FishSpawner fishSpawner;

        private TurnState nowState;

        public UnityAction<TurnState> changeStateEvent;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            //fishSpawner.SpawnStart();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AllObjectChangeState(TurnState state)
        {
            changeStateEvent.Invoke(state);
        }
    }
}