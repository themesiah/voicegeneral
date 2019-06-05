using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Shooting State")]
public class ShootingState : ScriptableState
{
    public override void OnEnterState(UnitController controller)
    {
        
        // First, play animation
        Soldier[] soldiers = controller.Soldiers;
        foreach (Soldier s in soldiers)
        {
            s.PlayAnimation("disparo flecha", 0f, 0.3f);
        }
        // Stop the target
        TargetController.instance.StopTarget();
        // Calculate the force
        Vector3 point = TargetController.instance.GetPoint();
        Vector3 force = CalculateForce(controller, controller.Unit.transform.position, point);
        foreach (Soldier s in soldiers)
        {
            controller.Unit.StartCoroutine(SpawnArrow(controller, s, force));
        }
    }

    public override void OnExitState(UnitController controller)
    {
    }

    public override void OnTick(UnitController controller)
    {
    }

    private Vector3 CalculateForce(UnitController controller, Vector3 position, Vector3 target)
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
        return vector * force * Mathf.Sqrt(2f);
    }

    IEnumerator SpawnArrow(UnitController controller, Soldier s, Vector3 force)
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.3f));
        GameObject go = Instantiate(controller.Data.ProjectilePrefab, s.transform.position, Quaternion.identity);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.VelocityChange);
    }
}
