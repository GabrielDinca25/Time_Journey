using UnityEngine;

public class RevivePlayer : MonoBehaviour
{
    /// <summary>
    /// Calls LoadPosition()
    /// </summary>
    public void Revive()
    {
        LoadPosition();
    }

    /// <summary>
    /// Loads player position to the one saved
    /// </summary>
    public void LoadPosition()
    {
        GameController.instance.player.transform.position = new Vector3(GameController.instance.saveSystemSO.m_PlayerPositionX,
                                                            GameController.instance.saveSystemSO.m_PlayerPositionY,
                                                            0);
        GameController.instance.player.SetActive(true);
    }


}
