using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayer : MonoBehaviour
{
    public GameObject videoPlayer;
    public float timeToStop = 3f;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.SetActive(false );
        
    }
    public void StartVideo()
    {
        videoPlayer.SetActive(true);
        Destroy(videoPlayer, timeToStop);
    }

}
