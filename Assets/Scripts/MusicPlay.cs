using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    public AudioSource AudioSource;
    private void Start()
    {
        AudioSource.Play();
    }
}
