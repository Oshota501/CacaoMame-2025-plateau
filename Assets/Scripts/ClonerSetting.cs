using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonerSetting : MonoBehaviour
{
    
    public GameObject[] canCloneObj ;
    // Start is called before the first frame update
    void Start()
    {
        ClonerSetting.mainCloner = this ;
        ClonerSetting.Clone() ;
    }
    public static ClonerSetting mainCloner ;
    public static void Clone () {
        int r = Random.Range(0, mainCloner.canCloneObj.Length);
        mainCloner.NewFallObj(mainCloner.canCloneObj[r],mainCloner.transform.position);
    }

    // 1. 単純に複製する（位置や回転はオリジナルと同じ）
    private void NewFallObj (GameObject obj,Vector3 position){
        // Prefabをインスタンス化し、生成されたクローンを取得
        GameObject clone = Instantiate(obj, position, Quaternion.identity);
        
        // クローンに対して操作を行う
        clone.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
