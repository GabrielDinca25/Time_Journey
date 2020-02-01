using UnityEngine;

public class StoneChanger : MonoBehaviour
{
    // List of prefabs of all type that player can change ex:fire/ice
    public GameObject[] prefabtype;

    // The parent of the shots
    public GameObject[] parentForType;

    // List of particle system of all types
    public GameObject[] particleSystemType;

    // The selected stone
    public int stoneAvailable;

    // The stone attacks component
    private StoneAttacks StoneAttacks;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        // Get the StoneAttacks component
        StoneAttacks = GetComponent<StoneAttacks>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        SetPS("Fire");
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    private void Update()
    {
        //Check input for chainging stones
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StoneAttacks.FireWeapon = StoneAttacks.StoneAttack;
            StoneAttacks.levitation = false;
            ChangeStone("Fire");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && stoneAvailable > 1)
        {
            StoneAttacks.FireWeapon = StoneAttacks.StoneLevitation;
            StoneAttacks.levitation = true;
            SetPS("Levitation");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && stoneAvailable > 2)
        {
            StoneAttacks.FireWeapon = StoneAttacks.StoneAttack;
            StoneAttacks.levitation = false;
            ChangeStone("Ice");
        }
    }

    /// <summary>
    /// Changes stone
    /// </summary>
    /// <param name="name">The name of the stone to be changed</param>
    public void ChangeStone(string name)
    {
        SetType(name);
        SetParent(name);
        SetPS(name);
    }

    /// <summary>
    /// Sets the type of the stone
    /// </summary>
    /// <param name="name">The name of the stone</param>
    public void SetType(string name)
    {
        for (int i = 0; i < prefabtype.Length; i++)
        {
            if (prefabtype[i].name.Contains(name))
            {
                StoneAttacks.shot = prefabtype[i];
                return;
            }
        }
    }

    /// <summary>
    /// Sets the stone parent
    /// </summary>
    /// <param name="name">The name of the parent</param>
    public void SetParent(string name)
    {
        for (int i = 0; i < parentForType.Length; i++)
        {
            if (parentForType[i].name.Contains(name))
            {
                StoneAttacks.shotsParent = parentForType[i];
                return;
            }
        }
    }

    /// <summary>
    /// Sets the player shot
    /// </summary>
    /// <param name="name">The name of the player shot</param>
    public void SetPS(string name)
    {
        for (int i = 0; i < particleSystemType.Length; i++)
        {
            if (particleSystemType[i].name.Contains(name))
            {
                particleSystemType[i].SetActive(true);
            }
            else
            {
                particleSystemType[i].SetActive(false);
            }
        }
    }
}
