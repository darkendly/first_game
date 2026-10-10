using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Playercontroller player;
    private bool isGameOver = false;
    private bool isWin = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Win()
    {
        isWin = true;
    }
    void Start()
    {
        
        GameObject playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<Playercontroller>();
        



}

    // Update is called once per frame
    void Update()
    {
        if(player.IsDead)
        {

            isGameOver = true;
        }
        if (Keyboard.current.rKey.wasPressedThisFrame&&(isGameOver||isWin))
        {
            Debug.Log("④ R 收到了，开始重开");
            restart();
        }
    }
    
    
    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        return;
    }
    void OnGUI()
    {
        if (isGameOver)
        {
            GUI.Label(new Rect(10, 10, 400, 40), "Game Over!");
        }
    }
    
}
