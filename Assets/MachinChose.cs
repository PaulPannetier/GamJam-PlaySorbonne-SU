using UnityEngine;

public class MachinChose : MonoBehaviour
{

    [SerializeField] private Vector2 layout = new Vector2(100, 0);
    [SerializeField] private float rangeDeTestàlarriver;

    void OnLeBoutonQueTuVeuxAppuyer()
    {
        if (TestToTP())
        {
            ActivateTeleportation();
        }
        else
        {
            AvoidTeleportation();
        }
    }

    private bool TestToTP()
    {
        /*
        Transform resulPosition = transform.position + layout; // ca marhce pas car 2d/3d mais je te laisse faire 

        // tester par un raycast qu'il y a un sol et pas d'enemy à l'arriver 
        return // si t'as le droit ou pas 
        */
        return true;
    }

    private void ActivateTeleportation()
    {
        // tu tp et ca fait des trucs rigolo en animations
    }

    private void AvoidTeleportation()
    {
        // animation de tp raté
    }
}
