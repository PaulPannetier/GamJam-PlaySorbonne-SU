using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
    public virtual void OnAttach(PlayerFightController playerFightController)
    {

    }

    public virtual void OnLaunchAttack(PlayerAttack playerAttack)
    {

    }

    public virtual float GetBonusDamagePercent(PlayerAttack attack, IDamageable enemy)
    {
        return 0f;
    }

    public virtual float GetBonusDamage(PlayerAttack attack, IDamageable enem)
    {
        return 0f;
    }

    public virtual int GetPowerUp(PlayerAttack attack)
    {
        return 0;
    }
}
