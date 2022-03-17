using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    [SerializeField] float spawnRange = 7.5f;

    [SerializeField] float yMultiplier = 1f;
    [SerializeField] float xMultiplier = 1f;

    Vector3 currentPosition;

    Player player;
    [SerializeField] float eatDistance = 0.5f;

    private void Start()
    {
        PlaceFood();
        transform.position = currentPosition;

        player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {

        // We want it to float up and down
        // For this we need a bit of math!

        // Let's use a sine wave to move it smoothly up and down.
        // We need a height multiplier (y axis) and a speed multiplier (x axis)

        Vector3 movementOffset = new Vector3(0f, Mathf.Sin(Time.time * xMultiplier) * yMultiplier, 0f);

        transform.position = currentPosition + movementOffset;



        // Eat the food if player is close
        if (Vector3.Distance(transform.position, player.transform.position) < eatDistance)
        {
            PlaceFood();
            player.score += 1;
        }

    }


    private void PlaceFood()
    {
        currentPosition = new Vector3(Random.Range(-spawnRange, spawnRange), 0f, Random.Range(-spawnRange, spawnRange));
    }



}
