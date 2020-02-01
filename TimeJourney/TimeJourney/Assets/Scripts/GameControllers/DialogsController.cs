using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogsController : MonoBehaviour
{
    [HideInInspector] public static DialogsController instance;
    public TextMeshProUGUI textDisplay;
    public GameObject textBackGround;

    /// <summary>
    /// The method called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

}
