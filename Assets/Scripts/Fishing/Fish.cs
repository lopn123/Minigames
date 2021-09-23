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
        private Vector2 startPos;
        private Vector2 direction;
        private Vector3 quaternionToTarget;
        #endregion

        private Quaternion targetRotation;

        private GameObject player;

        [SerializeField]
        private int fishMoveTime;
        public int level;
        public int price;

        #region float
        [SerializeField]
        private float normalSpeed;
        [SerializeField]
        private float followSpeed;
        [SerializeField]
        private float rotSpeed;
        private float curSpeed;
        #endregion

        [HideInInspector]
        public bool isFollow;
        private bool isCatch;

        private TurnState state;

        public UnityAction<bool> detectEvent;

        [HideInInspector]
        public Sprite fishImage;

        private void Awake()
        {
            fishImage = GetComponent<SpriteRenderer>().sprite;
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
            if (!isCatch)
            {
                FollowPlayer();
                Move();
            }
        }

        private void Init()
        {
            isFollow = false;
            isCatch = false;

            curSpeed = normalSpeed;

            SetRandomPos();
            StartCoroutine(SetMoveCoroutine);
        }

        private void DetectionPlayer(bool isDetection)
        {
            if(isDetection)
            {
                isFollow = true;
                curSpeed = followSpeed;
                StopCoroutine(SetMoveCoroutine);
            }
            else
            {
                if(isFollow)
                {
                    isFollow = false;
                    curSpeed = normalSpeed;
                    StartCoroutine(SetMoveCoroutine);
                }
            }
        }

        private void SetRandomPos()
        {
            transform.position = Camera.main.ScreenToWorldPoint(
                new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height/2)));
        }

        private IEnumerator SetMovementDirection()
        {
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
            transform.position = Vector2.MoveTowards(transform.position, moveTargetPos, curSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }

        private void FollowPlayer()
        {
            if(isFollow)
            {
                moveTargetPos = player.transform.position;

                direction = player.transform.position - transform.position;

                quaternionToTarget = Quaternion.Euler(0, 0, 90) * direction;
                targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: quaternionToTarget);
            }
        }

        public void Catch()
        {
            isCatch = true;
            StopCoroutine(SetMoveCoroutine);
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