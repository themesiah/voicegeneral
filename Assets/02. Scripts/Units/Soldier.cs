using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject selectionObject;
    [HideInInspector]
    public Unit unit;
    

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
    }

    public void SetSelected()
    {
        if (selectionObject != null)
        {
            selectionObject.SetActive(true);
        }
    }

    public void SetUnselected()
    {
        if (selectionObject != null)
        {
            selectionObject.SetActive(false);
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