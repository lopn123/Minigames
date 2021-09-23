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

        public TurnState nowState;

        public UnityAction<TurnState> changeStateEvent;

        public int money;

        private void Awake()
        {
            instance = this;
            changeStateEvent += (state) => {
                Debug.Log("State Changed. NowState : " + state.ToString());
                nowState = state;
            };
        }

        private void Start()
        {
            money = 0;
            changeStateEvent.Invoke(TurnState.READY_TURN);
            fishSpawner.SpawnStart();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GetMoney(int value)
        {
            money += value;
            UIManager.instance.uiCommon.SetText_Money(value);
        }
    }
}