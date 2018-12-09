using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // float camera;
    public float x;
    public float cameraSpeed;

    public static GameManager instance;
    [SerializeField]
    private GameObject player;
    [SerializeField]
// private float minX = 7f;
    private float maxX = 7f;
    private float minY = -3.7f;
    private float maxY = 1f;
    private float X;
    private float Y;
    private Vector3 originalPosition;
    private bool moving;
    
    public GameObject platform,platform2,platform3;
    // Use this for initialization

    private void Awake()
    {
        MakeInstance();
    }
    void Start () {
        //camera = Camera.main;
        // camera = Camera.main.transform.position.x;
        originalPosition = Camera.main.transform.position;
        moving = true;
    }
	
	// Update is called once per frame
	void Update () {
        // Debug.Log(cameraSpe);
        //camera.transform.position.x = newX;
        if (moving)
        {
            x += cameraSpeed;
            Camera.main.transform.position = new Vector3(x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
    }

    public void ResetCamera()
    {
        moving = false;
        Instantiate(platform3, new Vector3(-4.14f, -2.75f, 1), Quaternion.identity);
        Instantiate(platform2, new Vector3(2.02f, -1.31f, 1), Quaternion.identity);
        Instantiate(platform2, new Vector3(7.88f, -2.93f, 1), Quaternion.identity);
        maxX = 7f;
        minY = -3.7f;
        maxY = 1f;
        x = 0f;
        X = 0;
        Y = 0;
        Camera.main.transform.position = originalPosition;

    }

    public void StartMoving()
    {
        moving = true;
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    public void CreateNewPlatform()
    {
        maxX = Random.Range(maxX + 6f, maxX + 8f);

        float newY = Random.Range(-3.7f, 1f);

        if (Mathf.Abs(newY - Y) > 2.3){
            int random = Random.Range(0, 10);
            if (random <= 5)
                newY = Y + 2;
            else if (random > 5)
                newY = Y - 2;
            if (newY < -3.7f || newY > 1)
                newY = -2.75f;
        }
        

        GameObject block = pickBlock();

        X = maxX;
        Y = newY;
        Instantiate(block, new Vector3(maxX, newY, 0), Quaternion.identity);
    }

    public void CreateNewPlatformAndMove(float position)
    {
        CreateNewPlatform();
    }

    private GameObject pickBlock ()
    {
        int number = Random.Range(0, 100);
        if (number <= 20)
            return platform;
        if (number <= 70)
            return platform2;
        else
            return platform3;

    }
}
