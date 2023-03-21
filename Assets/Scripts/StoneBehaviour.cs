using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class StoneBehaviour : NetworkBehaviour
{


    [ServerRpc]
    public void AddForceToStoneServerRpc(Vector2 vec, float force)
    {
        GetComponent<Rigidbody2D>().AddForce(vec * force);
    }

    [ServerRpc]
    public void StopStoneServerRpc()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;    
    }

}
