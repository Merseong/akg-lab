using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerObject : NetworkBehaviour
{
    [SerializeField] private GameObject stoneObject;
    [SerializeField] private StoneBehaviour playerStone;
    public float forceMult = 1f;
    NetworkVariable<float> stoneForceNetworkVar = new NetworkVariable<float>(1000f);

    // public void Start()
    // {
    //     if(!IsLocalPlayer) return;
        
    //     Time.timeScale = Time.timeScale * .1f;
    // }

    public void Update()
    {
        if(!IsLocalPlayer) return;

        if(Input.GetKeyDown(KeyCode.Q))
        {
            PlayerInputServerRpc(KeyCode.Q);
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            PlayerInputServerRpc(KeyCode.W);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            PlayerInputServerRpc(KeyCode.A);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            PlayerInputServerRpc(KeyCode.S);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            PlayerInputServerRpc(KeyCode.D);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            PlayerInputServerRpc(KeyCode.E);
        }
    }

    [ServerRpc]
    public void SpawnObjectServerRpc()
    {
        if(!IsServer) return;

        GameObject go = Instantiate(stoneObject, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f),1f), Quaternion.identity);
        
        go.GetComponent<NetworkObject>().Spawn();

        playerStone = go.GetComponent<StoneBehaviour>();
    }

    [ServerRpc]
    public void PlayerInputServerRpc(KeyCode key)
    {
        if(key == KeyCode.Q)
        {
            if(playerStone == null)
                SpawnObjectServerRpc();
        }

        if(key == KeyCode.W)
        {
            playerStone.AddForceToStoneClientRpc(Vector2.up, stoneForceNetworkVar.Value * forceMult);
        }
        else if(key == KeyCode.A)
        {
            playerStone.AddForceToStoneClientRpc(Vector2.right, stoneForceNetworkVar.Value * forceMult);
        }
        else if(key == KeyCode.S)
        {
            playerStone.AddForceToStoneClientRpc(Vector2.down, stoneForceNetworkVar.Value * forceMult);
        }
        else if(key == KeyCode.D)
        {
            playerStone.AddForceToStoneClientRpc(Vector2.left, stoneForceNetworkVar.Value * forceMult);
        }

        if(key == KeyCode.E)
        {
            playerStone.StopStoneClientRpc();
        }

    }
}
