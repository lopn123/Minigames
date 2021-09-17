using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Fishing
{
    public class FishingFloat : MonoBehaviour
    {
        private Transform fishingRodTransform;
        private Rigidbody2D rb2D;

        private Fish caughtFish;
        [HideInInspector]
        public TurnState state;

        [SerializeField]
        private float pullingSpeed;
        [SerializeField]
        private float throwingSpeed = 100f;
        [SerializeField]
        private float readyTurnGravity;
        [SerializeField]
        private float FishingTurnGravity;
        private float speed;
        private float curTime;

        private bool isCatch;
        private bool isPressed;

        // Start is called before the first frame update
        void Start()
        {
            fishingRodTransform = this.transform.parent.transform;

            rb2D = transform.GetComponent<Rigidbody2D>();

            state = TurnState.READY_TURN;
            TurnState_Init();
            GameManager.instance.changeStateEvent += ChangeState;
        }

        // Update is called once per frame
        void Update()
        {
            TurnState_Update();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(state == TurnState.READY_TURN)
            {
                if(collision.gameObject.layer == LayerMask.NameToLayer("Water"))
                {
                    GameManager.instance.AllObjectChangeState(TurnState.FISHING_TURN);
                }
            }
            else if(state == TurnState.FISHING_TURN)
            {
                if (!isCatch)
                {
                    if (collision.CompareTag("Enemy")) //Fish
                    {
                        isCatch = true;
                        caughtFish = collision.gameObject.GetComponent<Fish>();
                        caughtFish.Catch();
                        collision.transform.SetParent(this.transform);
                    }
                }
            }
        }

        private void TurnState_Init()
        {
            switch(state)
            {
                case TurnState.READY_TURN:
                    READY_Init();
                    break;
                case TurnState.FISHING_TURN:
                    FISHING_Init();
                    break;
            }
        }

        private void TurnState_Update()
        {
            switch (state)
            {
                case TurnState.READY_TURN:
                    READY_Update();
                    break;
                case TurnState.FISHING_TURN:
                    FISHING_Update();
                    break;
            }
        }

        #region STATE_READY
        private void READY_Init()
        {
            curTime = 0;
            isPressed = false;
            rb2D.gravityScale = 0;
        }

        private void READY_Update()
        {
            if (isPressed)
            {
                curTime += Time.deltaTime;
                if (curTime >= 3) ButtonUp_Casting();
            }
        }

        public void ButtonDown_Casting()
        {
            isPressed = true;
        }

        public void ButtonUp_Casting()
        {
            if(isPressed)
            {
                isPressed = false;

                READY_Move();
            }
        }

        private void READY_Move()
        {
            rb2D.gravityScale = readyTurnGravity;

            if (curTime < 1) curTime = 1;

            speed = curTime * throwingSpeed;
            rb2D.AddForce(Vector2.one * speed);
        }
        #endregion

        #region STATE_FISHING
        private void FISHING_Init()
        {
            rb2D.velocity = Vector2.zero;
            rb2D.gravityScale = FishingTurnGravity;

            if(isCatch) isCatch = false;

            if(caughtFish != null)
            {
                caughtFish.transform.SetParent(ObjectPool.instance.transform);
                caughtFish = null;
            }
        }

        private void FISHING_Update()
        {
            FISHING_Move();
        }

        private void FISHING_Move()
        {
            if (Input.GetMouseButton(0))
            {
                transform.position = Vector2.MoveTowards(transform.position, fishingRodTransform.position, pullingSpeed * Time.deltaTime);
                rb2D.velocity = Vector2.zero;

                if ((Vector2)transform.localPosition == Vector2.zero)
                {
                    if (caughtFish != null) caughtFish.Delete();
                    if (isCatch) isCatch = false;
                }
            }
        }
        #endregion

        private void ChangeState(TurnState newState)
        {
            state = newState;
            TurnState_Init();
        }
    }
}