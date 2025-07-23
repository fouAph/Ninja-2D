using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackDetector : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] int customDamage = 20;
    private Vector3 originalPos;
    void Awake()
    {
        originalPos = transform.localPosition;
    }
    void OnEnable()
    {
        transform.localPosition = originalPos;
    }

    void OnDisable()
    {
        transform.localPosition = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            if (playerController)
                damageable?.TakeDamage(playerController.attackDamage);
            else
                damageable?.TakeDamage(customDamage);
        }
    }
}
