using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected string _name;

    [SerializeField] protected int lvl = 0;
    [SerializeField] protected float[] baseCooldownTimes = new float[3];
    private bool canBeUse = true;
    [SerializeField] protected bool canCharacterRotateDuringAbility;
    
    protected AbilityType abilityType;
    
    [SerializeField] protected bool animationNeeded = true;
    protected Animator animator;
    [SerializeField] protected ParticleSystem particle;
    [SerializeField] protected ParentOfObject particleParent;
    protected AbilityManager abilityManager;
    [SerializeField] public PlayerAbilityProperties playerAbilityProperties;
    protected Character character;

    protected virtual void Awake()
    {
        if(animationNeeded)
        animator = GetComponentInChildren<Animator>();
        
        abilityManager = GetComponent<AbilityManager>();
        character = GetComponent<Character>();
   
    }
    
    protected abstract bool UseAbility();
    public bool TriggeAbility()
    {
        Debug.Log("wciskam");
        if (canBeUse && !abilityManager.UsingAbility && CheckAdditionalContidions())
        {
            Debug.Log("probuje1");
            if (UseAbility())
            {
                
                abilityManager.StopDetectHit();
                abilityManager.UsingAbility = true;
                OnSuccessfulUse();
                Debug.Log("probuje2");
                StartCoroutine(SetCoolDown());
                return true;
            }        
        }
        return false;
    }
    protected virtual void BaseUseAbility()
    {
        SetAbilityValues();
        if(animator != null)
            animator.SetTrigger(_name);
        if (particle != null)
        {
            particle.Play();
        }
            
        character.RotationEnabled = canCharacterRotateDuringAbility;
    }

    protected virtual bool CheckAdditionalContidions()
    {
        return true;
    }
    
    protected virtual void SetAbilityValues()
    {

    }
    protected virtual void OnSuccessfulUse()
    {

    }

    private IEnumerator SetCoolDown()
    {
        Debug.Log("Ustawiam cooldown" + baseCooldownTimes[lvl]);
        canBeUse = false;
        yield return new WaitForSeconds(baseCooldownTimes[lvl]);
        canBeUse = true;
        Debug.Log("zdejmuje cooldown");

    }
}
