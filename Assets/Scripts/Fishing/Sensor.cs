using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Fishing
{
    public class Sensor : MonoBehaviour
    {
        private Fish fish;

        private void Awake()
        {
            fish = transform.parent.GetComponent<Fish>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(GameManager.instance.nowState == TurnState.FISHING_TURN)
            {
                if (collision.CompareTag("Player") && collision.GetComponent<FishingFloat>().level >= fish.level)
                {
                    fish.detectEvent(true);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(GameManager.instance.nowState == TurnState.READY_TURN)
            {
                if(collision.CompareTag("Player") && fish.isFollow)
                {
                    fish.detectEvent(false);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (GameManager.instance.nowState == TurnState.FISHING_TURN)
            {
                if (collision.CompareTag("Player") && fish.isFollow)
                {
                    fish.detectEvent(false);
                }
            }
        }
    }
}