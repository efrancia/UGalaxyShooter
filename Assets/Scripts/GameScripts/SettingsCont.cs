using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsCont : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource Music;
    [SerializeField] AudioSource[] Effects;
    [SerializeField] GameObject[] Buttons;
    [SerializeField] GameObject ControlImage;
    [SerializeField] GameObject l2settings;

    [SerializeField] GameObject l1settings;
    [SerializeField] Slider music;

    [SerializeField] Slider effects;
    [SerializeField] GameObject backtopause;
    [SerializeField] GameObject Pause;
    
    void Start()
    {

        CreateDefaultVolume();
        l1settings.SetActive(true);
        foreach (Transform child in l2settings.transform)
        {
            child.gameObject.SetActive(true);
        }
        l2settings.SetActive(false);
        
    }
    public void CreateDefaultVolume() {
        if (PlayerPrefs.HasKey("BackgroundVolume") && PlayerPrefs.HasKey("EffectsVolume"))
        {
            music.value = PlayerPrefs.GetFloat("BackgroundVolume");
            Music.volume = PlayerPrefs.GetFloat("BackgroundVolume");
            effects.value = PlayerPrefs.GetFloat("EffectsVolume");
            foreach (AudioSource x in Effects)
            {
                x.volume = PlayerPrefs.GetFloat("EffectsVolume");
            }
        }
        else
        {
            PlayerPrefs.SetFloat("BackgroundVolume", .5f);
            PlayerPrefs.SetFloat("EffectsVolume", .5f);
            Music.volume = PlayerPrefs.GetFloat("BackgroundVolume");
            foreach (AudioSource x in Effects)
            {
                x.volume = PlayerPrefs.GetFloat("EffectsVolume");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void MusicVol() {
        Music.volume = music.value;
        PlayerPrefs.SetFloat("BackgroundVolume", Music.volume);
    }
    public void EffectsVol() {

       foreach (AudioSource x in Effects)
        {
            x.volume = effects.value;
        }
        PlayerPrefs.SetFloat("EffectsVolume", effects.value);
    }
    public void Volume() {
        l2settings.SetActive(true);
        l1settings.SetActive(false);
        music.gameObject.SetActive(true);
        effects.gameObject.SetActive(true);
        ControlImage.SetActive(false);
        backtopause.SetActive(true);
    }
    public void Controls()
    {
        l2settings.SetActive(true);
        l1settings.SetActive(false);
        music.gameObject.SetActive(false);
        effects.gameObject.SetActive(false);
        ControlImage.SetActive(true);
        backtopause.SetActive(true);
    }
    public void BackToPause()
    {
        foreach (Transform child in l2settings.transform)
        {
            child.gameObject.SetActive(true);
        }

        l2settings.SetActive(false);
        foreach (Transform child in l2settings.transform)
        {
            child.gameObject.SetActive(true);
        }
        l1settings.SetActive(true);
    }
    public void BackToGame() {

        l1settings.transform.parent.gameObject.SetActive(false);
    }
}
