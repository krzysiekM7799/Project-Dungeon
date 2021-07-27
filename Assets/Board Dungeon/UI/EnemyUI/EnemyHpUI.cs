using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Slider hpSlider;
    private Camera mainCamera;

    void Start()
    {
        hpSlider.value = 1;
        mainCamera = Camera.main;
    }
    public void SetHpValueToEnemyUI(int currentHP, int maxHP)
    {
        hpText.text = currentHP.ToString();
        hpSlider.value = (float)currentHP / maxHP;
    }
    
    void Update()
    {
        transform.LookAt(mainCamera.transform.position);
    }
}
