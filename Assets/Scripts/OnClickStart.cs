using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnStartButtonClick()
    {
        Debug.Log("Start Button Clicked. Loading Scene: Play Screen");
        SceneManager.LoadScene("PlayScreen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
