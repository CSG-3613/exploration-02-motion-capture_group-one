using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public GameObject PlayButton;

    public void Awake()
    {
        SceneManager.Instance.NewStateEvent.AddListener(SetPlayButton);
    }

    public void SetPlayButton(bool val)
    {
        if (PlayButton.activeSelf == !val) return;
        PlayButton.SetActive(!val);
    }

    public void OnLeftArrowButton()
    {
        SceneManager.Instance.MoveLeft();
    }

    public void OnRightArrowButton()
    {
        SceneManager.Instance.MoveRight();
    }

    public void OnPlayShowcaseButton()
    {
        SceneManager.Instance.PlayShowcase();
    }
}
