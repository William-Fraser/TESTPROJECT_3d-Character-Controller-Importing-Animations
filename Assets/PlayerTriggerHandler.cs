using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTriggerHandler : MonoBehaviour
{
    [Header("Respawn")]
    public GameObject RespawnFog;

    //model
    private Quaternion m_Rotation;

    //fields for respawn
    private Vector3 m_playerSpawn;
    private Image fog;
    private bool respawning;
    private bool respawnOnce;


    void Start()
    {
        //save players origin as spawn
        m_playerSpawn = this.transform.position;
        m_Rotation = this.transform.rotation;

        //initialization for respawn
        RespawnFog.SetActive(true);
        fog = RespawnFog.GetComponent<Image>();
        respawning = true;
        respawnOnce = false;
    }

    void Update()
    {
        if (respawning && respawnOnce == false)
        {
            respawnOnce = true;
            fog.CrossFadeAlpha(1, .5f, false);
            Debug.Log("Run this Once");
            StartCoroutine("Respawn");
        }
    }

    // Triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killbox")
        {
            respawning = true;
        }
    }
    
    //handles respawn animations
    IEnumerator Respawn()
    {
        Debug.Log("Starting respawntimer");
        yield return new WaitForSeconds(1);
        Debug.Log("Respawning");

        //reposition
        gameObject.transform.position = m_playerSpawn;
        gameObject.transform.rotation = m_Rotation;

        //remove respawn fog
        fog.CrossFadeAlpha(0, .7f, false);
        respawning = false;
        respawnOnce = false; // set once to false to respawn once again
    }
}
