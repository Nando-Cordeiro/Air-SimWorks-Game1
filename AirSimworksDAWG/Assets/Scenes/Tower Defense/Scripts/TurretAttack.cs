using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    GameObject targetEnemy;
    bool attackReady = true;
    [SerializeField] float attackDamage;
    [SerializeField] float fireCooldown;
    float resetTimer;
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        if (targetEnemy != null)
        {
            return;
        }

        if (other.tag == "Target")
        {
            targetEnemy = other.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("yo");

        if(targetEnemy != null)
        {
            return;
        }

        if(other.tag == "Target")
        {
            targetEnemy = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == targetEnemy)
        {
            targetEnemy = null;
        }
    }

    private void Update()
    {
        if (attackReady && targetEnemy != null)
        {
            Fire();
        }
    }

    void Fire()
    {
        attackReady = false;
        targetEnemy.gameObject.GetComponent<EnemyHealth>().Damage(attackDamage);
        StartCoroutine(ResetWeapon());
    }

    IEnumerator ResetWeapon()
    {
        resetTimer = fireCooldown;

        while(resetTimer > 0)
        {
            resetTimer -= Time.deltaTime;
            yield return null;
        }

        attackReady = true;
    }
}
