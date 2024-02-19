using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audio;
    AudioClip Music;

    string songName;

    bool played;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GManager.instance);
        GManager.instance.Start = false;
        songName = "Calamity Fortune";
        audio = GetComponent<AudioSource>();
        Music = (AudioClip)Resources.Load("Musics/" + songName);
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !played)
        {
            GManager.instance.Start = true;
            GManager.instance.StartTime = Time.time;
            played = true;
            audio.PlayOneShot(Music);
        }
        
    }
}
