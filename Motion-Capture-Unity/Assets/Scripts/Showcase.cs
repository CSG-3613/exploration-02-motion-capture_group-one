using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Playables;

public class Showcase : MonoBehaviour
{
    [SerializeField] VideoPlayer[] vp;
    [SerializeField] PlayableDirector[] pd;
    bool isPlaying = false;
    public void Awake()
    {
        SceneManager.Instance.StopAllEvent.AddListener(StopShowcase);
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void Start()
    {
        transform.LookAt(2 * transform.position - SceneManager.Instance.PointsParent.transform.position);
    }

    public void StartShowcase()
    {
        isPlaying = true;
        foreach (VideoPlayer v in vp)
        {
            v.Play();
        }
        foreach (PlayableDirector p in pd)
        {
            p.Play();
        }
    }

    public void StopShowcase()
    {
        if (!isPlaying) return;
        isPlaying = false;
        foreach (VideoPlayer v in vp)
        {
            v.Stop();
        }
        foreach (PlayableDirector p in pd)
        {
            p.Stop();
        }
    }

}
