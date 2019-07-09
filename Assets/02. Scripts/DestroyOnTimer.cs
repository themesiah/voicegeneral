using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    [SerializeField]
    private float timeToDie = 3f;
    private float timer = 0f;
    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToDie)
        {
            Destroy(this.gameObject);
        }
    }
}
