using System.Collections;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] float kunaiLifeTime = 10f;
    private int kunaiDamage;
    private float kunaiTimer, kunaiMoveSpeed;
    private Transform owner;
    public void ThrowKunai(float xDirection, float _kunaiMoveSpeed, int _kunaiDamage, Transform _owner)
    {
        kunaiDamage = _kunaiDamage;
        kunaiMoveSpeed = _kunaiMoveSpeed;
        owner = _owner;
        StartCoroutine(ThrowKunaiCor(xDirection));
    }

    private IEnumerator ThrowKunaiCor(float xDirection)
    {
        while (kunaiTimer < kunaiLifeTime)
        {
            transform.position += new Vector3(xDirection, 0) * Time.deltaTime * kunaiMoveSpeed;
            kunaiTimer += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (owner == collision.transform) return;
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable?.TakeDamage(kunaiDamage);
        }
    }
}