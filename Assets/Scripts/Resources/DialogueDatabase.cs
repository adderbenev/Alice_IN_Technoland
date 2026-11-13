using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "Scriptable Objects/DialogueDatabase")]
public class DialogueDatabase : ScriptableObject
{
    public List<DialogueQuestion> questions = new List<DialogueQuestion>();
}

[System.Serializable]
public class DialogueQuestion
{
    [TextArea(2, 5)]
    public string questionText;

    public List<DialogueAnswer> answers = new List<DialogueAnswer>(4);
}

[System.Serializable]
public class DialogueAnswer
{
    public string answerText;
    public int nextQuestionIndex;
}