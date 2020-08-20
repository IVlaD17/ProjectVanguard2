﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public Session MySession { get; private set; }

    public RotationAxes Axes { get; private set; } = RotationAxes.MOUSE_X_AND_Y;

    float sensitivityHor = 1.0f;
    float sensitivityVert = 1.0f;

    float minimumVert = -45.0f;
    float maximumVert = 45.0f;

    float rotationX;

    // Start is called before the first frame update
    void Start()
    {
        GameObject sessionObject = GameObject.Find(Constants.SESS_MANAGER);
        if (sessionObject != null)
            MySession = sessionObject.GetComponent<Session>();
        else
            Debug.Log(Constants.NO_SESS_MANAGER_FOUND);
    }

    // Update is called once per frame
    void Update()
    {
        if (MySession.MySessionState == SessionState.PLAYING)
        {
            // Horizontal Movement
            if (Axes == RotationAxes.MOUSE_X)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            }
            // Vertical Movement
            else if (Axes == RotationAxes.MOUSE_Y)
            {
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);

                float rotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            }
            else if(Axes == RotationAxes.MOUSE_X_AND_Y)
            {
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);

                float delta = Input.GetAxis("Mouse X") * sensitivityHor;
                float rotationY = transform.localEulerAngles.y + delta;

                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            }
            else if(Axes == RotationAxes.TOP_DOWN)
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
        if (Axes == RotationAxes.MOUSE_X_AND_Y)
            Axes = RotationAxes.TOP_DOWN;
        else if (Axes == RotationAxes.TOP_DOWN)
            Axes = RotationAxes.MOUSE_X_AND_Y;
    }
}
