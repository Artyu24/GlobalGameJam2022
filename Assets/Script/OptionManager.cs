using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    private static OptionManager _instance = null;
    public static OptionManager instance { get => _instance; }

    [Header("Component")] 
    [SerializeField] private Button soundButton;
    [SerializeField] private Button vibrationButton;
    [SerializeField] private Sprite muteSprite;
    [SerializeField] private Sprite unmuteSprite;
    [SerializeField] private Sprite vibrationSprite;
    [SerializeField] private Sprite vibratioffSprite;
    [SerializeField] private GameObject cam;

    [Header("Option")]
    [SerializeField] private bool _isSoundEnabled = true;
    public bool isSoundEnable
    {
        get { return _isSoundEnabled; }
        set { _isSoundEnabled = value; }
    }
    [SerializeField] private bool _isVibrationEnabled = true;
    public bool isVibrationEnabled
    {
        get { return _isVibrationEnabled; }
        set { _isVibrationEnabled = value; }
    }

    private bool _isInMainMenu;
    public bool isInMainMenu
    {
        get { return _isInMainMenu; }
        set { _isInMainMenu = value; }
    }


    private void Awake()
    {
        _instance = this;
    }

    public void SwitchSound()
    {
        if (isSoundEnable)
        {
            //enable sound
            cam.GetComponent<AudioListener>().enabled = false;
            isSoundEnable = false;
            soundButton.image.sprite = muteSprite;
        }
        else
        {
            //disable sound
            cam.GetComponent<AudioListener>().enabled = true;
            isSoundEnable = true;
            soundButton.image.sprite = unmuteSprite;
        }
    }

    public void SwitchVibration()
    {
        if (isVibrationEnabled)
        {
            //enable sound
            isVibrationEnabled = false;
            vibrationButton.image.sprite = vibratioffSprite;
        }
        else
        {
            //disable sound
            isVibrationEnabled = true;
            vibrationButton.image.sprite = vibrationSprite;
        }
    }
}
