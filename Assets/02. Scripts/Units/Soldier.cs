using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private MeshRenderer[] renderers;
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
            renderers = GetComponentsInChildren<MeshRenderer>();
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

    public void PlayAnimation(string animationName)
    {
        if (anim != null)
        {
            anim.Play(animationName);
        }
    }
}