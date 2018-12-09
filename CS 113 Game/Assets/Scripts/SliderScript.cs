using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{

    public AudioClip aud;
    private bool isPlaying;
    AudioSource audioSource;
    private Slider powerBar;
    private float powerBarThreshhold = 10f;
    private float powerBarValue = 0f;

    // Use this for initialization
    void Start()
    {
        isPlaying = false;
        audioSource = GetComponent<AudioSource>();
        powerBar = GetComponent<Slider>();
        powerBar.minValue = 0f;
        powerBar.maxValue = 10f;
        powerBar.value = powerBarValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ChangePower();
 //           if (!isPlaying)
//            {
//                audioSource.clip = aud;
//                audioSource.Play();
 //               isPlaying = true;
 //           }
          
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isPlaying)
            {
                isPlaying = false;
                audioSource.Stop();
            }
            powerBarValue = 0f;
            powerBar.value = powerBarValue;
        }
    }

    void ChangePower()
    {
        powerBarValue += Time.deltaTime * powerBarThreshhold;
        powerBar.value = powerBarValue;
    }
}
