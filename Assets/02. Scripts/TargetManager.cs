using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {
    [SerializeField]
    GameObject particlesCan;
    [SerializeField]
    GameObject particlesCant;

    bool isValid = false;

    public void SetValidTarget(bool valid)
    { 
        if (isValid != valid)
        {
            isValid = valid;
            if (valid == true)
            {
                particlesCan.SetActive(true);
                particlesCant.SetActive(false);
            } else
            {
                particlesCan.SetActive(false);
                particlesCant.SetActive(true);
            }
        }
    }

    public bool IsValidTarget()
    {
        return isValid;
    }
}
