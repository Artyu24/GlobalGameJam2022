using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    private static OptionManager _instance = null;
    public static OptionManager instance { get => _instance; }

    [Header("Component")] 
    [SerializeField] private Button musiqueButton;
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button vibrationButton;
    [SerializeField] private Sprite musiqueOFFSprite;
    [SerializeField] private Sprite musiqueOnSprite;
    [SerializeField] private Sprite seOnSprite;
    [SerializeField] private Sprite seOffSprite;
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

    [SerializeField] private bool _isSoundEffectEnabled = true;

    public bool isSoundEffectEnabled
    {
        get { return _isSoundEffectEnabled; }
        set { _isSoundEffectEnabled = value; }
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

    private void Start()
    {
        isVibrationEnabled = PlayerPrefs.GetInt("isVibrationEnabled") == 1;
        SwitchVibration();
        isSoundEnable = PlayerPrefs.GetInt("isSoundEnable") == 1;
        SwitchSound();
        isSoundEffectEnabled = PlayerPrefs.GetInt("isSoundEffectEnabled") == 1;
        SwitchSoundEffect();
    }

    public void SwitchSoundEffect()
    {
        if (isSoundEffectEnabled)
        {
            //enable sound
            isSoundEffectEnabled = false;
            PlayerPrefs.SetInt("isSoundEffectEnabled", isSoundEffectEnabled ? 0 : 1);
            soundEffectButton.image.sprite = seOffSprite;
        }
        else
        {
            //disable sound
            isSoundEffectEnabled = true; 
            PlayerPrefs.SetInt("isSoundEffectEnabled", isSoundEffectEnabled ? 0 : 1);
            soundEffectButton.image.sprite = seOnSprite;
        }
    }
    public void SwitchSound()
    {
        if (isSoundEnable)
        {
            //disable sound
            cam.GetComponent<AudioSource>().Pause();
            isSoundEnable = false;
            PlayerPrefs.SetInt("isSoundEnable", isSoundEnable ? 0 : 1);
            musiqueButton.image.sprite = musiqueOFFSprite;
        }
        else
        {
            //enable sound
            cam.GetComponent<AudioSource>().Play();
            isSoundEnable = true;
            PlayerPrefs.SetInt("isSoundEnable", isSoundEnable ? 0 : 1);
            musiqueButton.image.sprite = musiqueOnSprite;
        }
    }

    public void SwitchVibration()
    {
        if (isVibrationEnabled)
        {
            //enable Vibration
            isVibrationEnabled = false;
            PlayerPrefs.SetInt("isVibrationEnabled", isVibrationEnabled ? 0 : 1);
            vibrationButton.image.sprite = vibratioffSprite;
        }
        else
        {
            //disable Vibration
            isVibrationEnabled = true;
            PlayerPrefs.SetInt("isVibrationEnabled", isVibrationEnabled?0:1);
            vibrationButton.image.sprite = vibrationSprite;
            /*Handheld.Vibrate();*/
        }
    }


}
