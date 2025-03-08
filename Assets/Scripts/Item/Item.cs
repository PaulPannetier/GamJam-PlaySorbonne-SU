using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private List<ItemEffect> effects;
    [SerializeField] private float value;
    public Sprite icon;

    public void OnAttach(PlayerFightController playerFightController)
    {
        foreach (ItemEffect effect in effects)
        {
            effect.OnAttach(playerFightController);
        }
    }

    public void OnLaunchAttack(PlayerAttack playerAttack)
    {
        foreach (ItemEffect effect in effects)
        {
            effect.OnLaunchAttack(playerAttack);
        }
    }

    public float GetBonusDamagePercent(PlayerAttack attack, IDamageable enemy)
    {
        float damage = 0f;
        foreach (ItemEffect effect in effects)
        {
            damage += effect.GetBonusDamagePercent(attack, enemy);
        }
        return damage;
    }

    public float GetBonusDamage(PlayerAttack attack, IDamageable enemy)
    {
        float damage = 0f;
        foreach (ItemEffect effect in effects)
        {
            damage += effect.GetBonusDamage(attack, enemy);
        }
        return damage;
    }

    public int GetPowerUp(PlayerAttack attack)
    {
        int damage = 0;
        foreach (ItemEffect effect in effects)
        {
            damage += effect.GetPowerUp(attack);
        }
        return damage;
    }
}
