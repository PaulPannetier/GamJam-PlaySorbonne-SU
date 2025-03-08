using System;
using System.Collections;
using UnityEngine;

public class PlayerWatch : MonoBehaviour
{
    [SerializeField] private Vector2 layout = new Vector2(100, 0);
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private bool isInFuture = false;
    [SerializeField] private InputKey activateKey;
    [SerializeField] private LayerMask groundLayerMask;

    private bool isOnCooldown = false;

    void OnPressT()
    {
        if (InputManager.GetKeyDown(activateKey))
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

    }

    private bool TestToTP()
    {
        float checkRadius = 0.5f;
        Collider2D hit;
        if (isInFuture)
        {
            hit = Physics2D.OverlapCircle(new Vector2(transform.position.x - layout.x , transform.position.y - layout.y), checkRadius, groundLayerMask);
        }
        else
        {
            hit = Physics2D.OverlapCircle(new Vector2(transform.position.x + layout.x , transform.position.y + layout.y), checkRadius, groundLayerMask);
        }
        Debug.Log(hit == null);
        return hit == null;
    }

    private void ActivateTeleportation()
    {
        if (isOnCooldown)
        {
            return;
        }

        if (isInFuture)
        {
            gameObject.transform.position = new Vector2(transform.position.x - layout.x, transform.position.y - layout.y);
        }
        else
        {
            gameObject.transform.position = new Vector2(transform.position.x + layout.x, transform.position.y + layout.y);
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
    /*
        private IEnumerator MoveCamera(Boolean toTheFuture)
        {
            if (isOnCooldown)
                yield break;

            isOnCooldown = true;
            Vector3 startPos = Camera.main.transform.position;
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
                float t = elapsedTime / moveDuration;
                t = Mathf.SmoothStep(0f, 1f, t);
                Camera.main.transform.position = Vector3.Lerp(startPos, targetPos, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Camera.main.transform.position = targetPos;

            yield return new WaitForSeconds(moveDuration);

            isOnCooldown = false;
        }*/
}
