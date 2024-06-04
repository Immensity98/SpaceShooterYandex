using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

    // сигнал от слушателя кнопки РЕСТАРТ
    public void RestartGameAction () {
        SceneManager.LoadScene("Main");
    }


}
