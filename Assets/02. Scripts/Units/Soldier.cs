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

    public Unit unit;

    private Unit engagedTo;
    private bool engaged;
    private Soldier currentTarget;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool dead = false;

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
            StartCoroutine(PlayAnimationDelayed(animationName, min, max));
        }
    }

    IEnumerator PlayAnimationDelayed(string animationName, float min, float max)
    {
        yield return new WaitForSeconds(Random.Range(min, max));
        anim.Play(animationName);
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

    public void Engage(Unit enemy)
    {
        engagedTo = enemy;
        currentTarget = GetTarget();
        engaged = true;
    }

    public void Disengage()
    {
        engagedTo = null;
        currentTarget = null;
        engaged = false;
    }

    private Soldier GetTarget()
    {
        int enemies = engagedTo.soldiers.Count;
        if (enemies == 0)
        {
            return null;
        }
        int me = unit.soldiers.IndexOf(this);
        int target = me % enemies;
        return engagedTo.soldiers[target];
    }

    private void Update()
    {
        if (dead == false)
        {
            if (engaged == true)
            {
                if (currentTarget == null || currentTarget.IsDead())
                {
                    currentTarget = GetTarget();
                }
                if (currentTarget != null)
                {
                    Vector3 pos = Vector3.MoveTowards(transform.position, currentTarget.transform.position + currentTarget.transform.forward, 5f * Time.deltaTime);
                    transform.position = pos;
                    transform.rotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position, Vector3.up);
                }
            }
            else
            {
                if (transform.localPosition != originalPosition)
                {
                    Vector3 pos = Vector3.MoveTowards(transform.localPosition, originalPosition, 5f * Time.deltaTime);
                    transform.localPosition = pos;
                    transform.rotation = originalRotation;
                }
            }
        }
    }
}