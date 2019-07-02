using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [Header("Sets")]
    [SerializeField]
    private RuntimeUnitSet allySet;
    [SerializeField]
    private RuntimeUnitSet enemySet;
    [SerializeField]
    private IsRoman isRoman;
    [Header("UI")]
    [SerializeField]
    private Text endText;
    [SerializeField]
    private Button endButton;
    [Header("BGM")]
    [SerializeField]
    private AudioClip victoryJingle;
    [SerializeField]
    private AudioClip defeatJingle;
    [SerializeField]
    private AudioClip romanBgm;
    [SerializeField]
    private AudioClip cartagoBgm;
    

    private bool initialized = false;


    private AudioSource cameraAudio;

    private bool finished = false;

    private void Awake()
    {
        Camera c = Camera.main;
        if (c != null)
        {
            cameraAudio = c.GetComponent<AudioSource>();
        }
    }

    public void Initialize()
    {
        initialized = true;
        if (isRoman.isRoman == true)
        {
            PlayBGM(romanBgm);
        }
        else
        {
            PlayBGM(cartagoBgm);
        }
    }

    void Update () {
        if (initialized == true)
        {
            if (allySet.Items.Count == 0 && !finished)
            {
                endText.text = "Has perdido la batalla, ¡pero no la guerra!";
                PlayBGM(defeatJingle);
                Finish();
            }
            else if (enemySet.Items.Count == 0 && !finished)
            {
                endText.text = "¡La victoria es tuya!";
                PlayBGM(victoryJingle);
                Finish();
            }
        }
	}

    private void PlayBGM(AudioClip clip)
    {
        if (cameraAudio != null)
        {
            cameraAudio.Stop();
            cameraAudio.clip = clip;
            cameraAudio.loop = true;
            cameraAudio.Play();
        }
    }

    private void Finish()
    {
        finished = true;
        endButton.gameObject.SetActive(true);
        endText.gameObject.SetActive(true);
    }
}
