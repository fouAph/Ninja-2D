using UnityEngine;

public class Ladder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerController))
        {
            if (playerController)
            {
                playerController.CanClimb = true;
                playerController.CurrentLadder = this;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerController))
        {
            if (playerController)
            {
                playerController.CanClimb = false;
                playerController.CurrentLadder = null;
            }
        }
    }
}
