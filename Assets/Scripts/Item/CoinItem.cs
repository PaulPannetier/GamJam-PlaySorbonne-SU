using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item/CoinItem")]
public class CoinItem : ItemEffect
{
    [SerializeField] private int nbCoinsToEarn;

    public override void OnAttach(PlayerFightController playerFightController)
    {
        base.OnAttach(playerFightController);
        playerFightController.GetComponent<PlayerInventory>().EarnCoins(nbCoinsToEarn);
    }

    private void OnValidate()
    {
        nbCoinsToEarn = Mathf.Max(nbCoinsToEarn, 0);
    }
}
