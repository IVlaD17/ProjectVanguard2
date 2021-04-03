using UnityEngine;

using ProjectVanguard.Models.Systems;
using ProjectVanguard.Models.Entities;

namespace ProjectVanguard.Models.Components
{
    public class PlayerView
    {
        private float rotationX;

        private float sensitivityHor = 1.0f;
        private float sensitivityVert = 1.0f;

        private float minimumVert = -45.0f;
        private float maximumVert = 45.0f;

        public const float MapMaxX = 15f;
        public const float MapMaxY = 15f;
        public const float MapMinY = -15f;
        public const float MapMinX = -15f;

        public readonly Vector3 LocalCameraPosition = new Vector3(0, 0.5f, 0);
        public readonly Vector3 LocalCameraPositionRotationWhite = new Vector3(0, 0, 0);
        public readonly Vector3 LocalCameraPositionRotationBlack = new Vector3(0, -180, 0);

        public readonly Vector3 TopCameraPositionWhite = new Vector3(0, 3, 5.5f);
        public readonly Vector3 TopCameraRotationWhite = new Vector3(90, 0, 0);
        public readonly Vector3 TopCameraPositionBlack = new Vector3(0, 3, -5.5f);
        public readonly Vector3 TopCameraRotationBlack = new Vector3(90, -180, 0);

        public Camera Camera { get; private set; }
        public Entities.Player Player { get; private set; }
        public CameraControlAxes Axes { get; private set; }

        public PlayerView(Entities.Player player)
        {
            Player = player;
            Cursor.lockState = CursorLockMode.Locked;
            Axes = CameraControlAxes.VerticalAndHorizontal;
            Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            GameUpdater.AddUpdateCallback(Update);
        }

        // Update is called once per frame
        void Update()
        {
            if (Entities.Game.Instance.IsSessionPlaying())
            {
                if (Cursor.lockState != CursorLockMode.Locked)
                    Cursor.lockState = CursorLockMode.Locked;

                if (Player.IsActive)
                {
                    // Horizontal Movement
                    if (Axes == CameraControlAxes.Horizontal)
                    {
                        Camera.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
                    }
                    // Vertical Movement
                    else if (Axes == CameraControlAxes.Vertical)
                    {
                        rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                        rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);

                        float rotationY = Camera.transform.localEulerAngles.y;
                        Camera.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
                    }
                    else if (Axes == CameraControlAxes.VerticalAndHorizontal)
                    {
                        rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                        rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);

                        float delta = Input.GetAxis("Mouse X") * sensitivityHor;
                        float rotationY = Camera.transform.localEulerAngles.y + delta;

                        Camera.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
                    }
                    else if (Axes == CameraControlAxes.TopDown)
                    {
                        Camera.transform.Translate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

                        if (Camera.transform.localPosition.x < MapMinX)
                            Camera.transform.localPosition = new Vector3(MapMinX, Camera.transform.localPosition.y, Camera.transform.localPosition.z);

                        if (Camera.transform.localPosition.x > MapMaxX)
                            Camera.transform.localPosition = new Vector3(MapMaxX, Camera.transform.localPosition.y, Camera.transform.localPosition.z);

                        if (Camera.transform.localPosition.z < MapMinY)
                            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y, MapMinY);

                        if (Camera.transform.localPosition.z > MapMaxY)
                            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y, MapMaxY);
                    }
                }
            }
            if(Entities.Game.Instance.IsSessionPaused())
            {
                if (Cursor.lockState != CursorLockMode.Confined)
                    Cursor.lockState = CursorLockMode.Confined;
            }
        }

        public void ToggleViews()
        {
            if (Axes == CameraControlAxes.VerticalAndHorizontal)
                Axes = CameraControlAxes.TopDown;
            else if (Axes == CameraControlAxes.TopDown)
                Axes = CameraControlAxes.VerticalAndHorizontal;
        }
        public void ResetCamera()
        {
            if (Axes == CameraControlAxes.TopDown)
            {
                if (Player.ChessColor == ChessColor.White)
                {
                    Camera.transform.localPosition = TopCameraPositionWhite;
                    Camera.transform.localEulerAngles = TopCameraRotationWhite;
                }
                
                if(Player.ChessColor == ChessColor.Black)
                {
                    Camera.transform.localPosition = TopCameraPositionBlack;
                    Camera.transform.localEulerAngles = TopCameraRotationBlack;
                }
            }

            if(Axes == CameraControlAxes.VerticalAndHorizontal)
            {
                Camera.transform.localPosition = LocalCameraPosition;
                if (Player.ChessColor == ChessColor.White)
                    Camera.transform.localEulerAngles = LocalCameraPositionRotationWhite;

                if (Player.ChessColor == ChessColor.Black)
                    Camera.transform.localEulerAngles = LocalCameraPositionRotationBlack;
            }
        }
    }
}
