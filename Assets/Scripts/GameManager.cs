using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button StartButton;
    public bool StartIsPressed;
    public GameObject Game;

    private void Awake()
    {
        StartButton = FindObjectOfType<StartButtonClass>().GetComponent<Button>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

        if(StartIsPressed)
        {
            Game.SetActive(true);
            //StartButton.enabled = false;
        }

    }

    public void StartGame()
    {
        if (!StartIsPressed)
        {
            Game.SetActive(true);
            StartIsPressed = true;
        }
    }


}
