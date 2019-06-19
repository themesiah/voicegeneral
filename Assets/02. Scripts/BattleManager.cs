using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private RuntimeUnitSet allySet;
    [SerializeField]
    private RuntimeUnitSet enemySet;

    [SerializeField]
    private Text endText;
    [SerializeField]
    private Button endButton;

    private bool finished = false;

	void Update () {
        if (allySet.Items.Count == 0 && !finished)
        {
            endText.text = "Has perdido la batalla, ¡pero no la guerra!";
            Finish();
        }
        else if (enemySet.Items.Count == 0 && !finished)
        {
            endText.text = "¡La victoria es tuya!";
            Finish();
        }
	}

    private void Finish()
    {
        finished = true;
        endButton.gameObject.SetActive(true);
        endText.gameObject.SetActive(true);
    }
}
