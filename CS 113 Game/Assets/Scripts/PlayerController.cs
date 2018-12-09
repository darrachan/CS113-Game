using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public float speed;
    public float jump;
    public float fallThreshold;
    float moveVelocity;
    bool inAir = true;
    bool dead = false;
  //  bool touchedEnemy = false;
    bool didJump = false;
    bool GameOver;
    bool firstJump;
    float startTime = 0;
    float distance = 0;
    Vector3 originalPos;

    public AudioClip jumpSound;
    public AudioClip fallSound;
    AudioSource audioSource;

    public GameObject player, startPlatform;
    //public Collider2D penguin;
   // public Collider2D crystal;
    public float startX;
    public float startY;
    bool revived;
    int score;
    public Text scoreText, endScoreText, gameOverText, continueText;
    GameObject[] platforms;



    Collider2D touchedBlock;

    private void Start()
    {
        score = 0;
        GameOver = false;
        firstJump = true;
        SetScoreText();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        originalPos = new Vector3(startX, startY, 1);
        didJump = false;
        revived = true;
        endScoreText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        continueText.gameObject.SetActive(false);
        startPlatform.gameObject.SetActive(false);
    }

    void Update()
    {
     
        if (GameOver)
        {
            //Debug.Log("game over here1");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startPlatform.gameObject.SetActive(false);
           //     Debug.Log("game over here");
                ResetGame();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Space) && !inAir && !GameOver)
        {
            startTime = Time.time;
            anim.SetTrigger("MakeBlink");
            if (firstJump)
            {
                GameManager.instance.StartMoving();
                firstJump = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && !GameOver)
        {
            if (!inAir)
            {
                audioSource.PlayOneShot(jumpSound, 0.5F);
                distance = (Time.time - startTime) * speed;
                GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x + distance, jump, 1);
                anim.SetTrigger("MakeIdle");
                didJump = true;
            }
        }

        if (inAir)
        {
            //   Debug.Log(transform.position.y);
            if (transform.position.y < fallThreshold && !dead)
            {
                audioSource.PlayOneShot(fallSound, 0.5F);
                dead = true;
            }

            if (dead && transform.position.y < fallThreshold - 12)
            {
                OnDead();
            }

            if (touchedBlock != null && touchedBlock.gameObject.CompareTag("Platform"))
            {
                touchedBlock.GetComponent<MeltBlock>().enabled = true;
                touchedBlock.GetComponent<FadeBlock>().enabled = true;
                touchedBlock.GetComponent<ShakeObject>().enabled = true;

            }

            

        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        inAir = false;
        touchedBlock = target;
        if (didJump && !revived)
        {
            didJump = false;
            if (target.tag == "Platform")
            {
                if (GameManager.instance != null)
                {
                    GameManager.instance.CreateNewPlatform();
                    GameManager.instance.CreateNewPlatform();
                }
                score += 1;
                SetScoreText();
            }
        }
        revived = false;
    }

    void OnTriggerExit2D()
    {
        inAir = true;
    }

    void OnDead()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 1);
        
        dead = false;
        inAir = true;
     //   if(GameManager.instance != null)
      //      GameManager.instance.ResetCamera();
        revived = true;
        DestroyClones();
        transform.position = originalPos;
        firstJump = true;
        GameOver = true;
        ShowEndScreen();
    }

    void DestroyClones()
    {
        var clones = GameObject.FindGameObjectsWithTag("Platform");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    void SetScoreText()
    {
        scoreText.text = score.ToString();
    }

    void ShowEndScreen()
    {
        GameManager.instance.ResetCamera();
        endScoreText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        continueText.gameObject.SetActive(true);

        platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (var platform in platforms)
        {
            platform.gameObject.SetActive(false);
        }
        //player.gameObject.SetActive(false);

        var startPlat = GameObject.FindGameObjectsWithTag("Start");
        startPlatform.gameObject.SetActive(true);

        endScoreText.text = score.ToString();
        GameOver = true;
      //  Debug.Log(GameOver.ToString());
    }

    void ResetGame()
    {
        endScoreText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        continueText.gameObject.SetActive(false);

       // platforms = GameObject.FindGameObjectsWithTag("Platform");
       
        score = 0;
        SetScoreText();
        foreach (var platform in platforms)
        {
            if (platform != null)
                platform.gameObject.SetActive(true);
        }
        GameOver = false;
    }
    //  void OnTrigerEnter2D(Collider target)
    //   {
    //      if (didJump)
    //      {
    //          didJump = false;
    //          if (target.tag == "Platform")
    //          {
    //             if (GameManager.instance != null)
    //              {
    //                 GameManager.instance.CreateNewPlatformAndMove(target.transform.position.x);
    //            }
    //        }
    //    }

    // }

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