using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float normalSpeed;

    public float rotationMultiplier = 50f;
    public float ForceMultiplier = 50f;

    public Rigidbody rb;

    public Transform BoatGFX;

    float horizontal;

    public Transform left;
    public Transform right;
    public Transform StartRot;
    //public float lerpSpeed = 0.5f;
    public float timeCount = 0f;

    public float gold = 0f;

    public float rotTime = 0.5f;
    public float lTime;
    public float rTime;

    public GameObject Particles;

    public ParticleSystem particle1;
    public ParticleSystem particle2;

    public Animator anim;

    public TextMeshProUGUI CoinText1;

    void Start()
    {
        Particles.SetActive(false);

        lTime = rotTime;
        rTime = rotTime;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        CoinText1.text = gold.ToString();

        timeCount = timeCount * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            //Vector3 v3Force = speed * transform.forward;
            //rb.velocity = v3Force;

            rb.AddForce(transform.forward * speed * ForceMultiplier);

            particle1.Play();
            particle2.gameObject.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vector3 v3Force = speed * transform.forward;
            rb.velocity = v3Force * -1f;

            particle1.Play();
            particle2.gameObject.SetActive(true);
        }
        else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            particle1.Stop();
            particle2.gameObject.SetActive(false);
        }

        horizontal = Input.GetAxis("Horizontal");

        horizontal *= Time.deltaTime * rotationMultiplier;

        transform.Rotate(0, horizontal, 0);

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) // left
        {
            lTime -= Time.deltaTime * 1f;

            if (lTime >= 0)
            {
                BoatGFX.Rotate(Time.deltaTime * 0, 0, 5);
            }
            else
            {
                BoatGFX.rotation = left.rotation;
            }

            anim.SetTrigger("isLeft");

            rTime = rotTime;
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) // right
        {
            rTime -= Time.deltaTime * 1f;

            if (rTime >= 0)
            {
                BoatGFX.Rotate(Time.deltaTime * 0, 0, -5);
            }
            else
            {
                BoatGFX.rotation = right.rotation;
            }

            anim.SetTrigger("isRight");

            lTime = rotTime;
        }
        else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            BoatGFX.transform.rotation = Quaternion.Lerp(BoatGFX.rotation, StartRot.rotation, Time.deltaTime * 5f);

            anim.SetTrigger("isStock");
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            speed = normalSpeed;

            Particles.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.V))
        {
            speed = 20f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("Idle1", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("Idle1", false);
        }
    }
}
