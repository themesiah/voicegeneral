using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Renderer[] renderers;
    private Material[] previousMaterials;
    [SerializeField]
    private Material selectedMaterial;
    [HideInInspector]
    public Unit unit;

    private Unit engagedTo;
    private bool engaged;
    //private Soldier currentTarget;
    

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool dead = false;
    
    private static float distanceToTarget = -20f;

    private Coroutine animationCoroutine;

    void Start()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.rotation;
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
        if (renderers == null || renderers.Length == 0)
        {
            renderers = GetComponentsInChildren<Renderer>();
        }
        previousMaterials = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; ++i)
        {
            previousMaterials[i] = renderers[i].material;
        }
    }

    public void SetSelectedMaterial()
    {
        for (int i = 0; i < renderers.Length; ++i)
        {
            renderers[i].material = selectedMaterial;
        }
    }

    public void UnsetSelectedMaterial()
    {
        for (int i = 0; i < renderers.Length; ++i)
        {
            renderers[i].material = previousMaterials[i];
        }
    }

    public void PlayAnimation(string animationName, float min = 0, float max = 0)
    {
        if (anim != null)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            animationCoroutine = StartCoroutine(PlayAnimationDelayed(animationName, min, max));
        }
    }

    IEnumerator PlayAnimationDelayed(string animationName, float min, float max)
    {
        yield return new WaitForSeconds(Random.Range(min, max));
        anim.Play(animationName);
        animationCoroutine = null;
    }

    public void Die()
    {
        dead = true;
        PlayAnimation("muerte", 0f, 0.3f);
    }

    public bool IsDead()
    {
        return dead;
    }

    private void Update()
    {
    }
}