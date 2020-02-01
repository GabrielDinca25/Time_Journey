using UnityEngine;

public class MultiSwitchManager : MonoBehaviour
{

    public GameObject objectToActivate;
    public GameObject[] switches;
    public Sprite switchOff;
    private bool[] switchesStates;

    public float timeToBeAlive = 5;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        switchesStates = new bool[switches.Length];
    }

    /// <summary>
    /// Updates the state of the switch to given state
    /// </summary>
    /// <param name="name"></param>
    public void UpdateStateOfSwitch(string name)
    {
        for (int i = 0; i < switches.Length; i++)
        {
            if (switches[i].name.Equals(name))
            {
                SetSwitchState(i);
            }
        }
    }

    /// <summary>
    /// Updates the state of the switch by given order
    /// </summary>
    /// <param name="number">The order of the swithc</param>
    public void SetSwitchState(int number)
    {
        switchesStates[number] = !switchesStates[number];

        if (CheckSwitches())
        {
            ActivateObject();
        }
    }

    /// <summary>
    /// Checks if all switches are enabled
    /// </summary>
    /// <returns>True if all switches are enabled, false otherwise</returns>
    public bool CheckSwitches()
    {
        for (int i = 0; i < switchesStates.Length; i++)
        {
            if (!switchesStates[i])
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Activates gameObject
    /// </summary>
    public void ActivateObject()
    {
        objectToActivate.SetActive(true);
        Invoke("TimesUP", timeToBeAlive);
    }

    /// <summary>
    /// Resets the switch states
    /// </summary>
    public void Reset()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            switches[i].GetComponent<SpriteRenderer>().sprite = switchOff;
        }

        for (int i = 0; i < switchesStates.Length; i++)
        {
            switchesStates[i] = false;
        }
    }

    /// <summary>
    /// Resets switches after time is up
    /// </summary>
    public void TimesUP()
    {
        Reset();
        objectToActivate.SetActive(false);
    }


}
