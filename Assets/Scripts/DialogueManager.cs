using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueDatabase dialogueDatabase;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text[] answerTexts;
    [SerializeField] private Button[] answerButtons;

    private int currentQuestionIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayQuestion(currentQuestionIndex);
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
        }
    }

    private void SelectAnswer(int answerIndex)
    {
        DialogueQuestion question = dialogueDatabase.questions[currentQuestionIndex];
        int nextIndex = question.answers[answerIndex].nextQuestionIndex;
        currentQuestionIndex = nextIndex;
        DisplayQuestion(currentQuestionIndex);
    }
}
