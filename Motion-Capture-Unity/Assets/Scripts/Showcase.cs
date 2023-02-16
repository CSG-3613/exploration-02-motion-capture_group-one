using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showcase : MonoBehaviour
{
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
    }

    public void StopShowcase()
    {
        if (!isPlaying) return;
        isPlaying = false;
    }

}
