using Unity.Netcode;

public interface ITurnController
{
    public delegate void TurnEventHandler(bool isPlayerTurn);
    public ReactData<bool> IsPlayerTurn { get; }
    public NetworkVariable<int> Timer { get; }
    public event TurnEventHandler OnTurnStarted;
    public event TurnEventHandler OnTurnEnded;
    public void PassTurn(bool isHonorSkip = false);
}