using UnityEngine;

public class PlatformCheck : MonoBehaviour
{
    public GameObject ButtonsAndroid;

    void Start()
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            ButtonsAndroid.SetActive(false);
        }
    }
}
