using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {
    public static TargetController instance;

    [SerializeField]
    private GameObject targetPrefab;
    [SerializeField]
    private LayerMask mask;

    private TargetManager target;
    private Camera cam;

    private bool isTargeting;
    private Vector3 lastPoint = Vector3.zero;

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
        target = Instantiate(targetPrefab).GetComponent<TargetManager>();
        target.gameObject.SetActive(false);
        isTargeting = false;
    }

    public void StartTarget()
    {
        isTargeting = true;
        //target.SetActive(true);
    }

    public void StopTarget()
    {
        isTargeting = false;
        target.gameObject.SetActive(false);
    }

    public bool IsTargeting()
    {
        return isTargeting;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (isTargeting == true)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, mask))
            {
                target.transform.position = hitInfo.point + Vector3.up * 0.1f;
                lastPoint = hitInfo.point;
                if (!target.gameObject.activeSelf)
                {
                    target.gameObject.SetActive(true);
                }
            }
        }
	}

    public Vector3 GetPoint()
    {
        return lastPoint;
    }

    public void ValidTarget(bool valid)
    {
        target.SetValidTarget(valid);
    }

    public bool IsValid()
    {
        return target.IsValidTarget();
    }
}
