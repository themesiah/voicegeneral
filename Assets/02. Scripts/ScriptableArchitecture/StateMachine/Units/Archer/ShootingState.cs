using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Shooting State")]
public class ShootingState : ScriptableState
{
    struct ShotInfo
    {
        public Vector3 force;
        public float time;
    }

    public AudioClip shootSound;

    public override void OnEnterState(UnitController controller)
    {
        
        // First, play animation
        Soldier[] soldiers = controller.Soldiers;
        foreach (Soldier s in soldiers)
        {
            s.PlayAnimation("disparo flecha", 0f, 0.3f);
        }
        // Calculate the force
        Vector3 point = controller.Input.GetPoint();
        ShotInfo shotInfo = CalculateForce(controller, controller.Unit.transform.position, point);
        foreach (Soldier s in soldiers)
        {
            controller.Unit.StartCoroutine(SpawnArrow(controller, s, shotInfo));
        }
        controller.Unit.StartCoroutine(SpawnDamage(controller, point, shotInfo));
        controller.Unit.PlayAudio(shootSound);
    }

    public override void OnExitState(UnitController controller)
    {
    }

    public override void OnTick(UnitController controller)
    {
    }

    private ShotInfo CalculateForce(UnitController controller, Vector3 position, Vector3 target)
    {
        // First we get the 2D vector from the archers position to the target
        Vector3 vector = target - position;
        vector.y = 0f;
        // We get the distance from the magnitude of the vector
        float distance = vector.magnitude;
        // We normalize it, so then we can apply the force
        vector.Normalize();
        // 0.5 is 45º for the y angle
        vector.y = 0.5f;
        // Formula from a group of equations: Y speed is equal to X speed, as we apply the same force to both.
        float time = Mathf.Sqrt(distance/4.9f);
        // We have the time, so we can now know the force to apply to get to that point in that time.
        float force = distance / time;
        // Why sqrt of 2? Who knows. I don't know why it works, but it works.
        ShotInfo si = new ShotInfo();
        si.time = time;
        si.force = vector * force * Mathf.Sqrt(2f);
        return si;
    }

    IEnumerator SpawnArrow(UnitController controller, Soldier s, ShotInfo shotInfo)
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.3f));
        GameObject go = Instantiate(controller.Data.ProjectilePrefab, s.transform.position, Quaternion.identity);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.AddForce(shotInfo.force, ForceMode.VelocityChange);
        GameObject.Destroy(go, shotInfo.time);
    }

    IEnumerator SpawnDamage(UnitController controller, Vector3 target, ShotInfo shotInfo)
    {
        yield return new WaitForSeconds(shotInfo.time-1f);
        Instantiate(controller.Data.AreaDamage, target, Quaternion.identity);
    }
}
