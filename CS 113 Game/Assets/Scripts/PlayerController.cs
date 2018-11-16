using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public float speed;
    public float jump;
    public float fallThreshold;
    float moveVelocity;
    bool inAir = true;
    bool dead = false;
    bool touchedEnemy = false;
    float startTime = 0;
    float distance = 0;
    Vector2 originalPos;

    public AudioClip jumpSound;
    public AudioClip fallSound;
    AudioSource audioSource;

    public Collider2D penguin;
    public Collider2D crystal;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        originalPos = new Vector2(transform.position.x, transform.position.y);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
            startTime = Time.time;
            anim.SetTrigger("MakeBlink");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!inAir)
            {
                audioSource.PlayOneShot(jumpSound, 0.5F);
                Debug.Log((Time.time - startTime).ToString("00.00.00"));
                distance = (Time.time - startTime) * speed;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + distance, jump);
                anim.SetTrigger("MakeIdle");
            }
        }
        
        if (inAir)
        {
            Debug.Log(transform.position.y);
            if (transform.position.y < fallThreshold && !dead)
            {
                audioSource.PlayOneShot(fallSound, 0.5F);
                dead = true;
            }

            if (dead && transform.position.y < fallThreshold - 5)
            {
                OnDead();
            }
      
        }

        if (penguin.IsTouching(crystal) && !dead)
        {
            audioSource.PlayOneShot(fallSound, 0.5F);
            OnDead();
           
        }
    }

    void OnTriggerEnter2D()
    {
        inAir = false;
    }

    void OnTriggerExit2D()
    {
        inAir = true;
    }

    void OnDead()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        transform.position = originalPos;
        dead = false;
        inAir = true;
    }

}

//
//		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
//      {
//          if (grounded)
//           {
//GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
//                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
//          }
//       }

            /*
        if (penguin.IsTouching(crystal) && !dead)
        {
            anim.SetTrigger("MakeDead");
            audioSource.PlayOneShot(fallSound, 0.5F);
            //transform.position = originalPos;
            dead = true;

        }*/
   //     if (dead && transform.position.y > fallThreshold - 5)
  //      {
   //         grounded = false;
   //         dead = false;
   //     }