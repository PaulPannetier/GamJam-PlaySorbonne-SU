using UnityEngine;

public abstract class Spell : MonoBehaviour
 {
    public string spellName;
    public float cooldown; 
    
    public abstract bool Condition(EnemyController enemy);
    public abstract void Cast(EnemyController enemy);

}