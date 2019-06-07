using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {
    [SerializeField]
    GameObject particlesCan;
    [SerializeField]
    GameObject particlesCant;

    bool isValid = false;

    bool validSignal = false;
    bool invalidSignal = false;

    public void SetValidTarget(bool valid)
    { 
        isValid = valid;
        if (valid == true)
        {
            particlesCan.SetActive(true);
            validSignal = true;
        } else
        {
            particlesCant.SetActive(true);
            invalidSignal = true;
        }
    }

    private void LateUpdate()
    {
        if (!validSignal)
        {
            particlesCan.SetActive(false);
        }
        if (!invalidSignal)
        {
            particlesCant.SetActive(false);
        }
        
        invalidSignal = false;
        validSignal = false;
    }

    public bool IsValidTarget()
    {
        return isValid;
    }
}
