using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip[] footstepsOnGrass;
    public AudioClip[] footstepsOnWood;

    public string material;

    private int walkTiming = 60;

    private int timer = 0;
    private int jumpTimer = 0;
    private bool jumping = false;


    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
        }

        if (jumping)
        {
            if (jumpTimer > 60)
            {
                jumping = false;
                jumpTimer = 0;
            }
            jumpTimer++;
        }
        else if (v > 0.1 || v < -0.1)
        {
            if (timer > walkTiming)
            {
                AudioSource myAudioSource = GetComponent<AudioSource>();
                myAudioSource.volume = Random.Range(0.9f, 1.0f);
                myAudioSource.pitch = Random.Range(0.8f, 1.2f);

                switch (material)
                {
                    case "Grass":
                        myAudioSource.PlayOneShot(footstepsOnGrass[Random.Range(0, footstepsOnGrass.Length)]);
                        break;

                    case "Wood":
                        myAudioSource.PlayOneShot(footstepsOnWood[Random.Range(0, footstepsOnWood.Length)]);
                        break;

                    default:
                        break;
                }
                timer = 0;
            }
            timer++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Grass":
            case "Wood":
                material = collision.gameObject.tag;
                break;

            default:
                break;
        }
    }
}
