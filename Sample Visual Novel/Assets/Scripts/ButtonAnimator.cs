// Controls appearing/disappearing animations for all decision buttons

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimator : MonoBehaviour
{
    const float LERPING_THRESHOLD = 1f; // Once height is within this distance from destination, snaps to destination height
    const float GROWING_SMOOTHNESS = 7.5f; // Larger is slower
    const float SHRINKING_SMOOTHNESS = 20f; // Larger is slower
    const float FADE_TIME = 0.2f;

    public GameObject[] buttons;

    // Animation
    bool[] buttonsVisible; // True if visible, false if invisible. Determines direction of animation in ButtonAnim
    RectTransform[] buttonRects;
    Image[] buttonBoxes;
    TextMeshProUGUI[] buttonTexts;
    float startHeight;

    private void Start()
    {
        startHeight = buttons[0].GetComponent<RectTransform>().rect.height;
        buttonRects = new RectTransform[buttons.Length];
        buttonBoxes = new Image[buttons.Length];
        buttonTexts = new TextMeshProUGUI[buttons.Length];
        for (int x = 0; x < buttons.Length; x++)
        {
            buttonRects[x] = buttons[x].GetComponent<RectTransform>();
            buttonBoxes[x] = buttons[x].GetComponent<Image>();
            buttonTexts[x] = buttons[x].GetComponentInChildren<TextMeshProUGUI>();
            RectTransformAssistant.SetHeight(buttonRects[x], 0);
            buttons[x].SetActive(false);
        }
        buttonsVisible = new bool[buttons.Length];
    }

    /* Sets buttons active
    * @param numberOfChoices number of choices
    */
    public void Appear(int numberOfChoices)
    {
        for (int buttonNum = 0; buttonNum < buttonRects.Length; buttonNum++)
        {
            buttonsVisible[buttonNum] = buttonNum < numberOfChoices;
            StartCoroutine(ButtonAnim(buttonRects[buttonNum], buttonNum));
        }
    }

    // Sets all buttons inactive.
    public void Disappear()
    {
        buttonsVisible = new bool[buttons.Length];
        for (int buttonNum = 0; buttonNum < buttonRects.Length; buttonNum++)
            StartCoroutine(ButtonAnim(buttonRects[buttonNum], buttonNum));
    }

    /* Animates buttons either appearing or disappearing.
     * @param buttonRect Rect to animate. Shrink/expand height.
     * @param buttonIndex Index of animating button.
     */
    IEnumerator ButtonAnim(RectTransform buttonRect, int buttonIndex)
    {
        if (buttonsVisible[buttonIndex])
        {
            buttons[buttonIndex].SetActive(true);
            buttons[buttonIndex].GetComponent<Button>().interactable = false;
            buttonBoxes[buttonIndex].CrossFadeAlpha(1, FADE_TIME, false);
            buttonTexts[buttonIndex].CrossFadeAlpha(1, FADE_TIME, false);
            while (buttonRect.rect.height < startHeight - LERPING_THRESHOLD)
            {
                RectTransformAssistant.SetHeight(
                    buttonRect,
                    Mathf.Lerp(RectTransformAssistant.GetHeight(buttonRect), startHeight, 1 / GROWING_SMOOTHNESS));
                yield return new WaitForFixedUpdate();
            }
            RectTransformAssistant.SetHeight(buttonRect, startHeight);
            buttons[buttonIndex].GetComponent<Button>().interactable = true;
            buttonRect.rect.Set(buttonRect.rect.x, buttonRect.rect.y, buttonRect.rect.width, startHeight);
        }
        else
        {
            buttons[buttonIndex].GetComponent<Button>().interactable = false;
            buttonBoxes[buttonIndex].CrossFadeAlpha(0, FADE_TIME, false);
            buttonTexts[buttonIndex].CrossFadeAlpha(0, FADE_TIME, false);
            float timeToStopShrink = Time.time + FADE_TIME;
            while (timeToStopShrink > Time.time)
            {
                RectTransformAssistant.SetHeight(
                    buttonRect,
                    Mathf.Lerp(RectTransformAssistant.GetHeight(buttonRect), 0, 1 / SHRINKING_SMOOTHNESS));
                yield return new WaitForFixedUpdate();
            }
            buttonRect.rect.Set(buttonRect.rect.x, buttonRect.rect.y, buttonRect.rect.width, 0);
            buttons[buttonIndex].SetActive(false);
        }
    }
}
