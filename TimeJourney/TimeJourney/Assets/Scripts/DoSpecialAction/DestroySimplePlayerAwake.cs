using UnityEngine;

public class DestroySimplePlayerAwake : MonoBehaviour
{
    // Switch new player instance
    public SwitchNewPlayer snp;

    /// <summary>
    /// The method called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (transform.position != Vector3.zero)
        {
            snp.GivePlayerItems();
            Destroy(gameObject);
        }
    }
}
