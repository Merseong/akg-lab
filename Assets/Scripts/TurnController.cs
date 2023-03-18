using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using VContainer;

/// Game scene에서 auto parenting으로 sync 맞추기
/// VContainer로 inject
public class TurnController : NetworkBehaviour, ITurnController
{
    private ReactData<bool> isPlayerTurn = new ReactData<bool>(false);
    private NetworkVariable<int> timer = new NetworkVariable<int>();
    // 나중에 GameSetting 클래스로 넘겨야함
    [SerializeField]
    private int maxTime = 15;

    public ReactData<bool> IsPlayerTurn => isPlayerTurn;
    public NetworkVariable<int> Timer => timer;

    public event ITurnController.TurnEventHandler OnTurnStarted;
    public event ITurnController.TurnEventHandler OnTurnEnded;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        // Initialize timer
        timer.Value = maxTime;
        StartCoroutine(TickTimer());

        // Randomly give token to one player
        var clientsList = NetworkManager.Singleton.ConnectedClientsList.ToList();
        var client = clientsList.PickRandom();
        
        // Create clientRpcParams for TargetRpc
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[1]{client.ClientId}
            }
        };

        InitTurnClientRpc(clientRpcParams);
    }

    public void PassTurn(bool isHonorSkip = false)
    {
        if (!IsClient) return;

        PassTurnServerRpc(isHonorSkip);
    }

    [ServerRpc]
    private void PassTurnServerRpc(bool isHonorSkip)
    {
        throw new System.NotImplementedException();
    }

    [ClientRpc]
    private void InitTurnClientRpc(ClientRpcParams clientRpcParams) 
    {
        isPlayerTurn.Value = true;
    }

    [ClientRpc]
    private void PassTurnClientRpc()
    {
        OnTurnStarted?.Invoke(isPlayerTurn.Value);
        isPlayerTurn.Value = !isPlayerTurn.Value;
        OnTurnEnded?.Invoke(isPlayerTurn.Value);
    }

    private IEnumerator TickTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timer.Value--;

            if (timer.Value == 0)
            {
                // Pass turn to another player
                PassTurnClientRpc();
                timer.Value = maxTime;
            }
        }
    }
}