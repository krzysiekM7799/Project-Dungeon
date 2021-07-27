using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleChecker : MonoBehaviour
{
    public ParticleSystem ps;
    // Start is called before the first frame update
    public void StartParticle()
    {
        ps.Play();
    }

    // Update is called once per frame
    public void StopParticle()
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
