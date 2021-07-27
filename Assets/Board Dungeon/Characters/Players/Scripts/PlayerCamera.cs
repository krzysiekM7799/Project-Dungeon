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
    [SerializeField] private float camHeightLookPositionOnEnemy = 2;
    [SerializeField] float cameraAngleSpeed = 0.17f;
    [SerializeField] float camLookAtSpeed = 12f;
    [SerializeField] float camFollowSpeed = 5f;
    private float cameraAngle;
    private Transform playerTransform;
    private Camera mainCamera;
    private ComboManager comboManager;
    PlayerUserControl playerUserControl;
    
    void Awake()
    {
        playerUserControl = FindObjectOfType<PlayerUserControl>();
        playerTransform = playerUserControl.transform;
        comboManager = playerTransform.GetComponent<ComboManager>();
        mainCamera = Camera.main;
    }

    void FreeCamera()
    {
      
        if (comboManager.Attacking)
        {
            var playerAngle = Vector3.Angle(Vector3.forward, playerTransform.forward);
            Vector3 normal = Vector3.Cross(Vector3.forward, playerTransform.forward);
            playerAngle *= Mathf.Sign(Vector3.Dot(normal, Vector3.up));
    
            cameraAngle = 180f;
            cameraAngle += playerAngle;
            //added angle to view combo
            cameraAngle += 50f;
        }
        else
        {
            cameraAngle += playerUserControl.TouchField.TouchDist.x * cameraAngleSpeed;
            
        }
            cameraHolder.position = playerTransform.position + Quaternion.AngleAxis(cameraAngle, Vector3.up) * new Vector3(0, camY, camX);
            Vector3 targetPoint = camZ * cameraHolder.right;
            cameraHolder.position += targetPoint;
            cameraHolder.LookAt(playerTransform.position + new Vector3(0, camHeightLookPosition, 0) + targetPoint);
        

        Vector3 smoothedPosition = Vector3.Slerp(mainCamera.transform.position, cameraHolder.position, Time.deltaTime * camFollowSpeed);
        mainCamera.transform.position = smoothedPosition;
        
        Vector3 camLookAtPoint;
        float camLookAtSpeed2;
        if (comboManager.Attacking && comboManager.LookAtPointDuringCombo != null)
        {
            camLookAtPoint = playerTransform.position + targetPoint + new Vector3(0, camHeightLookPosition, 0);
            if(comboManager.LookAtPointDuringCombo.position.y >= 0.5f && comboManager.LookAtPointDuringCombo.position.y <= 2.5f)
            camLookAtPoint.y = comboManager.LookAtPointDuringCombo.position.y;

            camLookAtSpeed2 = 3.5f;
           
        }
        else
        {
           camLookAtPoint = playerTransform.position + new Vector3(0, camHeightLookPosition, 0) + targetPoint;
            camLookAtSpeed2 = camLookAtSpeed;

        }
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, Quaternion.LookRotation(camLookAtPoint - mainCamera.transform.position), camLookAtSpeed2 * Time.deltaTime);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerUserControl.CameraForward = Vector3.Scale(cameraHolder.forward, new Vector3(1, 0, 1)).normalized;
        playerUserControl.CameraRight = cameraHolder.right;
        FreeCamera();   
    }
}
