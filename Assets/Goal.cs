using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject WinText;
    public GameOver gameover;

    void Start()
    {
        WinText.SetActive(false);
        gameover = GetComponent<GameOver>();
        
    }
   

     void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Goal 被触发，碰到的是：" + other.name);
        if (other.CompareTag("Player") )
        {
            WinText.SetActive(true);
            if (gameover != null) gameover.Win();
        }
    }
    
}
