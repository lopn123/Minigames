using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Minigame.Fishing
{
    public class Fish : MonoBehaviour
    {
        private IEnumerator SetMoveCoroutine;

        [SerializeField]
        private Vector2 maxMoveValue;
        private Vector2 moveTargetPos;

        private GameObject player;

        [SerializeField]
        private int fishMoveTime;
        [SerializeField]
        private int speed;
        private float angle;
        private bool isFollow;

        public UnityAction<bool> detectEvent;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            detectEvent += DetectionPlayer;
        }

        private void OnEnable()
        {
            Init();
        }

        private void Update()
        {
            FollowPlayer();
            Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                //TODO :: ³¬½Ë´ë °É¸²
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

            SetMoveCoroutine = SetMovementDirection();
            StartCoroutine(SetMoveCoroutine);
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

                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTargetPos, speed * Time.deltaTime);
        }

        private void FollowPlayer()
        {
            if(isFollow)
            {
                moveTargetPos = player.transform.position;
            }
        }

        public void Delete()
        {
            ObjectPool.instance.ReturnObject(this.gameObject);
        }
    }
}