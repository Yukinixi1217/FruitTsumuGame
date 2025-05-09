using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using Unity.Services.Core;
using System;
using System.Threading.Tasks;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IStoreController storeController;
    public static IExtensionProvider storeExtensionProvider;

    private const string removeAdsId = "remove_ads";

    async void Start()
    {
        await InitializeServicesAndPurchasing();
    }

    private async Task InitializeServicesAndPurchasing()
    {
        try
        {
            await UnityServices.InitializeAsync();

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(removeAdsId, ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }
        catch (Exception e)
        {
            Debug.LogError($"Unity Services/IAP 初期化失敗: {e.Message}");
        }
    }

    public void BuyRemoveAds()
    {
        if (storeController != null && storeController.products.WithID(removeAdsId).availableToPurchase)
        {
            storeController.InitiatePurchase(removeAdsId);
        }
        else
        {
            Debug.LogWarning("購入できません");
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (args.purchasedProduct.definition.id == removeAdsId)
        {
            PlayerPrefs.SetInt("isAdRemoved", 1);
            Debug.Log("広告削除購入完了");
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
        Debug.Log("IAP 初期化成功");
    }

    // ✅ 修正ポイント：引数1つにする
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"IAP 初期化失敗: {error}");
    }
    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError($"IAP 初期化失敗: {error} - {message}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.LogError($"購入失敗: {product.definition.id} - {reason}");
    }
}
