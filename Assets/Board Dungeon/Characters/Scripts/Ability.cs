using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    //Trigger of the Ability
    [SerializeField] protected string triggerName;
    //CurrentlLvl of Ability
    protected int lvl = 0;
    //Max lvl of Ability
    [Range(1, 5)]
    [SerializeField] protected int maxLvl = 3;
    //Array of cooldowns
    [SerializeField] protected float[] baseCooldownTimes = new float[1];
    //Cooldown boolean
    private bool canBeUse = true;
    //If ability has animation
    [SerializeField] protected bool animationNeeded = true;
    //If ability has particles
    [SerializeField] protected ParticleSystem particle;
    [SerializeField] protected bool playParticleOnStart = true;
    
    //Basic components

    protected AbilityManager abilityManager;
    protected Animator animator;

    protected virtual void Awake()
    {
        if (animationNeeded)
            animator = GetComponentInChildren<Animator>();

        abilityManager = GetComponent<AbilityManager>();
    }

    protected virtual void Start()
    {
    }

    //Method that invokes the ability
    public bool TriggeAbility()
    {
        if (canBeUse && !abilityManager.UsingAbility && CheckAdditionalContidions())
        {
            if (UseAbility())
            {
                BaseUseAbility();
                //Make sure attack collider is disabled
                abilityManager.StopDetectHit();
                //Indicating that the character is using a ability
                abilityManager.UsingAbility = true;
                OnSuccessfulUse();
                StartCoroutine(SetCoolDown());
                return true;
            }
        }
        return false;
    }

    //Method to override, here the logic of the ability is called
    protected abstract bool UseAbility();

    protected virtual void BaseUseAbility()
    {
        if (animator != null)
            animator.SetTrigger(triggerName);
        
        if (particle != null && playParticleOnStart)
            particle.Play();
    }

    //Method in which individual ability conditions will be checked before use ability
    protected abstract bool CheckAdditionalContidions();
  
    //A method whereby actions can be triggered after using a ability successfully
    protected abstract void OnSuccessfulUse();

    //Cooldown counter
    private IEnumerator SetCoolDown()
    {
        canBeUse = false;
        yield return new WaitForSeconds(baseCooldownTimes[ThingCalculator.CheckAbilityLvl(baseCooldownTimes.Length, lvl)]);
        canBeUse = true;
    }

}
