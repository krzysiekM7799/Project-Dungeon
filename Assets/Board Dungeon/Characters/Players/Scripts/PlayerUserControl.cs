using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUserControl : MonoBehaviour
{
    //UI control elements
    private FixedJoystick leftJoystick;
    private const string resourcesDirAbilitiesButtons = "Controls/Buttons/";
    private const string abilityButtonName = "AbilityButton";
    private FixedTouchField touchField;
    private FixedButton attackButton;
    public FixedButton[] abilityButtons;
    private List<Vector2>[] abilityButtonLayouts;
    Vector3 cameraForward;
    Vector3 cameraRight;
    AbilityManager abilityManager;
    PlayerCharacter playerCharacter;
    ComboManager comboManager;
    Transform uiCanvas;

    

    public FixedJoystick LeftJoystick { get => leftJoystick;}
    public PlayerCharacter PlayerCharacter { get => playerCharacter; set => playerCharacter = value; }
    public FixedTouchField TouchField { get => touchField;}
    public Vector3 CameraForward { get => cameraForward; set => cameraForward = value; }
    public Vector3 CameraRight { get => cameraRight; set => cameraRight = value; }




    // Start is called before the first frame update

    private void Awake()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
        
    }
    private void SetAbilitiesButtonLayouts(int buttonsCount)
    {
        abilityButtonLayouts = new List<Vector2>[buttonsCount];
        var rectTransformAttackButton = attackButton.GetComponent<RectTransform>();
        int xCordRelativeToAttackButton;
        int yCordRelativeToAttackButton;
        switch (buttonsCount)
        {
            case 1:
                {   //Here set layout for 1 ability button
                    abilityButtonLayouts[0] = new List<Vector2>();
                    xCordRelativeToAttackButton = -50;
                    yCordRelativeToAttackButton = 50;
                    abilityButtonLayouts[0].Add(new Vector2(rectTransformAttackButton.localPosition.x + xCordRelativeToAttackButton, rectTransformAttackButton.localPosition.y + yCordRelativeToAttackButton));
                }
                break;
            case 2:
                { //Here set layout for 2 ability buttons
                    abilityButtonLayouts[1] = new List<Vector2>();
                    xCordRelativeToAttackButton = -80;
                    yCordRelativeToAttackButton = -60;
                    abilityButtonLayouts[1].Add(new Vector2(rectTransformAttackButton.localPosition.x + xCordRelativeToAttackButton, rectTransformAttackButton.localPosition.y + yCordRelativeToAttackButton));
                    xCordRelativeToAttackButton = -80;
                    yCordRelativeToAttackButton = 60;
                    abilityButtonLayouts[1].Add(new Vector2(rectTransformAttackButton.localPosition.x + xCordRelativeToAttackButton, rectTransformAttackButton.localPosition.y + yCordRelativeToAttackButton));
                }
                break;
        }    
    }

       


    
    private void SetAbilitiesButtons()
    {
        var buttonSkill = Resources.Load<GameObject>(resourcesDirAbilitiesButtons + abilityButtonName);
        abilityButtons = new FixedButton[abilityManager.AttackAbilitiesCount + abilityManager.BuffAbilitiesCount];
        
        for (int i = 0; i < abilityManager.AttackAbilitiesCount + abilityManager.BuffAbilitiesCount; i++)
        {
            var currentButtonSkill = Instantiate(buttonSkill);
            currentButtonSkill.transform.SetParent(uiCanvas, false);
            abilityButtons[i] = currentButtonSkill.GetComponent<FixedButton>();
            abilityButtons[i].GetComponent<RectTransform>().localPosition = abilityButtonLayouts[abilityManager.AttackAbilitiesCount + abilityManager.BuffAbilitiesCount - 1][i];
        }
       


    }
    void Start()
    {
        uiCanvas = FindObjectOfType<UICanvas>().transform;

        /* Szablon dla przyciskow skilli
         * var leftJoystickGO = Instantiate(Resources.Load<GameObject>(resourcesDirLeftJoystick + currentJoystickName));
          leftJoystickGO.transform.SetParent(uiCanvas,false);
          leftJoystick = leftJoystickGO.GetComponent<FixedJoystick>();*/

        leftJoystick = FindObjectOfType<FixedJoystick>();
        touchField = FindObjectOfType<FixedTouchField>();
        attackButton = FindObjectOfType<FixedButton>();
        abilityManager = playerCharacter.AbilityManager;
        comboManager = GetComponent<ComboManager>();
        SetAbilitiesButtonLayouts(abilityManager.AttackAbilitiesCount + abilityManager.BuffAbilitiesCount);
        SetAbilitiesButtons();
    }



    private void HandleAbilitiyButtons()
    {
        for (int i = 0; i < abilityButtons.Length; i++)
        {
            if (abilityButtons[i].Pressed)
            {
                abilityManager.PerformAbility(i);
                abilityButtons[i].Pressed = false;
            }
        }
    }
    private void HandleAttackButton()
    {
        if (attackButton.Pressed)
        {
            comboManager.HandleAttack();
            attackButton.Pressed = false;
        }
    }
    public void HandleButtons()
    {
        HandleAbilitiyButtons();
        HandleAttackButton();
        
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
            if(!abilityManager.UsingAbility)
            playerCharacter.MakeDash();
        }

        HandleButtons();



    }
}
