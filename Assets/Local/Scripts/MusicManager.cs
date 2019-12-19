using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> musicTracks; // drag and drop audio clips in Inspector * MINIMUM 2 clips *


    public float playTimeMin = 60; // time in seconds, minimum time before changing tracks
    public float playTimeMax = 180; // time in seconds, maximum time before changing tracks
 
    public float transitionFadeTime = 3; // time in seconds, time it takes for fade to complete
    public float transitionFadeVolume = 0.25f; // volume between 0 and 1, volume the current track gets to before blending in the new track
 
 
    private AudioSource[] audioSources;
    private int currentSource = 0;
 
    private List<int> musicTracksAvailable;
    private int currentTrack = 0;
 
    private bool isFading = false;
 
    private float timer = 0;
    private float timerMax = 60;
 
 
 //  Persistant Functions
 //    ----------------------------------------------------------------------------
 
 
    void Awake()
    {
        // NOTE : this object must have 2 child objects,
        // each with an AudioSource component to work
        audioSources =  GetComponentsInChildren<AudioSource>();
    }


    void Start()
    {
        // dummy select current track playing 
        // (so its possible that musicTracks[0] could be selected as first track to play)
        currentTrack = Random.Range(0, musicTracks.Count);

        // set timer over max so first update is set to fading
        // after resetting values in CheckTrackTimer()
        timer = timerMax + 1;
    }


    void Update()
    {
        timer += Time.deltaTime;

        switch (isFading)
        {
            case false:
                CheckTrackTimer();
                break;

            case true:
                FadeTransition();
                break;
        }
    }


    //  Other Functions
    //    ----------------------------------------------------------------------------


    void SelectNextTrack()
    {
        // create a list of available tracks to choose from
        // (so the current track is not chosen and repeated)
        musicTracksAvailable = new List<int>();

        for (int t = 0; t < musicTracks.Count; t++ )
     {
            musicTracksAvailable.Add(t);
        }

        //remove current track from the list
        musicTracksAvailable.RemoveAt(currentTrack);

        // pick a new random track
        currentTrack = musicTracksAvailable[Random.Range(0, musicTracksAvailable.Count)];

        // assign track to AudioSource that is NOT currently playing
        audioSources[(currentSource + 1) % 2].Stop();
        audioSources[(currentSource + 1) % 2].clip = musicTracks[currentTrack];
        audioSources[(currentSource + 1) % 2].volume = 0;
    }


    void CheckTrackTimer()
    {
        if (timer > timerMax)
        {
            // reset timer, set new max time to next transition
            timer = 0;
            timerMax = Random.Range(playTimeMin, playTimeMax);

            SelectNextTrack();

            isFading = true;
        }
    }


    void FadeTransition()
    {
        float fadeIn = 0;

        // calculate the fade Out volume
        float fadeOut = 1.0f - (timer / transitionFadeTime);
        fadeOut = Mathf.Clamp01(fadeOut);

        audioSources[currentSource].volume = fadeOut;


        // if fadeOut is low enough, start playing the new track and fade it in

        if (fadeOut < (transitionFadeVolume * 2.0))
        {
            // is the next track playing yet?
            if (!audioSources[(currentSource + 1) % 2].isPlaying)
            {
                audioSources[(currentSource + 1) % 2].Play();
            }

            // calculate the fade In volume
            fadeIn = timer;
            // minus how long for the other source to reach transitionFadeVolume
            fadeIn -= (1.0f - transitionFadeVolume) * transitionFadeTime;
            // add how long for this source to reach transitionFadeVolume
            fadeIn += transitionFadeVolume * transitionFadeTime;
            // normalize by dividing by fade time
            fadeIn /= transitionFadeTime;

            fadeIn = Mathf.Clamp01(fadeIn);

            audioSources[(currentSource + 1) % 2].volume = fadeIn;
        }


        // check if next track has fully faded in
        if (fadeIn == 1)
        {
            // stop the old audio from playing
            audioSources[currentSource].Stop();

            // set new current AudioSource index
            currentSource = (currentSource + 1) % 2;

            // reset timer
            timer = 0;

            isFading = false;
        }

        //Debug.Log( "fadeOut = " + fadeOut + " : fadeIn = " + fadeIn );
    }
}
 
