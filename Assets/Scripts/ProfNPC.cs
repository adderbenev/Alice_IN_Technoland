using UnityEngine;

public class ProfNPC : MonoBehaviour
{
    [SerializeField] private GameObject questionCanvas;
    [SerializeField] private DialogueManager dialogueManager;

    public void ProfArrivesAtPlayer()
    {
        questionCanvas.SetActive(true);
        StartCoroutine(dialogueManager.FadeInUI());
    }
}
