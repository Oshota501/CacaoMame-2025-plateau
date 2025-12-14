using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectCloner : MonoBehaviour
{
    [SerializeField] private GameObject NextPrefab ;
    public float Sensibility = 0.005f;
    
    // public GameObject box ;
    private bool isProcessed = false;
    private Rigidbody rb ;
    public bool isFalling = false ;
    private bool TimeCount = false ;
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
        this.IsInRange();
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
            M5Receiver.targetObj = null ;
            this.isFalling = true ;
            this.rb.useGravity = true ;
            this.Invoke(nameof(SetTimeCount),2f);
            ClonerSetting.TimeOutSpown() ;
        }
    }
    public void SetTimeCount()
    {
        TimeCount = true;
    }
    public void MoveToFin()
    {
        Debug.Log("Move To Fin.Game End.");
        SceneManager.LoadScene("Fin");
    }
    public void IsInRange()
    {   
        if (TimeCount && 
            ( this.transform.position.y >= 83f ||
            this.transform.position.y <= 65f )
        ){
            MoveToFin();
        }
    }
 
    void OnCollisionStay(Collision collision){
        if (isProcessed) return;
        if(collision.gameObject.name == this.gameObject.name){
            isProcessed = true ;
            ObjectCloner oc = collision.gameObject.GetComponent<ObjectCloner> () ;
            oc.isProcessed = true ;
            Destroy(this.gameObject) ;
            Destroy(collision.gameObject) ;
            Debug.Log("collision");
            GameController.scoreBoad.ScoreAdd(100);

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
