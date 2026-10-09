using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject WinText;

    void Start()
    {
        WinText.SetActive(false);
    }
   

     void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Goal 被触发，碰到的是：" + other.name);
        if (other.CompareTag("Player") )
        {
            WinText.SetActive(true);
        }
    }
}
