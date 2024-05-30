using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    public Button RestartButton;



    private void Start()
    {
        RestartButton.onClick.AddListener(Restart); 
    }
    public void Restart()
    {
        Debug.Log("RESTART");
        SceneManager.LoadScene("Main");
    }    
}
