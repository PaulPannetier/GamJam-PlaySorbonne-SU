using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item/IncreaseDamage")]
public class IncreaseDamage : ItemEffect
{
    [SerializeField] private float percentDamageIncrease;
    [SerializeField] private float damageIncrease;

    public override float GetBonusDamagePercent(PlayerAttack attack, IDamageable enemy)
    {
        return percentDamageIncrease;
    }

    public override float GetBonusDamage(PlayerAttack attack, IDamageable enem)
    {
        return damageIncrease;
    }

    private void OnValidate()
    {
        damageIncrease = Mathf.Max(0f, damageIncrease);
        percentDamageIncrease = Mathf.Max(0f, percentDamageIncrease);
    }
}
