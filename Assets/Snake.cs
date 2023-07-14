using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    //makes the snake go automatically right if no bindings are pressed

    private List<Transform> _segments = new List<Transform>();
    //keeps track of all the snake segments

    public Transform segmentPrefab;
    public int initialSize = 4;
    //sets the snakes initial size

    private void Start()
    //starts list and adds the segments
    {
        ResetState();
    }
    
    private void Update()
    //sets binding for the players movement directions
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            _direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            _direction = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            _direction = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            _direction = Vector2.right;
        }

    }

    private void FixedUpdate()
    //updates the actual position of the snake when bindings are pressed
    //Mathf.Round ensures that the numbers are rounded so the snake is aligned to the grid
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        //this code makes sure that from the tail each segment is following the one infront of it and updates the head last
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow()
    //made to actually grow the snakes body
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        //this takes the tail of the body

        _segments.Add(segment);
    }

    private void ResetState()
    //creates the function ResetState which is what happens when the player collides with the wall or itself / loses
    {
        for (int i = 1; i < _segments.Count; i++) {
            //starts at 1 as 0 is actually the players head which should always remain
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        //clears list but adds back the head

        for (int i = 1; i < this.initialSize; i++) {
            //adds back the segments until the set initial size is hit
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
        //resets position at 0
    }

    private void OnTriggerEnter2D(Collider2D other)
    //this code will be called whenever the game objects collide with eachother as theyre all marked as a trigger
    {   
        if (other.tag == "Food") {
        //this just makes sure that its only the food tagged objects that will collide with the snake
            Grow();
        } else if (other.tag == "Obstacle") {
        //this makes sure that the Snake collides with the walls as well as with its own body
            ResetState();
        }
        
    }
}


