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
            //インベントリの情報の取得
            Debug.Log($"インベントリの情報の取得開始");
            PlayFabClientAPI.GetUserInventory(userInventoryRequest, OnSuccessInventory, OnErrorInventory);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            //GetUserInventoryRequestのインスタンスを生成
            var userInventoryRequest = new GetUserInventoryRequest();

            //インベントリの情報の取得
            Debug.Log($"インベントリの情報の取得開始");
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

    //インベントリの情報の取得に成功
    private void OnSuccessInventory(GetUserInventoryResult result)
    {
        //result.Inventoryがインベントリの情報
        Debug.Log($"インベントリの情報の取得に成功 : インベントリに入ってるアイテム数 {result.Inventory.Count}個");

        //インベントリに入ってる各アイテムの情報をログで表示
        foreach (ItemInstance item in result.Inventory)
        {
            Debug.Log($"ID : {item.ItemId}, Name : {item.DisplayName}, ItemInstanceId : {item.ItemInstanceId}");
        }
    }

    //インベントリの情報の取得に失敗
    private void OnErrorInventory(PlayFabError error)
    {
        Debug.LogError($"インベントリの情報の取得に失敗\n{error.GenerateErrorReport()}");
    }

    //仮想通貨の追加に成功
    private void OnSuccessVirtualCurrency(GetUserInventoryResult result)
    {
        //result.Inventoryがインベントリの情報
        Debug.Log($"インベントリの情報の取得に成功");

        //所持している仮想通貨の情報をログで表示
        foreach (var virtualCurrency in result.VirtualCurrency)
        {
            Debug.Log($"仮想通貨 {virtualCurrency.Key} : {virtualCurrency.Value}");
        }
    }

    //仮想通貨の追加に失敗
    private void OnErrorVirtualCurrency(PlayFabError error)
    {
        Debug.LogError($"インベントリの情報の取得に失敗\n{error.GenerateErrorReport()}");
    }
}
