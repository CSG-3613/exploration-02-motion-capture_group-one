using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Camera _camera;
    public GameObject PointsParent;
    [SerializeField] List<GameObject> Showcases;
    public float cameraMoveSpeed = 5f;
    public float moveCooldown = 2f;
    int index;
    float timer;
    Transform prevLocation;
    Transform targetLocation;
    [HideInInspector] public UnityEvent StopAllEvent;

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
    }


    void Start()
    {
        index = 0;
        prevLocation = Showcases[index].transform;
        targetLocation = Showcases[index].transform;
        timer = 0;
    }

    private void Update()
    {
        _camera.transform.position = Vector3.Lerp(prevLocation.position, targetLocation.position, cameraMoveSpeed * Time.deltaTime);
        _camera.transform.LookAt(2 * _camera.transform.position - PointsParent.transform.position);
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PrevSpot();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                NextSpot();
            }
        }
    }

    public void NextSpot()
    {
        StopAllEvent.Invoke();
        prevLocation = _camera.transform;
        index = (index + 1) % Showcases.Count;
        timer = moveCooldown;
        targetLocation = Showcases[index].transform;
    }

    public void PrevSpot()
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
    }

    bool isCameraMoving()
    {
        return false;
    }
}
