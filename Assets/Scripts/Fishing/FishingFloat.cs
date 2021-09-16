using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Fishing
{
    public class FishingFloat : MonoBehaviour
    {
        private Transform boatTransform;
        private Rigidbody2D rigidBody2D;

        private Fish caughtFish;

        [SerializeField]
        private float pullingSpeed;

        private bool isCatch;

        // Start is called before the first frame update
        void Start()
        {
            boatTransform = this.transform.parent.transform;
            rigidBody2D = transform.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!isCatch)
            {
                if(collision.CompareTag("Enemy")) //Fish
                {
                    isCatch = true;
                    caughtFish = collision.gameObject.GetComponent<Fish>();
                    caughtFish.Catch();
                    collision.transform.SetParent(this.transform);
                }
            }
        }

        private void Move()
        {
            if(Input.GetMouseButton(0))
            {
                transform.position = Vector2.MoveTowards(transform.position, boatTransform.position, pullingSpeed * Time.deltaTime);
                rigidBody2D.velocity = Vector2.zero;

                if((Vector2)transform.localPosition == Vector2.zero)
                {
                    if (caughtFish != null) caughtFish.Delete();
                    if (isCatch) isCatch = false;
                }
            }
        }
    }
}