using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offseet : MonoBehaviour
{
    // The speed that the bacgkround moves with
    public float bgSpeed;

    // The renderer of the background
    public Renderer bgRend;

    // The Player object
    public PlayerMovementWithSword pmws;

    /// <summary>
    /// Method called every fixed frame-rate frame
    /// </summary>
    void FixedUpdate()
    {
        // Sets the mainTextureOffset of the background to the player's horizontal moving
        bgRend.material.mainTextureOffset += new Vector2(pmws.horizontalMove * Time.fixedDeltaTime * 0.001f, 0f);
    }
}
