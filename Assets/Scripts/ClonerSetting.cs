using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonerSetting : MonoBehaviour
{
    public Transform CloneParent ;
    public GameObject[] canCloneObj ;
    // Start is called before the first frame update
    void Start()
    {
        ClonerSetting.mainCloner = this ;
        ClonerSetting.Clone() ;
    }
    public static ClonerSetting mainCloner ;
    private bool isInvoke = false ;
    public static void Clone () {
        int r = Random.Range(0, mainCloner.canCloneObj.Length - 2 );
        mainCloner.NewFallObj(mainCloner.canCloneObj[r],mainCloner.transform.position);
    }
    public static void CloneNext (GameObject obj,Vector3 position)
    {
        mainCloner.NewFallingObj(obj,position) ;
    }
    public static void TimeOutSpown ()
    {
        if (mainCloner.isInvoke) return;
        mainCloner.isInvoke = true ;
        mainCloner.Invoke(nameof(CloneInv), 1.2f);
    }
    private void CloneInv () {
        mainCloner.isInvoke = false ;
        ClonerSetting.Clone();
    }

    // 1. 単純に複製する（位置や回転はオリジナルと同じ）
    private void NewFallObj (GameObject obj,Vector3 position){
        // Prefabをインスタンス化し、生成されたクローンを取得
        GameObject clone = Instantiate(obj, position, Quaternion.identity,CloneParent);
        M5Receiver.targetObj = clone ;
        
        // クローンに対して操作を行う
        clone.SetActive(true);
    }
    private void NewFallingObj (GameObject obj,Vector3 position){
        // Prefabをインスタンス化し、生成されたクローンを取得
        GameObject clone = Instantiate(obj, position, Quaternion.identity,CloneParent);
        
        // クローンに対して操作を行う
        clone.SetActive(true);

        Rigidbody rb = clone.GetComponent<Rigidbody>();

        rb.useGravity = true ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
