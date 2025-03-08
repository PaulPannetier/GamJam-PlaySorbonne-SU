using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWatch : MonoBehaviour
{

    [SerializeField] private Vector2 layout = new Vector2(100, 0);
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private float rangeDeTestàlarriver;
    [SerializeField] private Boolean isInFuture = false;
    [SerializeField] private GameObject mainCamera;

    private bool isOnCooldown = false;

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
        if (isOnCooldown)
        {
            return;
        }
        if (isInFuture)
        {
            transform.position -= (Vector3)layout;
            StartCoroutine(MoveCamera(false));
        } else
        {
            transform.position += (Vector3)layout;
            StartCoroutine(MoveCamera(true));
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

    private IEnumerator MoveCamera(Boolean toTheFuture)
    {
        if (isOnCooldown)
            yield break; // Exit if still on cooldown

        isOnCooldown = true;
        Vector3 startPos = mainCamera.transform.position;
        Vector3 targetPos = startPos;
        if (toTheFuture)
        {
            targetPos += (Vector3)layout;
        } else
        {
            targetPos -= (Vector3)layout;
        }
            
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration; // Normalize time (0 to 1)
            t = Mathf.SmoothStep(0, 1, t); // Apply ease in - ease out
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, t);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for next frame

        }

        mainCamera.transform.position = targetPos;

        yield return new WaitForSeconds(moveDuration); // Wait for cooldown

        isOnCooldown = false;
    }
}
