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

    private bool canBeUse = true;

    [SerializeField] protected bool animationNeeded = true;
    protected Animator animator;

    [SerializeField] protected ParticleSystem particle;
    [SerializeField] protected bool playParticleOnStart = true;
    //Basic components
    protected AbilityManager abilityManager;

    protected virtual void Awake()
    {
        if (animationNeeded)
            animator = GetComponentInChildren<Animator>();

        abilityManager = GetComponent<AbilityManager>();
    }

    protected virtual void Start()
    {
    }

    protected abstract bool UseAbility();

    public bool TriggeAbility()
    {
        if (canBeUse && !abilityManager.UsingAbility && CheckAdditionalContidions())
        {
            if (UseAbility())
            {
                BaseUseAbility();
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
        if (animator != null)
            animator.SetTrigger(triggerName);
        if (particle != null && playParticleOnStart)
        {
            particle.Play();
        }
    }

    protected abstract bool CheckAdditionalContidions();

    protected abstract void OnSuccessfulUse();

    private IEnumerator SetCoolDown()
    {
        Debug.Log("Ustawiam cooldown" + baseCooldownTimes[ThingCalculator.CheckAbilityLvl(baseCooldownTimes.Length, lvl)]);
        canBeUse = false;
        yield return new WaitForSeconds(baseCooldownTimes[ThingCalculator.CheckAbilityLvl(baseCooldownTimes.Length, lvl)]);
        canBeUse = true;
        Debug.Log("zdejmuje cooldown");

    }

}
