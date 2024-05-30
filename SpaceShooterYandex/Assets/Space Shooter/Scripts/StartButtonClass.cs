using UnityEngine;

public class StartButtonClass : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (GameManager.StartIsPressed)
        {
            gameObject.SetActive(false);
        }
    }
    public GameManager GameManager;

    public void StartGame()
    {
        if (!GameManager.StartIsPressed)
        {
            GameManager.StartGame();
            gameObject.SetActive(false);
        }
    }
}
