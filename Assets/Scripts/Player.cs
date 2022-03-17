using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] float speed = 3f;

    [SerializeField] LayerMask layerMask;

    [SerializeField] Text uiText;

    public int score = 0;

    Vector3 targetPos;
    Vector3 direction;

    void Update()
    {

        // Take mouse click input
        if (Input.GetMouseButtonDown(0))
        {
            // Get point on ground where we clicked

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit, 100f, layerMask);

            if (hasHit)
            {

                // We want to get the location of the hit
                targetPos = hit.point;

                // We want to move our bear to that location at a constant speed
                // We need the direction to move in!

                direction = (targetPos - transform.position).normalized;

                // We need to move in direction at speed
                // Let's do this in a fixed update method
            }
        }


    }

    private void FixedUpdate()
    {
        // Let's make the bear look in the direction of movement
        transform.forward = direction;

        // We have target position and direction; let's move our bear

        Vector3 movementThisFrame = direction * speed * Time.fixedDeltaTime;

        // If we overshoot our target, we're going to rubber band around (maybe forever!)
        // Let's add a clause if we're close enough to our target that we would overshoot it

        if (Vector3.Distance(transform.position, targetPos) <= movementThisFrame.magnitude)
        {

            transform.position = targetPos;
        }
        else
        {
            transform.position = transform.position + movementThisFrame;
        }


        uiText.text = score.ToString();

    }
}
