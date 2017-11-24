﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    public AudioClip[] musicClips;
    AudioSource source;
    AudioLowPassFilter lowPassFilter;
    Player playerObject;

    public float maximumCutOffValue = 20000.0f;

    bool isTakingPlayerHealth = false;

	// Use this for initialization
	void Start () {
        StartInitials();

        EventManager.instance.OnStartRound.AddListener(()=> {
            StartCoroutine("Fade", true);
        });
        EventManager.instance.OnEndRound.AddListener(()=> {
            isTakingPlayerHealth = false;
            StartCoroutine("Fade", false);
        });

        StartCoroutine("Fade", true);
    }
	
	// Update is called once per frame
	void Update () {
        if (!source.isPlaying)
            PlayNewAudio();

        if (isTakingPlayerHealth)
            lowPassFilter.cutoffFrequency = DynamicPass();
	}

    //Plays a new audio clip continuously
    void PlayNewAudio()
    {
        source.PlayOneShot(musicClips[(int)Random.Range(0, musicClips.Length)]);
    }

    //Dynamically changes low pass filter
    float DynamicPass()
    {
        float passVal = playerObject.health / playerObject.maxHealth;

        passVal *= maximumCutOffValue;

        return Mathf.Clamp(passVal, 250, maximumCutOffValue);
    }

    // Initialisations made when starting
    void StartInitials()
    {
        if (!GetComponent<AudioSource>())
            Debug.Log(this.name + "does not contain an Audio Source!");
        else
            source = GetComponent<AudioSource>();

        if (!GetComponent<AudioLowPassFilter>())
            Debug.Log(this.name + "does not contain an Audio Low Pass Filter!");
        else
            lowPassFilter = GetComponent<AudioLowPassFilter>();

        lowPassFilter.cutoffFrequency = 250;

        if (!FindObjectOfType<Player>())
            Debug.Log("There is no Player object on the scene!");
        else
            playerObject = FindObjectOfType<Player>();
    }

    IEnumerator Fade(bool isFadingIn)
    {
        float fadeValue = 0.0f;
        
        while(fadeValue < 1)
        {
            yield return new WaitForSeconds(0.05f);
            fadeValue += Time.deltaTime;
            if(isFadingIn)
                lowPassFilter.cutoffFrequency = Mathf.Clamp(fadeValue * maximumCutOffValue, 250, maximumCutOffValue);
            else if(!isFadingIn)
                lowPassFilter.cutoffFrequency = Mathf.Clamp(maximumCutOffValue - (fadeValue * maximumCutOffValue), 250, maximumCutOffValue);
        }

        isTakingPlayerHealth = isFadingIn;
    }
}
