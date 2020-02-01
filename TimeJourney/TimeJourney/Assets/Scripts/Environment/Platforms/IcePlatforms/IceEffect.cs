using UnityEngine;

public class IceEffect : MonoBehaviour
{
    // The force of the ice effect
    public float force;

    // The area effector of the ice effect
    private AreaEffector2D ae2D;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        ae2D = GetComponent<AreaEffector2D>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        GameController.instance.player.GetComponent<CharacterController2D>().m_JumpForce = 350;
    }

    /// <summary>
    /// The function called when the behaviour becomes disabled.
    /// </summary>
    private void OnDisable()
    {
        if (GameController.instance.player != null)
        {
            GameController.instance.player.GetComponent<CharacterController2D>().m_JumpForce = 300;
        }
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    private void Update()
    {
        bool right = GameController.instance.player.GetComponent<CharacterController2D>().m_FacingRight;

        if (right)
        {
            ae2D.forceMagnitude = force;
        }
        else
        {
            ae2D.forceMagnitude = -force;
        }

    }
}
