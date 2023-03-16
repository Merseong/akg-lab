using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// Game scene에서 auto parenting으로 sync 맞추기
/// VContainer로 inject
public class TurnManager : NetworkBehaviour
{
    public NetworkVariable<int> timer = new NetworkVariable<int>(0);

    public override void OnNetworkSpawn()
    {
        Debug.Log($"Test ON!!");

        if (IsServer)
        {
            StartCoroutine(CountTimer());
        }
    }

    private IEnumerator CountTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timer.Value++;
        }
    }
}