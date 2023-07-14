using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start()
    //randomises food position when game starts
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    //this code generates the random position for the food to spawn within the bounds defined
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    //this code will be called whenever the game objects collide with eachother as theyre all marked as a trigger
    {   
        if (other.tag == "Player") {
        //this just makes sure that its only the player tagged objects that will collide with the food
            RandomizePosition();
        }
        
    }
}
