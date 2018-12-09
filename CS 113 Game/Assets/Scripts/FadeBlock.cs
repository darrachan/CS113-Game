using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlock : MonoBehaviour {

    public float fadeSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Color color = this.GetComponent<SpriteRenderer>().material.color;
        color.a -= Time.deltaTime * fadeSpeed;
        if (color.a < -0.035)
            Destroy(gameObject);
        this.GetComponent<SpriteRenderer>().material.color = color;
    }


}
