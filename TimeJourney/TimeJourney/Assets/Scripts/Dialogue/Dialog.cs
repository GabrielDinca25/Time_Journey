using System;
using System.Collections;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    // The main dialog sentence
    public string[] dialogSentence;

    // The extra dialog sentence
    public string[] moreDialogSentences;

    // The index of the dialog sentence
    public int index;

    // The typing speed of the dialog animation
    public float typingSpeed = 0.015f;

    // The type of the dialog
    private IEnumerator type;

    // Bool indicating wether the dialog is destroyed
    public bool destroyDialog;

    // Bool indicating if extra dialog should be shown
    private bool moreDialog;

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
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))//Input.anyKeyDown
            {
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
            MoreDialogSentences();
        }
    }

    public void MoreDialogSentences()
    {
        if (moreDialogSentences.Length > 0)
        {
            Array.Resize(ref dialogSentence, moreDialogSentences.Length);
            moreDialogSentences.CopyTo(dialogSentence, 0);
        }
        EndDialog();
    }

    public void EndDialog()
    {
        index = 0;
        StopCoroutine(type);
        DialogsController.instance.textDisplay.text = "";
        DialogsController.instance.textDisplay.gameObject.SetActive(false);
        DialogsController.instance.textBackGround.SetActive(false);
        if (destroyDialog)
        {
            Destroy(gameObject);
        }
        GetComponent<Dialog>().enabled = false;
    }

    public void CheckDialogStatus()
    {
        EndDialog();
    }
}