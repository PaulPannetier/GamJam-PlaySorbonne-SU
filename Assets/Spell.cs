using UnityEngine;

public abstract class Spell : ScriptableObject {
    public string spellName;
    public float cooldown; 
    public float lastCastTime = -Mathf.Infinity;

    // Méthode qui exécute le sort, on peut lui passer le lanceur ou la cible si besoin.
    public abstract bool Condition(EnemyController enemy);
    public abstract void Cast(EnemyController enemy);

}