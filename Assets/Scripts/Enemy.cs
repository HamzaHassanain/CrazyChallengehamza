using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float MoveTimeInOneDirection;

    private GroundCheck GroundCheck;
    private Rigidbody2D rb;
    private int dir;
    private float MoveRemaningTime;
    private bool isFliped;
    // Start is called before the first frame update
    void Start()
    {
        isFliped = false;
        rb = GetComponent<Rigidbody2D>();
        GroundCheck = GetComponentInChildren<GroundCheck>();
        MoveRemaningTime = MoveTimeInOneDirection;
        dir = -1;
    }

    private void Update()
    {
        //if (dir == -1 && isFliped)
        //{
        //    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        //    isFliped = false;
        //}
        //else if (dir == 1 && !isFliped)
        //{
        //    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        //    isFliped = true;
        //}


    }
    void FixedUpdate()
    {
        if(MoveRemaningTime > 0)
        {
            if (!GroundCheck.IsCollidnig)
            {
                Debug.Log("NotColliding");
                rb.velocity = new Vector2(0,0);
                dir *= -1;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

            }
            else
            {
                rb.velocity = new Vector2(dir * Speed * Time.fixedDeltaTime, rb.velocity.y);
                MoveRemaningTime -= Time.fixedDeltaTime;
            }
        } else
        {
            MoveRemaningTime = MoveTimeInOneDirection;
            dir *= -1;
            Debug.Log(dir);
            transform.localScale = new Vector3(transform.localScale.x * (-1 * dir), transform.localScale.y, transform.localScale.z);
        }

    }
}
