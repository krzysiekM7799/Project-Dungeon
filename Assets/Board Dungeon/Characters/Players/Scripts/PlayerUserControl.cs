using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUserControl : MonoBehaviour
{
    //UI control elements
    FixedJoystick leftJoystick;
    const string resourcesDirLeftJoystick = "Controls/Joysticks/";
    const string currentJoystickName = "SimpleLeftJoystick";
    FixedTouchField touchField;
    Vector3 cameraForward;
    Vector3 cameraRight;

    PlayerCharacter playerCharacter;
    Transform uiCanvas;

    

    public FixedJoystick LeftJoystick { get => leftJoystick; set => leftJoystick = value; }
    public PlayerCharacter PlayerCharacter { get => playerCharacter; set => playerCharacter = value; }
    public FixedTouchField TouchField { get => touchField; set => touchField = value; }
    public Vector3 CameraForward { get => cameraForward; set => cameraForward = value; }
    public Vector3 CameraRight { get => cameraRight; set => cameraRight = value; }




    // Start is called before the first frame update

    private void Awake()
    {
        uiCanvas = FindObjectOfType<UICanvas>().transform;

        /* Szablon dla przyciskow skilli
         * var leftJoystickGO = Instantiate(Resources.Load<GameObject>(resourcesDirLeftJoystick + currentJoystickName));
          leftJoystickGO.transform.SetParent(uiCanvas,false);
          leftJoystick = leftJoystickGO.GetComponent<FixedJoystick>();*/

        leftJoystick = FindObjectOfType<FixedJoystick>();
        touchField = FindObjectOfType<FixedTouchField>();
        playerCharacter = GetComponent<PlayerCharacter>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var vInput = leftJoystick.Vertical;
        var hInput = leftJoystick.Horizontal;


        /* m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
         //ZAMIENIC NA Vinput Hinput
         m_Move = vInput * m_CamForward + hInput * m_Cam.right;*/

        //ZAMIENIC NA Vinput Hinput
        Vector3 move = vInput * cameraForward + hInput * cameraRight;
        
    //    Vector3  move = vInput * Vector3.forward + hInput * Vector3.right;
        playerCharacter.Move(move);


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerCharacter.MakeDash();
        }


    }
}
