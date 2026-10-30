using UnityEngine;

public class TeleportEvent : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator anim;
    [SerializeField] private string arrivalTrigger = "PlayerDetected";

    [Header("Player")]
    [SerializeField] private string playerTag = "Player";

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered)
            return;

        if (other.CompareTag(playerTag))
        {
            Debug.Log("player detected");
            hasTriggered = true;

            if (anim != null)
            {
                anim.SetTrigger(arrivalTrigger);
            }
        }
    }
}
