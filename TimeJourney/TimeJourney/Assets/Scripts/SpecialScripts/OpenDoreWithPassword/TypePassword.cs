using TMPro;
using UnityEngine;

public class TypePassword : MonoBehaviour
{
    // The current selected letter
    private int m_CurrentLetter;

    // The current sentence
    private char[] m_CurrentSentence;

    // The current sentence TextMeshPro text
    public TMP_Text[] m_CurrentSentenceText;

    // The correct password
    public string password;

    /// <summary>
    /// The method called when the script on the object is enabled (before any update frame)
    /// </summary>
    private void Start()
    {
        m_CurrentSentence = new char[m_CurrentSentenceText.Length];
        ResetText();
    }

    /// <summary>
    /// Sets the password text
    /// </summary>
    private void SetText()
    {
        for(int i = 0; i < m_CurrentSentenceText.Length; i++)
        {
            m_CurrentSentenceText[i].text = m_CurrentSentence[i].ToString();
        }
    }

    /// <summary>
    /// Resets the current sentence
    /// </summary>
    private void ResetCurrentSentence()
    {
        for (int i = 0; i < m_CurrentSentence.Length; i++)
        {
            m_CurrentSentence[i] = (char) 0;
        }
    }

    /// <summary>
    /// Resets the text
    /// </summary>
    public void ResetText()
    {
        m_CurrentLetter = 0;
        ResetCurrentSentence();
        SetText();
    }

    /// <summary>
    /// Sets the next character
    /// </summary>
    /// <param name="letter">The letter to be set</param>
    public void SetNextCharacter(string letter)
    {  
        m_CurrentSentence[m_CurrentLetter] = letter.ToCharArray()[0];
        SetText();

        if (m_CurrentLetter < m_CurrentSentence.Length - 1)
        {
            m_CurrentLetter++;
        }
        else
        {
            CheckValidation();
        }
    }
    /// <summary>
    /// Check if password is corect, if soo open the door else if not reset text
    /// </summary>
    public void CheckValidation()
    {
        if (CheckPassword())
        {
            // Go to next level if password is correct
            GetComponent<ActivateByTrigger>().Disable();
            Destroy(GetComponent<ActivateByTrigger>());
            GetComponent<TypePassword>().enabled = false;
            GetComponent<GoToNextLevel>().ChangeScene();
            //Door.GetComponent<ActivateDoor>().enabled = true;
        }
        else
        {
            // Reset the text if the password is wrong
            ResetText();
        }
    }

    /// <summary>
    /// check if password is same with curent Sentence
    /// </summary>
    /// <returns></returns>
    public bool CheckPassword()
    {
        // Check if entered sentence is the same as the correct passowrd
        for(int i = 0; i < password.Length; i++)
        {
            if(!password[i].Equals(m_CurrentSentence[i]))
            {
                return false;
            }
        }
        return true;
    }
}
