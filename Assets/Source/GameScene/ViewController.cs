using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProjectVanguard.Models;

public class ViewController : MonoBehaviour
{
    string gameManagerObjectName = "GameManager";

    public Session MySession { get; private set; }

    public CameraControlAxes Axes { get; private set; } = CameraControlAxes.VerticalAndHorizontal;

    float sensitivityHor = 1.0f;
    float sensitivityVert = 1.0f;

    float minimumVert = -45.0f;
    float maximumVert = 45.0f;

    float rotationX;

    // Start is called before the first frame update
    void Start()
    {
        GameObject sessionObject = GameObject.Find(gameManagerObjectName);
        if (sessionObject != null)
            MySession = sessionObject.GetComponent<Session>();
        else
            Debug.LogErrorFormat("Failed to find the object named {0}.", gameManagerObjectName);
    }

    // Update is called once per frame
    void Update()
    {
        if (MySession.MySessionState == SessionState.Playing)
        {
            // Horizontal Movement
            if (Axes == CameraControlAxes.Horizontal)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            }
            // Vertical Movement
            else if (Axes == CameraControlAxes.Vertical)
            {
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);

                float rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            }
            else if(Axes == CameraControlAxes.VerticalAndHorizontal)
            {
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);

                float delta = Input.GetAxis("Mouse X") * sensitivityHor;
                float rotationY = transform.localEulerAngles.y + delta;

                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            }
            else if(Axes == CameraControlAxes.TopDown)
            {              
                transform.Translate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

                if (transform.localPosition.x < Constants.MAP_MIN_X)
                    transform.localPosition = new Vector3(Constants.MAP_MIN_X, transform.localPosition.y, transform.localPosition.z);

                if (transform.localPosition.x > Constants.MAP_MAX_X)
                    transform.localPosition = new Vector3(Constants.MAP_MAX_X, transform.localPosition.y, transform.localPosition.z);

                if (transform.localPosition.z < Constants.MAP_MIN_Y)
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Constants.MAP_MIN_Y);

                if (transform.localPosition.z > Constants.MAP_MAX_Y)
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Constants.MAP_MAX_Y);
            }
        }
    }

    public void ToggleViews()
    {
        if (Axes == CameraControlAxes.VerticalAndHorizontal)
            Axes = CameraControlAxes.TopDown;
        else if (Axes == CameraControlAxes.TopDown)
            Axes = CameraControlAxes.VerticalAndHorizontal;
    }
}
