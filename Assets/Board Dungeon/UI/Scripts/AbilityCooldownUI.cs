using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownUI : MonoBehaviour
{

    private Image abilityImage;

    public Sprite AbilityImageSprite { set => abilityImage.sprite = value; }

    public void PerformUICooldown(float cooldown)
    {
        Debug.Log("STATRTUJEEEEEEEE");
        StartCoroutine(Countdown(cooldown));
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        abilityImage = GetComponent<Image>();
    }
   
    private IEnumerator Countdown(float cooldown)
    {
        float duration = cooldown; 
                             
        float normalizedTime = 0;
        while (normalizedTime <= 1.1f)
        {
            abilityImage.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
    }

}
