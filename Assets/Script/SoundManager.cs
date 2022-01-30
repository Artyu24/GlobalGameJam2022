using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("Sound")] 
    [SerializeField] private AudioClip musicIG;
    [SerializeField] private AudioClip musicMenu;
    [SerializeField] private GameObject cam;

    // Update is called once per frame
    void Update()
    {
        if (OptionManager.instance.isSoundEnable)
        {
            if (OptionManager.instance.isInMainMenu)
            {
                cam.GetComponent<AudioSource>().clip = musicIG;
            }
            else
            {
                cam.GetComponent<AudioSource>().clip = musicMenu;
            }
        }
        else
        {
            cam.GetComponent<AudioSource>().clip = null;
        }
    }
}
