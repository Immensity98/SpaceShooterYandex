using UnityEngine;
using UnityEngine.SceneManagement;
using KulibinSpace.MessageBus;
using UnityEngine.Events;

public class GameEventGenerator : MonoBehaviour {

    public GameMessage startGame;
    public UnityEvent sceneLoadedEvent;

    public void StartGameEvent () {
        startGame?.Invoke();
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    void SceneLoaded (Scene scene, LoadSceneMode mode) {
        sceneLoadedEvent?.Invoke();
    }

}
