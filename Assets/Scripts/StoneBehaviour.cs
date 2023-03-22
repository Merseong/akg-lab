using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class StoneBehaviour : NetworkBehaviour
{
    Rigidbody2D Rigidbody;
    bool moved = false;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!moved) return;

        if (IsHost && Rigidbody.velocity == Vector2.zero)
        {
            UpdateStoneClientRpc(transform.position);
            moved = false;
        }
    }

    [ClientRpc]
    public void AddForceToStoneClientRpc(Vector2 vec, float force)
    {
        GetComponent<Rigidbody2D>().AddForce(vec * force);
        moved = true;
    }

    [ClientRpc]
    public void StopStoneClientRpc()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;    
    }

    [ClientRpc]
    public void UpdateStoneClientRpc(Vector3 position)
    {
        Debug.Log("Synced");
        transform.position = position;
    }
}
