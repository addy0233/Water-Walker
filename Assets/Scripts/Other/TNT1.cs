using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT1 : MonoBehaviour
{
    public float damage = 20f;
    public float hitRadius = 1f;

    public GameObject GFX;

    public bool destroyed = false;

    public GameObject DestroyParticle;

    Transform target;

    public LayerMask playerLayer;

    void Start()
    {
        target = PlayerManager.instance.player.transform;

        destroyed = false;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);

        if (distanceToPlayer <= hitRadius && destroyed == false) //Melee Attack
        {
            Collider[] PlayerToDamage = Physics.OverlapSphere(transform.position, hitRadius, playerLayer);
            for (int i = 0; i < PlayerToDamage.Length; i++)
            {
                PlayerToDamage[i].GetComponent<PlayerHealth1>().TakeDamage(damage); //Damaging Player

                GameObject DeathParticle1 = Instantiate(DestroyParticle, transform.position, transform.rotation) as GameObject;

                Destroy(DeathParticle1, 6f);

                destroyed = true;
            }
        }

        if (destroyed == true)
        {
            GFX.SetActive(false);

            Destroy(gameObject, 7f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }

}
