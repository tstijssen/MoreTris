using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupOscillation : MonoBehaviour {
    float startY;
    public float yMovement = 0;
    // Use this for initialization
    void Start () {
        startY = transform.position.y;
	}

	// Update is called once per frame
	void FixedUpdate () {
        yMovement = Mathf.PingPong(Time.time, 0.5f);
        this.transform.position = new Vector2(transform.position.x, startY + yMovement);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("Collision");
        if (collision.transform.tag.Equals("Block"))
        {
            this.gameObject.SetActive(false);
        }
    }

}
