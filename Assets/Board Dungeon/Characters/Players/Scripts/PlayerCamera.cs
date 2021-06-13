using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform cameraHolder;
    [SerializeField] float camX = 3.68f;
    [SerializeField] float camY = 2.56f;
    [SerializeField] float camZ;
    [SerializeField] float camHeightLookPosition = 1.31f;
    Transform currentCamTarget;
    float cameraAngle;
    [SerializeField] float cameraAngleSpeed = 0.17f;
    [SerializeField] float camLookAtSpeed = 12f;
    [SerializeField] float camFollowSpeed = 5f;
    bool isCamLocked = false;
    Transform player;
    Camera MainCamera;
    ComboManager comboManager;
    
    PlayerUserControl playerUserControl;
    // Start is called before the first frame update
    void Awake()
    {
        playerUserControl = FindObjectOfType<PlayerUserControl>();
        player = playerUserControl.transform;
        comboManager = player.GetComponent<ComboManager>();
        MainCamera = Camera.main;
        

    }



    void FreeCamera()
    {
        
        if (comboManager.Attacking)
        {

            var playerAngle = Vector3.Angle(Vector3.forward, player.forward);
            Vector3 normal = Vector3.Cross(Vector3.forward, player.forward);// zwraca wektor miedzy dwoma wektorami
            playerAngle *= Mathf.Sign(Vector3.Dot(normal, Vector3.up));

            

            cameraAngle = 180f;
            cameraAngle += playerAngle;
        }
        else
        {
            cameraAngle += playerUserControl.TouchField.TouchDist.x * cameraAngleSpeed;
            
        }
            cameraHolder.position = player.position + Quaternion.AngleAxis(cameraAngle, Vector3.up) * new Vector3(0, camY, camX);
            Vector3 targetPoint = camZ * cameraHolder.right;
            cameraHolder.position += targetPoint;
            cameraHolder.LookAt(player.position + new Vector3(0, camHeightLookPosition, 0) + targetPoint);
        

        Vector3 smoothedPosition = Vector3.Slerp(MainCamera.transform.position, cameraHolder.position, Time.deltaTime * camFollowSpeed);
        MainCamera.transform.position = smoothedPosition;
        Vector3  camLookAtPoint = player.position + new Vector3(0, camHeightLookPosition, 0) + targetPoint;
        MainCamera.transform.rotation = Quaternion.Lerp(MainCamera.transform.rotation, Quaternion.LookRotation(camLookAtPoint - MainCamera.transform.position), camLookAtSpeed * Time.deltaTime);

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerUserControl.CameraForward = Vector3.Scale(cameraHolder.forward, new Vector3(1, 0, 1)).normalized;
        playerUserControl.CameraRight = cameraHolder.right;
        FreeCamera();   
    }
}
