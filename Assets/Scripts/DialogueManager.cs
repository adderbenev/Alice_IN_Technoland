using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueDatabase dialogueDatabase;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text[] answerTexts;
    [SerializeField] private Button[] answerButtons;

    [SerializeField] private GameObject questionCanvas;
    private CanvasGroup questionCanvasGroup;
    private bool fadeIn = false;
    private bool fadeOut = false;
    [SerializeField] private float fadeInTime = 3f;
    [SerializeField] private float fadeOutTime = 3f;

    private int currentQuestionIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayQuestion(currentQuestionIndex);
        questionCanvasGroup = questionCanvas.GetComponent<CanvasGroup>();
    }

    private void DisplayQuestion(int index)
    {
        if (index < 0 || index >= dialogueDatabase.questions.Count)
        {
            return;
        }

        DialogueQuestion question = dialogueDatabase.questions[index];
        questionText.text = question.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Remove old listeners
            answerButtons[i].onClick.RemoveAllListeners();

            if (i < question.answers.Count)
            {
                answerTexts[i].text = question.answers[i].answerText;
                answerButtons[i].gameObject.SetActive(true);
                int answerIndex = i;
                answerButtons[i].onClick.AddListener(() => SelectAnswer(answerIndex));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }

            if(question.answers.Count == 0)
            {
                StartCoroutine(FadeOutUI());
            }
        }
    }

    private void SelectAnswer(int answerIndex)
    {
        DialogueQuestion question = dialogueDatabase.questions[currentQuestionIndex];
        int nextIndex = question.answers[answerIndex].nextQuestionIndex;
        currentQuestionIndex = nextIndex;
        DisplayQuestion(currentQuestionIndex);
    }

    public IEnumerator FadeInUI()
    {
        Debug.Log("fade in UI");
        yield return new WaitForSeconds(fadeInTime);
        fadeIn = true;
    }

    private IEnumerator FadeOutUI()
    {
        yield return new WaitForSeconds(fadeOutTime);
        fadeOut = true;
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (questionCanvasGroup.alpha < 1)
            {
                questionCanvasGroup.alpha += Time.deltaTime;
                if (questionCanvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (questionCanvasGroup.alpha >= 0)
            {
                questionCanvasGroup.alpha -= Time.deltaTime;
                if (questionCanvasGroup.alpha == 0)
                {
                    fadeOut = false;
                    questionCanvas.SetActive(false);
                }
            }
        }
    }
}
