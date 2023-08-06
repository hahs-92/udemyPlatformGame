using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private GameObject playerPrefab;
    public Transform respawnPoint;
    public GameObject currentPlayer;
    public int choosenSkinId;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerRespawn();
        }
    }

    public void PlayerRespawn()
    {
        if (currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
        }
    }

}
