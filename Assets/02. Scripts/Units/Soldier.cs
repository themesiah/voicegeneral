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

    void Start()
    {
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

    void Update()
    {

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
}