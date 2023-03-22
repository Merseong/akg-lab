using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class StoneBehaviour : NetworkBehaviour
{


    [ClientRpc]
    public void AddForceToStoneClientRpc(Vector2 vec, float force)
    {
        GetComponent<Rigidbody2D>().AddForce(vec * force);
    }

    [ClientRpc]
    public void StopStoneClientRpc()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;    
    }

}
