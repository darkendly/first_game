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
        if(other.CompareTag("Player") )
        {
            WinText.SetActive(true);
        }
    }
}
