using Unity.VisualScripting;
using UnityEngine;

public class ObjectCloner : MonoBehaviour
{
    [SerializeField] private GameObject NextPrefab ;
    public float Sensibility = 0.005f;
    // public GameObject box ;
    private bool isProcessed = false;
    private Rigidbody rb ;
    private bool isFalling = false ;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.rb.useGravity = false ;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFalling){
            KeyEvents();
        }
    }
    private void KeyEvents () {
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.position += new Vector3(0f,0f,Sensibility);
        }
        if(Input.GetKey(KeyCode.S))
        {
            this.transform.position -= new Vector3(0f,0f,Sensibility);
        }
        if(Input.GetKey(KeyCode.A))
        {
            this.transform.position += new Vector3(Sensibility,0f,0f);
        }
        if(Input.GetKey(KeyCode.D))
        {
            this.transform.position -= new Vector3(Sensibility,0f,0f);
        }
        if(Input.GetKey(KeyCode.C)){
            this.isFalling = true ;
            this.rb.useGravity = true ;
            ClonerSetting.TimeOutSpown() ;
        }
    }
    void OnCollisionEnter(Collision collision){
        if (isProcessed) return;
        isProcessed = true ;
        
        if(collision.gameObject.name == this.gameObject.name){
            Destroy(this.gameObject) ;
            Destroy(collision.gameObject) ;
            Debug.Log("collision");
            if(NextPrefab == null)
            {
                return ;
            }
            ClonerSetting.CloneNext(NextPrefab,this.transform.position) ;
        }
    }
    // private bool IsInArea_X ( Vector3 pos)
    // {
    //     float posx = pos.x - box.transform.position.x ;
    //     return (
    //         posx < 3.0f &&
    //         posx > 0.0f
    //     ) ;
    // }

    // private bool IsInArea_Y(Vector3 pos)
    // {
    //     float posy = pos.y - box.transform.position.y;
    //     return (
    //         posy > 0.0f && 
    //         posy < 3.0f
    //     );
    // }
}
