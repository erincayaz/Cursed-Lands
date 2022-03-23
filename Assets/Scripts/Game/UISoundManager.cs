using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] List<AudioClip> UISelectSounds = new List<AudioClip>();

    public void playRandomSelectSound()
    {
        int rand = Random.Range(0, UISelectSounds.Count);

        source.clip = UISelectSounds[rand];
        source.Play();
    }
}
