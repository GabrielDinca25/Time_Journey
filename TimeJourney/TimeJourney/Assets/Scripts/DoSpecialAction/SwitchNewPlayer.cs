using UnityEngine;

public class SwitchNewPlayer : MonoBehaviour
{
    // The simple player camera instance
    public Cinemachine.CinemachineVirtualCamera simplePlayerCam;

    // The sword player camera instance
    public Cinemachine.CinemachineVirtualCamera playerCam;

    // The sword logic gameObject
    public GameObject swordLogic;

    // The stone logic gameObject
    public GameObject stoneLogic;

    // The simple player gameObject
    public GameObject simplePlayerObject;

    // The sword and stone tutorial dialog game object
    public GameObject useSwordAndStoneTutorial;

    // The enemy body game object
    [Tooltip("The first enemy body for getting components")]
    public GameObject enemyBody;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        if (simplePlayerObject != null && simplePlayerObject.transform.position == Vector3.zero)
        {
            GameController.instance.SpecialAction = GivePlayerItems;
        }
        if (GameController.instance.saveSystemSO.m_PlayerPositionX != 0 && GameController.instance.saveSystemSO.m_PlayerPositionY != 0)
        {
            GivePlayerItems();
            GameController.instance.GameOver();
        }
        enabled = false;
    }

    /// <summary>
    /// Enables the items of the player
    /// </summary>
    public void GivePlayerItems()
    {
        swordLogic.SetActive(true);
        stoneLogic.SetActive(true);

        simplePlayerObject.GetComponent<PlayerMovement>().enabled = false;

        GameController.instance.player.transform.position = simplePlayerObject.transform.position;
        GameController.instance.player.transform.rotation = simplePlayerObject.transform.rotation;

        simplePlayerCam.enabled = false;
        playerCam.enabled = true;

        GameController.instance.player.GetComponent<CharacterController2D>().m_FacingRight = simplePlayerObject.GetComponent<PlayerMovement>().m_FacingRight;
        GameController.instance.player.GetComponent<PlayerMovementWithSword>().enabled = true;

        Destroy(simplePlayerObject);

        useSwordAndStoneTutorial.SetActive(true);

        Destroy(enemyBody.GetComponent<DamageToSimplePlayer>());
        enemyBody.AddComponent<DamageToPlayer>();
    }
}