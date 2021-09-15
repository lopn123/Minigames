using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Minigame.Fishing
{
    public class Fish : MonoBehaviour
    {
        private IEnumerator SetMoveCoroutine;

        #region Vector
        [SerializeField]
        private Vector2 maxMoveValue;
        private Vector2 moveTargetPos;
        private Vector2 screenPos;
        private Vector3 quaternionToTarget;
        #endregion

        private Quaternion targetRotation;

        private GameObject player;

        [SerializeField]
        private int fishMoveTime;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float rotSpeed;
        private bool isFollow;

        public UnityAction<bool> detectEvent;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            SetMoveCoroutine = SetMovementDirection();
            detectEvent += DetectionPlayer;
        }

        private void OnEnable()
        {
            Init();
        }

        private void OnDisable()
        {
            StopCoroutine(SetMoveCoroutine);
        }

        private void Update()
        {
            FollowPlayer();
            Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject == player)
            {
                transform.SetParent(collision.transform);
            }
        }

        private void DetectionPlayer(bool isDetection)
        {
            if(isDetection)
            {
                isFollow = true;
                StopCoroutine(SetMoveCoroutine);
            }
            else
            {
                isFollow = false;
                StartCoroutine(SetMoveCoroutine);
            }
        }

        private void Init()
        {
            isFollow = false;

            SetRandomPos();
            StartCoroutine(SetMoveCoroutine);
        }

        private void SetRandomPos()
        {
            transform.position = Camera.main.ScreenToWorldPoint(
                new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
        }

        private IEnumerator SetMovementDirection()
        {
            Vector2 startPos;
            Vector2 direction;

            WaitForSeconds wait = new WaitForSeconds(fishMoveTime);

            while(true)
            {
                yield return wait;

                moveTargetPos = new Vector2(
                    Random.Range(transform.position.x - maxMoveValue.x, transform.position.x + maxMoveValue.x),
                    Random.Range(transform.position.y - maxMoveValue.y, transform.position.y + maxMoveValue.y));

                startPos = transform.position;
                direction = moveTargetPos - startPos;

                quaternionToTarget = Quaternion.Euler(0, 0, 90) * direction;
                targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: quaternionToTarget);
            }
        }

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTargetPos, speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }

        private void FollowPlayer()
        {
            if(isFollow)
            {
                moveTargetPos = player.transform.position;
            }
        }

        private void IsMapOut()
        {
            screenPos = Camera.main.WorldToScreenPoint(transform.position);

            if(screenPos.x > Screen.width)
            {
                Delete();
            }
        }

        public void Delete()
        {
            ObjectPool.instance.ReturnObject(this.gameObject);
        }
    }
}