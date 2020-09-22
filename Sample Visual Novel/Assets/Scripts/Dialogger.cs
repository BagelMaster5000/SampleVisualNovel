/* Script that accepts ink file and displays text.
 * Handles most of the logic of the game.
 * A lot of inspiration was drawn from YouTube tutorials by Dan Cox.
*/

using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class Dialogger : MonoBehaviour
{
    const float TEXT_DISPLAY_SPEED = 0.03f;
    const int TEXT_SCALE_START_DELTA = -40;
    const int TALKING_SOUND_DELAY = 3;

    [Header("Story")]
    public TextAsset inkFile;
    static Story story;

    [Header("UI")]
    GameObject[] buttons; // All buttons
    public TextMeshProUGUI message; // Dialog displaying text
    public ButtonAnimator buttonAnimator; // Controls animations for all buttons
    public Image proceedGraphic; // Bobbing arrow to show dialogue is done

    // Related to text appearing
    bool canContinue = true;
    float startTextSize;
    bool madeDecision = true;
    bool fastForwarding;
    Coroutine fastForwardCoroutine;

    [Header("Sounds")]
    public AudioSource talkingSound;
    public AudioSource selectionSound;

    InputMaster inputs;

    [Header("Character")]
    public Animator characterAnimator;

    #region Inputs and Buttons Set-Up

    // Initialize variables
    private void Awake()
    {
        inputs = new InputMaster();
        inputs.InGame.AdvanceDialog.performed += ctx => ContinueStory();
        inputs.InGame.FastForward.started += ctx => fastForwardCoroutine = StartCoroutine(FastForwardingHold());
        inputs.InGame.FastForward.canceled += ctx => StopCoroutine(fastForwardCoroutine);
        inputs.InGame.FastForward.canceled += ctx => fastForwarding = false;
        inputs.Enable();

        buttons = GetComponentInChildren<ButtonAnimator>().buttons;
    }

    // Disables inputs when object is set inactive
    private void OnDisable() { inputs.Disable(); }

    #endregion

    // Set-up
    void Start()
    {
        startTextSize = message.fontSize;
        story = new Story(inkFile.text);
        proceedGraphic.enabled = false;
        ContinueStory();
    }

    // Advances dialog if able; otherwise, ends story. Called with press of primary button
    public void ContinueStory()
    {
        if (story.canContinue && madeDecision)
        {
            if (canContinue)
                AdvanceDialog();
            else
                fastForwarding = true;
        }
        else if (madeDecision && canContinue)
            FinishDialog();
    }

    // Advances dialog
    void AdvanceDialog()
    {
        string curSentence = story.Continue();
        ParseTags();
        StartCoroutine(ShowDialog(curSentence));
    }

    // Checks tags for commands (currently used only for the character animation)
    void ParseTags()
    {
        // Doesn't parse tags if no animator attached
        if (characterAnimator == null | story.currentTags.Count == 0)
            return;

        // Checks each tag
        foreach (string t in story.currentTags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(' ')[1];

            switch (prefix.ToLower())
            {
                case "anim":
                    SetAnimation(param);
                    break;
            }
        }
    }

    // Sets animation of the character. Called by ParseTags()
    void SetAnimation(string param)
    {
        switch (param)
        {
            case "idle":
                characterAnimator.SetTrigger("Idle");
                break;
            case "questioning":
                characterAnimator.SetTrigger("Questioning");
                break;
            case "happy":
                characterAnimator.SetTrigger("Happy");
                break;
            case "invisible":
                characterAnimator.SetTrigger("Invisible");
                break;
        }
    }

    // Shows buttons for player to make a choice
    void ShowChoices()
    {
        List<Choice> choices = story.currentChoices;

        // Throws error if more choices than available buttons
        if (choices.Count > buttons.Length)
        {
            Debug.LogError("More choices than buttons");
            return;
        }

        madeDecision = false;

        // Sets buttons
        for (int x = 0; x < choices.Count; x++)
        {
            buttons[x].transform.GetComponentInChildren<TextMeshProUGUI>().text = choices[x].text;
            buttons[x].GetComponent<Selectable>().element = choices[x];
        }
        buttonAnimator.Appear(choices.Count);
    }

    // Ends dialog and loads next scene
    void FinishDialog()
    {  
        // FIXME Put an end behavior such as loading next scene
    }

    /* Selects a decision. Called by choice button's script
     * @param element Choice selected
     */
    public void SetDecision(Choice element)
    {
        if (element == null)
            return;
        selectionSound.Play();
        story.ChooseChoiceIndex(element.index);
        AdvanceFromDecision();
    }

    // Advances dialog following decision
    void AdvanceFromDecision()
    {
        madeDecision = true;
        buttonAnimator.Disappear();
        if (story.canContinue)
            AdvanceDialog();
    }

    /* Shows dialog character-by-character. Also does a fancy letter-scaling effect.
     * @param sentence segment of dialog to display
     */
    IEnumerator ShowDialog(string sentence)
    {
        canContinue = false;
        List<float> letterSizes = new List<float>();

        talkingSound.Play();
        int soundCounter = 0;

        proceedGraphic.enabled = false;

        // Printing text
        for (int index = 0; index < sentence.Length; index++)
        {
            letterSizes.Add(startTextSize + TEXT_SCALE_START_DELTA);
            message.text = "";
            for (int letNum = 0; letNum < letterSizes.Count; letNum++)
            {
                message.text += "<size=" + letterSizes[letNum] + ">" + sentence.Substring(letNum, 1) + "</size>";
                letterSizes[letNum] = Mathf.SmoothStep(letterSizes[letNum], startTextSize, 0.5f);
            }
            soundCounter++;
            if (soundCounter > TALKING_SOUND_DELAY)
            {
                soundCounter = 0;
                talkingSound.Play();
            }
            if (fastForwarding)
                yield return new WaitForSecondsRealtime(TEXT_DISPLAY_SPEED / 2);
            else
                yield return new WaitForSecondsRealtime(TEXT_DISPLAY_SPEED);
        }

        // Shrinking text after all text has been displayed
        while (letterSizes[sentence.Length - 1] < startTextSize - 1)
        {
            message.text = "";
            for (int letNum = 0; letNum < letterSizes.Count; letNum++)
            {
                message.text += "<size=" + letterSizes[letNum] + ">" + sentence.Substring(letNum, 1) + "</size>";
                letterSizes[letNum] = Mathf.SmoothStep(letterSizes[letNum], startTextSize, 0.5f);
            }
            if (fastForwarding)
                yield return new WaitForSecondsRealtime(TEXT_DISPLAY_SPEED / 2);
            else
                yield return new WaitForSecondsRealtime(TEXT_DISPLAY_SPEED);
        }
        message.text = "<size=" + startTextSize + ">" + sentence + "</size>";

        if (story.currentChoices.Count > 0)
            ShowChoices();
        else
            proceedGraphic.enabled = true;
        fastForwarding = false;
        canContinue = true;

    }

    // Keeps fastForward set to true while speed up button pressed
    IEnumerator FastForwardingHold()
    {
        while (true)
        {
            fastForwarding = true;
            yield return null;
        }
    }
}
