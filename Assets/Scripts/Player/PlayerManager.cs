using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion 

    public GameObject[] players;
    public GameObject player;

    public GameObject playerCamera;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player Prefab");
    }
}
