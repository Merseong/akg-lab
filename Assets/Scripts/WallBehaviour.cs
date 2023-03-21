using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WallBehaviour : NetworkBehaviour
{
    BoxCollider2D boxCollider2D;
    MeshRenderer meshRenderer;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        DisableWallClientRpc();
    }

    [ClientRpc]
    void DisableWallClientRpc()
    {
        boxCollider2D.enabled = false;
        meshRenderer.enabled = false;
    }
}
