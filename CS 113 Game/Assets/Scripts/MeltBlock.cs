using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltBlock : MonoBehaviour {


    public float fallSpeed = 40.0f;      //How fast should the object fall
                                         // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
    }

}
