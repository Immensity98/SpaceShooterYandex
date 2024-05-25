using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInvisBullet : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
