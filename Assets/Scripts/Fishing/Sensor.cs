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
            if (collision.CompareTag("Player"))
            {
                fish.detectEvent(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                fish.detectEvent(false);
            }
        }
    }
}