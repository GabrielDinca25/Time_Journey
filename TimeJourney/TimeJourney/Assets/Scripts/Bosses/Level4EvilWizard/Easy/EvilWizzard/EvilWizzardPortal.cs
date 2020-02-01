using System.Collections;
using UnityEngine;

public class EvilWizzardPortal : MonoBehaviour
{
    // The portal animation
    private IEnumerator portalAnimation;

    // The particle system renderer of the portal
    private ParticleSystemRenderer portalRenderer;

    // The postition where the portal teleports you
    public Vector3[] teleportPosition;

    // The boss gameObject
    public GameObject boss;

    /// <summary>
    /// The method called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        portalRenderer = GetComponentInChildren<ParticleSystem>().GetComponent<ParticleSystemRenderer>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        int random = Random.Range(0, teleportPosition.Length);
        transform.position = teleportPosition[random];
        portalAnimation = PortalIncreaseAnimation();
        StartCoroutine(portalAnimation);
    }

    /// <summary>
    /// Increases the portal size
    /// </summary>
    /// <returns></returns>
    public IEnumerator PortalIncreaseAnimation()
    {
        while (portalRenderer.minParticleSize < 0.3f)
        {
            portalRenderer.minParticleSize += 0.01f;
            portalRenderer.maxParticleSize = portalRenderer.minParticleSize;
            yield return new WaitForSeconds(.1f);
        }
        EnableBoss();
    }

    /// <summary>
    /// Decreases the portal size
    /// </summary>
    /// <returns></returns>
    public IEnumerator PortalDecreaseAnimation()
    {
        while (portalRenderer.maxParticleSize > 0)
        {
            portalRenderer.maxParticleSize -= 0.01f;
            portalRenderer.minParticleSize = portalRenderer.maxParticleSize;

            yield return new WaitForSeconds(.1f);
        }
        //Disables the gameObject
        gameObject.SetActive(false);

    }

    /// <summary>
    /// Disables the portal
    /// </summary>
    public void Disable()
    {
        portalAnimation = PortalDecreaseAnimation();
        StartCoroutine(portalAnimation);
    }

    /// <summary>
    /// The function called when the behaviour becomes disabled.
    /// </summary>
    public void OnDisable()
    {
        if (boss.GetComponent<Health>().m_CurrentHealth > 0)
        {
            Invoke("Enable", 0.5f);
        }
    }

    /// <summary>
    /// Enables the current gameobject
    /// </summary>
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Enables the boss
    /// </summary>
    public void EnableBoss()
    {
        boss.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0);
        boss.SetActive(true);
    }
}
