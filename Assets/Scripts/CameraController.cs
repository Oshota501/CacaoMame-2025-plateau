using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private float ph = 0f ;
    void Update () {
        this.transform.position += new Vector3(Mathf.Sin(ph),0f,Mathf.Cos(ph)) ;
        ph += 0.005f ;
    }
}
