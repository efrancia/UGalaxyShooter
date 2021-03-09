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
    [SerializeField]    Slider music;

    [SerializeField]    Slider effects;
    [SerializeField] GameObject backtopause;
    [SerializeField] GameObject Pause;
    
    void Start()
    {
        
        music.value = PlayerPrefs.GetFloat("BackgroundVolume");
        effects.value = PlayerPrefs.GetFloat("DeathVolume");

        l1settings.SetActive(true);
        foreach (Transform child in l2settings.transform)
        {
            child.gameObject.SetActive(true);
        }
        l2settings.SetActive(false);
        
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
            PlayerPrefs.SetFloat(x.gameObject.name + "Volume", effects.value);
        }
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
