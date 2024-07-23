using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRandomiser : MonoBehaviour
{

    public AudioClip[] sounds;
    private AudioSource source;
    public bool PlayOnAwake = false;
    [Range(0.1f,0.5f)]
    public float volumeChangeMultiplier = 0.2f;
    [Range(0.1f, 0.5f)]
    public float pitchChangeMultiplier = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        
        source = gameObject.AddComponent<AudioSource>();
        if (PlayOnAwake)
        {
            RandomiseSFX();
        }
    }

    public void RandomiseSFX()
    {
        source.clip = sounds[Random.Range(0, sounds.Length)];
        source.volume = Random.Range(0.4f , 0.5f);
        source.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        source.PlayOneShot(source.clip);
    }

    public void RandomiseHitSFX()
    {
        source.clip = sounds[Random.Range(0, 4)];
        source.volume = Random.Range(0.3f - volumeChangeMultiplier, 0.3f);
        source.pitch = 0.8f;
        source.PlayOneShot(source.clip);
        source.clip = sounds[Random.Range(8, 11)];
        source.volume = Random.Range(0.2f, 0.3f);
        source.pitch = 1;
        source.PlayOneShot(source.clip);


    }

    public void RandomiseClothesSFX()
    {
        source.clip = sounds[Random.Range(5, 7)];
        source.volume = Random.Range(0.2f , 0.3f);
        source.pitch = Random.Range(0.8f, 1);
        source.PlayOneShot(source.clip);
    }

}
