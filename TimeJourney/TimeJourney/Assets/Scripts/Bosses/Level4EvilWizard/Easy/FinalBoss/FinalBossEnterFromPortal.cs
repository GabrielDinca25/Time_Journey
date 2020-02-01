using UnityEngine;

public class FinalBossEnterFromPortal : MonoBehaviour
{
    // Boolean for checking if boss has already entered
    public bool oneTime;

    /// <summary>
    /// Method called every frame
    /// </summary>
    private void Update()
    {
        transform.localScale += new Vector3(0.001f, 0.001f, 0);
        if (transform.localScale.x >= 0.1f)
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0);
            if (Mathf.RoundToInt(GetComponent<FinalBossHealth>().m_CurrentHealth) > GetComponent<FinalBossHealth>().m_maxHp / 2)
            {
                GetComponent<FinalBossMovement>().Attack();
            }
            else
            {

                if (!oneTime)
                {
                    GetComponent<FinalBossMovement>().enabled = false;
                    GetComponent<FinalBossMovementV2>().enabled = true;
                    oneTime = true;
                }
                GetComponent<FinalBossMovementV2>().Attack();
            }
            enabled = false;
        }
    }
}
