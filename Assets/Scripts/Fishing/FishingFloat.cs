using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Fishing
{
    public class FishingFloat : MonoBehaviour
    {
        [SerializeField]
        private int speed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetTouch();
        }

        private void GetTouch()
        {
            if(Input.GetMouseButton(0))
            {
                var a = transform.parent.gameObject;
                transform.position = Vector2.MoveTowards(transform.position, a.transform.position, speed * Time.deltaTime);
            }
        }
    }
}