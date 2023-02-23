using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Camera _camera;
    public GameObject PointsParent;
    [SerializeField] List<GameObject> Showcases;
    public float cameraMoveSpeed = 5f;
    public float moveCooldown = 2f;
    [SerializeField] TMP_Text label;
    int index;
    float timer;
    Transform prevLocation;
    Transform targetLocation;
    bool isMoving = false;
    [HideInInspector] public UnityEvent StopAllEvent;
    [HideInInspector] public UnityEvent<bool> NewStateEvent;

    #region SceneManager Singleton
    static private SceneManager instance;
    static public SceneManager Instance { get { return instance; } }

    void CheckManagerInScene()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private void Awake()
    {
        CheckManagerInScene();
        StopAllEvent = new UnityEvent();
        NewStateEvent = new UnityEvent<bool>();
    }


    void Start()
    {
        index = 0;
        prevLocation = Showcases[index].transform;
        targetLocation = Showcases[index].transform;
        label.text = Showcases[index].GetComponent<Showcase>().Label;
        timer = 0;
    }

    private void Update()
    {
        _camera.transform.position = Vector3.Lerp(prevLocation.position, targetLocation.position, cameraMoveSpeed * Time.deltaTime);
        _camera.transform.LookAt(2 * _camera.transform.position - PointsParent.transform.position);

        if (isMoving != isCameraMoving())
        {
            isMoving = isCameraMoving();
            NewStateEvent.Invoke(isMoving);
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayShowcase();
        }
    }

    public void MoveRight()
    {
        StopAllEvent.Invoke();
        prevLocation = _camera.transform;
        index = (index + 1) % Showcases.Count;
        timer = moveCooldown;
        targetLocation = Showcases[index].transform;
        label.text = Showcases[index].GetComponent<Showcase>().Label;
    }

    public void MoveLeft()
    {
        StopAllEvent.Invoke();
        prevLocation = _camera.transform;
        index--;
        if (index == -1)
        {
            index = Showcases.Count - 1;
        }
        timer = moveCooldown;
        targetLocation = Showcases[index].transform;
        label.text = Showcases[index].GetComponent<Showcase>().Label;
    }

    public void PlayShowcase()
    {
        if (isCameraMoving()) return;

        Showcases[index].GetComponent<Showcase>().StartShowcase();

    }

    bool isCameraMoving()
    {
        return Mathf.Abs((_camera.transform.position - targetLocation.position).magnitude) > 0.05f;
    }
}
