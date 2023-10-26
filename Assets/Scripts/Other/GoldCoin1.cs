using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin1 : MonoBehaviour
{
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
            PlayerController playerController1 = target.gameObject.GetComponent<PlayerController>();

            playerController1.gold += 1f;

            GameObject DeathParticle1 = Instantiate(DestroyParticle, transform.position, transform.rotation) as GameObject;

            Destroy(DeathParticle1, 6f);

            GFX.SetActive(false);

            destroyed = true;
        }

        if (destroyed == true)
        {
            GFX.SetActive(false);

            Destroy(gameObject, 2f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }
}
