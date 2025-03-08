using System;
using UnityEngine;

public class PlayerWatch : MonoBehaviour
{

    [SerializeField] private Vector2 layout = new Vector2(100, 0);
    [SerializeField] private float rangeDeTestàlarriver;
    [SerializeField] private Boolean isInFuture = false;
    [SerializeField] private GameObject mainCamera;

    void OnPressT()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T key was pressed!");
            if (TestToTP())
            {
                ActivateTeleportation();
            }
            else
            {
                AvoidTeleportation();
            }
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
        if (isInFuture)
        {
            transform.position -= (Vector3)layout;
            mainCamera.transform.position -= (Vector3)layout;
        } else
        {
            transform.position += (Vector3)layout;
            mainCamera.transform.position += (Vector3)layout;
        }
        isInFuture = !isInFuture;
    }

    private void AvoidTeleportation()
    {
        // animation de tp raté
    }

    private void Update()
    {
        OnPressT();
    }
}
