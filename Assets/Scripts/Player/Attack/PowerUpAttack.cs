using UnityEngine;

public abstract class PowerUpAttack : PlayerAttack
{
    protected int GetPowerUp()
    {
        return base.fightController.GetComponent<PlayerInventory>().GetPowerUp(this);
    }
}
