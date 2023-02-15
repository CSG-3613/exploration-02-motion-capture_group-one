using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] GameObject PointsParent;
    [SerializeField] List<GameObject> CameraLocations;
    public float cameraMoveSpeed = 5f;
    public float moveCooldown = 2f;
    int index;
    float timer;
    Transform prevLocation;
    Transform targetLocation;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        prevLocation = CameraLocations[index].transform;
        targetLocation = CameraLocations[index].transform;
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
        prevLocation = _camera.transform;
        index = (index + 1) % CameraLocations.Count;
        timer = moveCooldown;
        targetLocation = CameraLocations[index].transform;
    }

    public void PrevSpot()
    {
        prevLocation = _camera.transform;
        index--;
        if (index == -1)
        {
            index = CameraLocations.Count - 1;
        }
        timer = moveCooldown;
        targetLocation = CameraLocations[index].transform;
    }

    bool isCameraMoving()
    {
        return false;
    }
}
