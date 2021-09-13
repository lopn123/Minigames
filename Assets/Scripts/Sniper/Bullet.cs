using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Sniper
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private Rigidbody2D rb;

        #region Vector2
        [HideInInspector]
        public Vector2 mousePosition;
        private Vector2 startPos;
        private Vector2 direction;
        private Vector2 screenPos;
        #endregion

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            startPos = transform.position;
            direction = mousePosition - startPos;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            Launch(direction.normalized, speed);
        }

        private void Update()
        {
            IsOutCam();
        }

        private void IsOutCam()
        {
            screenPos = Camera.main.WorldToScreenPoint(transform.position);

            if (screenPos.x > Screen.width || screenPos.x < 0 || screenPos.y > Screen.height || screenPos.y < 0)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Wall"))
            {
                transform.rotation = Quaternion.Inverse(transform.rotation);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Launch(Vector2 Direction, float Speed)
        {
            rb.AddForce(Direction * Speed);
        }
    }
}