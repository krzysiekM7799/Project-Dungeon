using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUserControl : MonoBehaviour
{
    //UI control elements
    private FixedJoystick leftJoystick;
    private const string resourcesDirAbilitiesButtons = "Controls/Buttons/";
    private const string abilityButtonName = "AbilityButton";
    private FixedTouchField touchField;
    private FixedButton attackButton;
    private FixedButton[] abilityButtons;
    private FixedButton dashButton;
    private List<Vector2>[] abilityButtonLayouts;
    
    //Camera properties needed to transform the motion vector
    private Vector3 cameraForward;
    private Vector3 cameraRight;

    //Basic components needed by the class
    private PlayerCharacter playerCharacter;
    private PlayerAbilityManager playerAbilityManager;
    private ComboManager comboManager;

    public FixedJoystick LeftJoystick { get => leftJoystick; }
    public PlayerCharacter PlayerCharacter { get => playerCharacter; set => playerCharacter = value; }
    public FixedTouchField TouchField { get => touchField; }
    public Vector3 CameraForward { get => cameraForward; set => cameraForward = value; }
    public Vector3 CameraRight { get => cameraRight; set => cameraRight = value; }

    private void Awake()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
        playerAbilityManager = playerCharacter.PlayerAbilityManager;
        comboManager = playerCharacter.ComboManager;
    }
    void Start()
    {
        /* Szablon dla przyciskow skilli
         * var leftJoystickGO = Instantiate(Resources.Load<GameObject>(resourcesDirLeftJoystick + currentJoystickName));
          leftJoystickGO.transform.SetParent(uiCanvas,false);
          leftJoystick = leftJoystickGO.GetComponent<FixedJoystick>();*/

        leftJoystick = FindObjectOfType<FixedJoystick>();
        touchField = FindObjectOfType<FixedTouchField>();
        attackButton = FindObjectOfType<FixedButton>();
        SetAbilitiesButtonLayouts(playerAbilityManager.AbilitiesCount);
        SetAbilitiesButtons();
        AddListenersToButtons();
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
                    xCordRelativeToAttackButton = -70;
                    yCordRelativeToAttackButton = 80;
                    abilityButtonLayouts[0].Add(new Vector2(rectTransformAttackButton.localPosition.x + xCordRelativeToAttackButton, rectTransformAttackButton.localPosition.y + yCordRelativeToAttackButton));
                }
                break;
            case 2:
                { //Here set layout for 2 ability buttons
                    abilityButtonLayouts[1] = new List<Vector2>();
                    xCordRelativeToAttackButton = -105;
                    yCordRelativeToAttackButton = -60;
                    abilityButtonLayouts[1].Add(new Vector2(rectTransformAttackButton.localPosition.x + xCordRelativeToAttackButton, rectTransformAttackButton.localPosition.y + yCordRelativeToAttackButton));
                    xCordRelativeToAttackButton = -80;
                    yCordRelativeToAttackButton = 60;
                    abilityButtonLayouts[1].Add(new Vector2(rectTransformAttackButton.localPosition.x + xCordRelativeToAttackButton, rectTransformAttackButton.localPosition.y + yCordRelativeToAttackButton));
                }
                break;
            case 3:
                { //Here set layout for 3 ability buttons
                    abilityButtonLayouts[2] = new List<Vector2>();
                    xCordRelativeToAttackButton = -80;
                    yCordRelativeToAttackButton = -60;
                    abilityButtonLayouts[2].Add(new Vector2(rectTransformAttackButton.localPosition.x + xCordRelativeToAttackButton, rectTransformAttackButton.localPosition.y + yCordRelativeToAttackButton));
                    xCordRelativeToAttackButton = -80;
                    yCordRelativeToAttackButton = 60;
                    abilityButtonLayouts[2].Add(new Vector2(rectTransformAttackButton.localPosition.x + xCordRelativeToAttackButton, rectTransformAttackButton.localPosition.y + yCordRelativeToAttackButton));
                    xCordRelativeToAttackButton = 0;
                    yCordRelativeToAttackButton = 80;
                    abilityButtonLayouts[2].Add(new Vector2(rectTransformAttackButton.localPosition.x + xCordRelativeToAttackButton, rectTransformAttackButton.localPosition.y + yCordRelativeToAttackButton));
                }
                break;
            case 4:
                { //Here set layout for 4 ability buttons
                    abilityButtonLayouts[3] = new List<Vector2>();
                    xCordRelativeToAttackButton = -245;
                    yCordRelativeToAttackButton = -52;
                    abilityButtonLayouts[3].Add(new Vector2(xCordRelativeToAttackButton, yCordRelativeToAttackButton));
                    xCordRelativeToAttackButton = -219;
                    yCordRelativeToAttackButton = 132;
                    abilityButtonLayouts[3].Add(new Vector2(xCordRelativeToAttackButton, yCordRelativeToAttackButton));
                    xCordRelativeToAttackButton = -67;
                    yCordRelativeToAttackButton = 242;
                    abilityButtonLayouts[3].Add(new Vector2(xCordRelativeToAttackButton, yCordRelativeToAttackButton));
                    xCordRelativeToAttackButton = 119;
                    yCordRelativeToAttackButton = 216;
                    abilityButtonLayouts[3].Add(new Vector2(xCordRelativeToAttackButton, yCordRelativeToAttackButton));
                }
                break;
        }
    }





    private void SetAbilitiesButtons()
    {
        var buttonSkill = Resources.Load<GameObject>(resourcesDirAbilitiesButtons + abilityButtonName);
        abilityButtons = new FixedButton[playerAbilityManager.AbilitiesCount];

        for (int i = 0; i < playerAbilityManager.AbilitiesCount; i++)
        {
            var currentButtonSkill = Instantiate(buttonSkill);
            currentButtonSkill.transform.SetParent(attackButton.transform, false);
            abilityButtons[i] = currentButtonSkill.GetComponent<FixedButton>();
            abilityButtons[i].GetComponent<RectTransform>().localPosition = abilityButtonLayouts[playerAbilityManager.AbilitiesCount - 1][i];
            // currentButtonSkill.GetComponent<Image>().sprite = abilityManager.GetImgOfAbility(i);
            if (i == 0)
            {
                var dashButton2 = Instantiate(buttonSkill);
                dashButton2.transform.SetParent(attackButton.transform, false);
                dashButton = dashButton2.GetComponent<FixedButton>();
                dashButton.GetComponent<RectTransform>().localPosition = abilityButtons[i].GetComponent<RectTransform>().localPosition + new Vector3(-200, 0, 0);
            }

        }



    }


    private void AddListenersToButtons()
    {
        for (int i = 0; i < playerAbilityManager.AbilitiesCount; i++)
        {
            var abilityUI = abilityButtons[i].transform.GetComponent<AbilityCooldownUI>();

            //abilityManager.GetAbilitiesEvent(i).AddListener(Ping);


            abilityUI.AbilityImageSprite = playerAbilityManager.PlayerAbilities[i].playerAbilityProperties.Image;

            playerAbilityManager.PlayerAbilities[i].playerAbilityProperties.OnAbilityUse.AddListener(abilityUI.PerformUICooldown);
            // Debug.Log(abilityManager.abilities[i].playerAbilityProperties.OnAbilityUse);
            // abilityManager.GetImgOfAbility(i);

        }
    }
    private void HandleAbilitiyButtons()
    {
        for (int i = 0; i < abilityButtons.Length; i++)
        {
            if (abilityButtons[i].Pressed)
            {
                playerAbilityManager.PerformAbility(i);
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


        if (Input.GetKeyDown(KeyCode.LeftShift) || dashButton.Pressed)
        {
            if (!playerAbilityManager.UsingAbility)
                playerCharacter.MakeDash();
            dashButton.Pressed = false;

        }

        HandleButtons();



    }
}
