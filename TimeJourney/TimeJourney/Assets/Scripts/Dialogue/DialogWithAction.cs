using System;
using System.Collections;
using UnityEngine;

public class DialogWithAction : MonoBehaviour
{
    // The main dialog sentence
    public string[] dialogSentence;

    // The extra dialog sentences
    public string[] nextDialogSentences;

    // The index of the dialog sentence
    public int index;

    // The typing speed of the dialog animation
    public float typingSpeed = 0.015f;

    // The type of the dialog
    private IEnumerator type;

    // The count of finished dialogs
    public int endedDialogCount;

    // Bool indicating wether the next line should be displayed
    private bool nextLine;

    // Bool indicating that the dialog ended
    public bool dialogEnded;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        DialogsController.instance.textBackGround.SetActive(true);
        DialogsController.instance.textDisplay.gameObject.SetActive(true);
        type = Type();
        StartCoroutine(type);
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    private void Update()
    {
        if (DialogsController.instance.textDisplay.text == dialogSentence[index])
        {
            if ((Input.GetKeyDown(KeyCode.Space) && nextLine) || (Input.GetKeyDown(KeyCode.E) && nextLine))//Input.anyKeyDown
            {
                nextLine = false;
                NextSentence();
            }
        }
    }

    public IEnumerator Type()
    {
        foreach (char letter in dialogSentence[index].ToCharArray())
        {
            DialogsController.instance.textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        nextLine = true;
    }

    public void NextSentence()
    {
        if (index < dialogSentence.Length - 1)
        {
            index++;
            DialogsController.instance.textDisplay.text = "";
            type = Type();
            StartCoroutine(type);
        }
        else
        {
            NextDialogSentences();
        }
    }

    public void NextDialogSentences()
    {
        if (nextDialogSentences.Length > 0)
        {
            Array.Resize(ref dialogSentence, nextDialogSentences.Length);
            nextDialogSentences.CopyTo(dialogSentence, 0);
        }
        dialogEnded = true;
        EndDialog();
    }

    public void EndDialog()
    {
        index = 0;
        StopCoroutine(type);
        DialogsController.instance.textDisplay.text = "";
        DialogsController.instance.textDisplay.gameObject.SetActive(false);
        DialogsController.instance.textBackGround.SetActive(false);
        endedDialogCount += 1;
        if (endedDialogCount >= 2)
        {
            GameController.instance.DoSpecialAction();
            Destroy(gameObject);
            return;
        }
        enabled = false;
    }

    public void CheckDialogStatus()
    {
        if (!dialogEnded)
        {
            NextDialogSentences();
        }
    }
}