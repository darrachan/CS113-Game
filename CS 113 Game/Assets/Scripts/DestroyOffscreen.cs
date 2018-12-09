using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffscreen : MonoBehaviour {

    // Use this for initialization
    public static DestroyOffscreen instance;

    private void Awake()
    {
        MakeInstance();
    }


    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnGameReset()
    {
        Destroy(gameObject);
        Debug.Log("game reset");
    }
}
