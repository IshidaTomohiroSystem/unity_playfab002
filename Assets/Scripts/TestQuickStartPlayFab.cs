using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class TestQuickStartPlayFab : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var userInventoryRequest = new GetUserInventoryRequest();
            //�C���x���g���̏��̎擾
            Debug.Log($"�C���x���g���̏��̎擾�J�n");
            PlayFabClientAPI.GetUserInventory(userInventoryRequest, OnSuccessInventory, OnErrorInventory);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            //GetUserInventoryRequest�̃C���X�^���X�𐶐�
            var userInventoryRequest = new GetUserInventoryRequest();

            //�C���x���g���̏��̎擾
            Debug.Log($"�C���x���g���̏��̎擾�J�n");
            PlayFabClientAPI.GetUserInventory(userInventoryRequest, OnSuccessVirtualCurrency, OnErrorVirtualCurrency);
        }
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    //�C���x���g���̏��̎擾�ɐ���
    private void OnSuccessInventory(GetUserInventoryResult result)
    {
        //result.Inventory���C���x���g���̏��
        Debug.Log($"�C���x���g���̏��̎擾�ɐ��� : �C���x���g���ɓ����Ă�A�C�e���� {result.Inventory.Count}��");

        //�C���x���g���ɓ����Ă�e�A�C�e���̏������O�ŕ\��
        foreach (ItemInstance item in result.Inventory)
        {
            Debug.Log($"ID : {item.ItemId}, Name : {item.DisplayName}, ItemInstanceId : {item.ItemInstanceId}");
        }
    }

    //�C���x���g���̏��̎擾�Ɏ��s
    private void OnErrorInventory(PlayFabError error)
    {
        Debug.LogError($"�C���x���g���̏��̎擾�Ɏ��s\n{error.GenerateErrorReport()}");
    }

    //���z�ʉ݂̒ǉ��ɐ���
    private void OnSuccessVirtualCurrency(GetUserInventoryResult result)
    {
        //result.Inventory���C���x���g���̏��
        Debug.Log($"�C���x���g���̏��̎擾�ɐ���");

        //�������Ă��鉼�z�ʉ݂̏������O�ŕ\��
        foreach (var virtualCurrency in result.VirtualCurrency)
        {
            Debug.Log($"���z�ʉ� {virtualCurrency.Key} : {virtualCurrency.Value}");
        }
    }

    //���z�ʉ݂̒ǉ��Ɏ��s
    private void OnErrorVirtualCurrency(PlayFabError error)
    {
        Debug.LogError($"�C���x���g���̏��̎擾�Ɏ��s\n{error.GenerateErrorReport()}");
    }
}
