using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
        GameController.scoreBoad = this ;
    } 
    public static GameController scoreBoad ;
    public TextMeshProUGUI scoreText ;
    public int score = 0 ;
    public void ScoreAdd (int d)
    {
        if(d <= 0)
        {
            return ;
        }
        
        this.score += d ;
        Debug.Log(score) ;
        this.scoreText.text = "Score : " + this.score.ToString() ;
    }


}