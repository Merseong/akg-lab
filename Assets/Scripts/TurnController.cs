using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using VContainer;

/// Game scene에서 auto parenting으로 sync 맞추기
/// VContainer로 inject
public class TurnController : NetworkBehaviour
{
    private HelloWorldService helloWorldService;
    public NetworkVariable<int> timer = new NetworkVariable<int>(0);

    [Inject]
    public void Construct(HelloWorldService helloWorldService)
    {
        this.helloWorldService = helloWorldService;
    }

    public override void OnNetworkSpawn()
    {
        Debug.Log($"Test ON!!");

        if (IsServer)
        {
            StartCoroutine(CountTimer());
        }

        helloWorldService.Hello();
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