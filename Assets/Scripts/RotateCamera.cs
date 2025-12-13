using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public GameObject CenterObject ;

    public float Rad ;

    public float Sensibility ;

    public float CameraHight ;

    private float phase = 0f ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.Q))
        {
            this.phase += Sensibility ;
        }
        if (Input.GetKey (KeyCode.E))
        {
            this.phase -= Sensibility ;
        }
    }
    void LateUpdate()
    {
        this.transform.position = new Vector3 (
            Mathf.Sin(phase) * Rad + CenterObject.transform.position.x ,
            0f + CenterObject.transform.position.y + CameraHight,
            Mathf.Cos(phase) * Rad + CenterObject.transform.position.z
        ) ;
        // ターゲットが設定されていない場合のエラー回避
        if (CenterObject == null) return;

        // 魔法の1行：自分のZ軸（正面）をターゲットに向ける
        transform.LookAt(CenterObject.transform);
    }
}
