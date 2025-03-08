using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item/IncreaseSpellPower")]
public class IncreaseSpellPower : ItemEffect
{
    [SerializeField] private int powerUp;

    public override int GetPowerUp(PlayerAttack attack)
    {
        return powerUp;
    }

    private void OnValidate()
    {
        powerUp = Mathf.Max(powerUp, 0);
    }
}
