using System;
using UnityEngine;

public class StoneAttacks : MonoBehaviour
{
    // The stone attacks instance
    public static StoneAttacks instance;

    // The FireWeapon delegate function
    public Action FireWeapon = delegate { };

    // The player instance
    private PlayerMovementWithSword pmws;

    // The target of the attack
    private Vector3 target;

    // The camera of the scene
    public Camera cam;

    // Position from where shots begin
    public Transform shotPosition;

    // Parent for the current shots to use, may change for diferent shots
    public GameObject shotsParent;

    // GameObject to instantiate if we don't have enough shots
    public GameObject shot;

    // The current shot of the stone
    private GameObject currentShot;

    // Bool indicating if levtiation is enabled
    public bool levitation;

    // Layer mask for the levitation feature
    public LayerMask levitationLayerMask;

    /// <summary>
    /// The method called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        // Get PlayerMovement with sword component
        pmws = GameController.instance.player.GetComponent<PlayerMovementWithSword>();
        FireWeapon = StoneAttack;
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && pmws.canAttack)
        {
            FireWeapon();
        }
    }

    /// <summary>
    /// Start the levitation stone feature
    /// </summary>
    public void StoneLevitation()
    {
        StoneAnimation();
        Vector2 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 1f, levitationLayerMask);
        if ((hit.collider != null && hit.collider.gameObject.GetComponent<PickUp>()))
        {
            hit.collider.gameObject.GetComponent<PickUp>().enabled = true;
        }
        else if (hit.collider != null && hit.collider.gameObject.GetComponent<PickUpBomb>())
        {
            hit.collider.gameObject.GetComponent<PickUpBomb>().enabled = true;
        }
    }

    /// <summary>
    /// Starts the stone animation
    /// </summary>
    public void StoneAttack()
    {
        StoneAnimation();
    }

    /// <summary>
    /// Instantiates shot
    /// </summary>
    public void Shot()
    {
        // Levitation stone is the only one to not instantiate shots
        if (!levitation)
        {
            currentShot = GetNextShot();
            // instantiate shot
            StoneInstantiate(currentShot, shotPosition.position);
        }
    }

    /// <summary>
    /// Triggers the StoneAnimation
    /// </summary>
    public void StoneAnimation()
    {
        pmws.canAttack = false;
        pmws.animator.SetTrigger("StoneAttack");
    }

    /// <summary>
    /// Gets next shot
    /// </summary>
    /// <returns>The gameobject of the shot</returns>
    public GameObject GetNextShot()
    {
        for (int i = 0; i < shotsParent.transform.childCount; i++)
        {
            if (!shotsParent.transform.GetChild(i).gameObject.activeSelf)
            {
                return shotsParent.transform.GetChild(i).gameObject;
            }
        }
        GameObject newShot = Instantiate(shot, shotPosition.position, Quaternion.identity);
        newShot.transform.SetParent(shotsParent.transform);
        newShot.SetActive(false);
        return newShot;
    }

    /// <summary>
    /// Instantiates shot
    /// </summary>
    /// <param name="shot">The shot gameObject</param>
    /// <param name="positionToInstantiate">The position to instantiate the shot</param>
    public void StoneInstantiate(GameObject shot, Vector2 positionToInstantiate)
    {
        shot.transform.position = positionToInstantiate;
        Vector3 targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
        shot.GetComponent<ShotMovement>().target = new Vector3(targetPos.x, targetPos.y, 0);
        shot.SetActive(true);
    }
}
