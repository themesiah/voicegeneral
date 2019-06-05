using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Health))]
public class IDamageableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        IDamageable d = target as IDamageable;
        if (GUILayout.Button("Damage 1"))
            d.TakeDamage(1);
        if (GUILayout.Button("Damage 10"))
            d.TakeDamage(10);
        if (GUILayout.Button("Damage 50"))
            d.TakeDamage(50);
        if (GUILayout.Button("Damage 100"))
            d.TakeDamage(100);
    }
}