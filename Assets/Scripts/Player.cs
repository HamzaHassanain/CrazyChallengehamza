using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] float PlayerHealth;
    [SerializeField] float PlayerBreath;
    [SerializeField] GameManager gameManager;
    [SerializeField] float Speed;
    [SerializeField] float JumpSpeed;
    [SerializeField] float JumpHoldTime;
    [SerializeField] float GroundCheckRadus;
    [SerializeField] Transform FeetPosetion;
    [SerializeField] LayerMask WhatIsGround;
    [SerializeField] GameObject PlayerDestroyParticles;
    [SerializeField] GameObject CoinDestroyParticles;


    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject BreathBar;

    private Slider HealthSlider;
    private Slider BreathSldier;

    private float health;
    private float breath;


    private Rigidbody2D rb;
    private Animator animator;
    private float dir;
    private float JumpRemaningTime;
    private bool isGrounded;
    private bool isJumping;

    private bool isOnSpicks;
    private bool isUnderWater;

    private bool isPlayerUnderWater;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerUnderWater = true;
        isOnSpicks = false;
        isUnderWater = false;
        breath = PlayerBreath;
        health = PlayerHealth;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        HealthSlider = HealthBar.GetComponent<Slider>();
        BreathSldier = BreathBar.GetComponent<Slider>();

        HealthSlider.maxValue = PlayerHealth;
        BreathSldier.maxValue = PlayerBreath;

        HealthSlider.value = health;
        BreathSldier.value = breath;

    }

    // Update is called once per frame
    void Update()
    {
        dir = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(dir));
        animator.SetFloat("Dir" , dir);

     
        
        isGrounded = Physics2D.OverlapCircle(FeetPosetion.position, GroundCheckRadus, WhatIsGround);
        if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            JumpRemaningTime = JumpHoldTime;
            animator.SetTrigger("Jump");
            isJumping = true;
            rb.velocity = Vector3.up * JumpSpeed;
        }

        if(Input.GetKey(KeyCode.UpArrow) && isJumping)
        {

            if(JumpRemaningTime > 0)
            {
                animator.SetTrigger("Jump");

                rb.velocity = Vector3.up * JumpSpeed;
                JumpRemaningTime -= Time.deltaTime;
            } else
            {
                animator.ResetTrigger("Jump");

                isJumping = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if(isUnderWater)
        {
            breath -= Time.fixedDeltaTime * 30;
            if(breath <= 0)
                Die();
        } else
        {
            if(breath <= PlayerBreath) 
                breath += Time.fixedDeltaTime * 40;     
        }

        if (isOnSpicks)
        {
            health -= Time.fixedDeltaTime * 50;
            if (health <= 0)
                Die();
        }
        else
        {
            if (health <= PlayerHealth)
                health += Time.fixedDeltaTime * 20;
        }

        rb.velocity = new Vector2(dir * Speed * Time.fixedDeltaTime , rb.velocity.y);

        HealthSlider.value = health;
        BreathSldier.value = breath;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Die"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("Spicks"))
        {
            isOnSpicks = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
      
        if (collision.gameObject.CompareTag("Spicks"))
        {
            isOnSpicks = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
             FindObjectOfType<AudioManager>().Play("Coin");
            Instantiate(CoinDestroyParticles , collision.transform.position , Quaternion.identity);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("water"))
        {
            if(isPlayerUnderWater)
                FindObjectOfType<AudioManager>().Play("JumpInWater");
            isUnderWater = true;
            isPlayerUnderWater = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("water"))
        {
            isUnderWater = false;
            isPlayerUnderWater = true;

        }
    }
    private void Restart()
    {
        gameManager.Restart();
    }
    private void Die()
    {
        FindObjectOfType<AudioManager>().Play("Die");
        health = 0;
        HealthSlider.value = 0;
        Destroy(gameObject);
        Instantiate(PlayerDestroyParticles, transform.position, Quaternion.identity);
        Restart();
    }
    
}
