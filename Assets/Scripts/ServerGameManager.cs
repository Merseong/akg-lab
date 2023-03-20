using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerGameManager : NetworkSingletonBehaviour<ServerGameManager>
{
    NetworkVariable<int> m_testVar = new(0); // ������ ��������, ��ΰ� ���� �� ����
    NetworkVariable<int> m_testVar2 = new(0, NetworkVariableReadPermission.Owner); // ������ ��������������, Owner�� ���� �� ����
    NetworkVariable<int> m_testVar3 = new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner); // Owner�� ���� ���������� ��ΰ� ���� �� ����
    NetworkVariable<int> m_testVar4 = new(0, NetworkVariableReadPermission.Owner, NetworkVariableWritePermission.Owner); // Owner�� ����, Owner�� �б�

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        m_testVar.OnValueChanged += ReadNetworkVars;
        m_testVar2.OnValueChanged += ReadNetworkVars;
        m_testVar3.OnValueChanged += ReadNetworkVars;
        m_testVar4.OnValueChanged += ReadNetworkVars;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                UpdateNetworkVarsServerRpc(+10);
            }
            else if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                UpdateNetworkVarsServerRpc(-10);
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdateNetworkVarsServerRpc(int change)
    {
        m_testVar.Value += change;
        m_testVar2.Value += change;
        m_testVar3.Value += change;
        m_testVar4.Value += change;
    }

    private void ReadNetworkVars(int prev, int changed)
    {
        Debug.Log($"{m_testVar.Value} / {m_testVar2.Value} / {m_testVar3.Value} / {m_testVar4.Value}");
    }
}
