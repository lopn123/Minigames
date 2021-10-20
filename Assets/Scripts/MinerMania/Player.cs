using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.MinerMania
{
    public class Player : MonoBehaviour
    {
        private RaycastHit2D hitInfo;

        private Vector2 targetPos;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Touch();
        }

        private void Touch()
        {
            if(Input.GetMouseButtonDown(0))
            {
                targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hitInfo = Physics2D.Raycast(targetPos, Vector2.zero, 0f);
                
                if(hitInfo.collider != null)
                {
                    if (hitInfo.collider.GetComponent<Block>() != null)
                    {
                        var block = hitInfo.collider.GetComponent<Block>();
                        block.GetTouched();
                    }
                }


                Move();
            }
        }

        private void Move()
        {
            transform.position = targetPos;
        }
    }
}