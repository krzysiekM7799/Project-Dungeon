using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected string _name;
   // [SerializeField] protected Sprite image;
    [SerializeField] protected int lvl = 0;
    [SerializeField] protected float[] baseCooldownTimes = new float[3];
    private bool canBeUse = true;
    //public CustomUnityEvents.FloatUnityEvent OnAbilityUse = new CustomUnityEvents.FloatUnityEvent();
    protected AbilityType abilityType;
    // public Sprite Image { get => image; }
    [SerializeField] protected bool animationNeeded = true;
    protected Animator animator;
    [SerializeField] protected ParticleSystem particle;
    protected AbilityManager abilityManager;

    protected void Awake()
    {
        if(animationNeeded)
        animator = GetComponentInChildren<Animator>();
        
        abilityManager = GetComponent<AbilityManager>();
}
    protected abstract bool UseAbility();
    public void TriggeAbility()
    {
        Debug.Log("wciskam");
        if (canBeUse && CheckAdditionalContidions())
        {
            Debug.Log("probuje1");
            if (UseAbility())
            {
                OnSuccessfulUse();
                Debug.Log("probuje2");
                StartCoroutine(SetCoolDown());
            }        
        }
      
    }
    protected virtual void BaseUseAbility()
    {
        SetAbilityValues();
        if(animator != null)
            animator.SetTrigger(_name);
        if (particle != null)
            particle.Play();
    }
   
    public abstract bool CheckAdditionalContidions();
    
    protected abstract void SetAbilityValues();
    protected abstract void  OnSuccessfulUse();

    private IEnumerator SetCoolDown()
    {
        Debug.Log("Ustawiam cooldown" + baseCooldownTimes[lvl]);
        canBeUse = false;
        yield return new WaitForSeconds(baseCooldownTimes[lvl]);
        canBeUse = true;
        Debug.Log("zdejmuje cooldown");

    }
}
