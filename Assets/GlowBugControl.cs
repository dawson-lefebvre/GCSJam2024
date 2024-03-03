using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlowBugControl : MonoBehaviour
{
    public enum BugState
    {
        LookingForPlayer,
        LookingForLamp,
        StayingOnLamp
    }

    GameObject followedObject;

    public BugState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BugState.LookingForPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BugState.LookingForPlayer:
                List<GameObject> hit = new List<GameObject>();

                foreach (RaycastHit2D collider in Physics2D.CircleCastAll(transform.position, 3, Vector2.up))
                {
                    hit.Add(collider.transform.gameObject);

                }
                foreach (GameObject thing in hit)
                {
                    if(thing.GetComponent<PlayerController>())
                    {
                        state = BugState.LookingForLamp;
                        followedObject = thing;
                    }
                }
                break;
            case BugState.LookingForLamp:
                //Move to player
                Vector2 Axis;
                Axis = followedObject.transform.position - transform.position;
                Axis.Normalize();

                float speed;

                speed = Vector2.Distance(transform.position, followedObject.transform.position) / 1.5f;


                if(Vector2.Distance(transform.position, followedObject.transform.position) < 0.5)
                {
                    speed = 0;
                }
                GetComponent<Rigidbody2D>().velocity = Axis * speed;
                //Look for Lamp
                List<GameObject> ObjectsInRange = new List<GameObject>();

                foreach (RaycastHit2D collider in Physics2D.CircleCastAll(transform.position, 3, Vector2.up))
                {
                    ObjectsInRange.Add(collider.transform.gameObject);

                }
                foreach (GameObject thing in ObjectsInRange)
                {
                    if (thing.GetComponent<LampControl>())
                    {
                        state = BugState.StayingOnLamp;
                        followedObject = thing;
                        followedObject.GetComponent<LampControl>().holdingLightBugs = true;
                    }
                }
                

                break;
            case BugState.StayingOnLamp:
                Axis = followedObject.transform.position - transform.position;
                Axis.Normalize();

                speed = Vector2.Distance(transform.position, followedObject.transform.position) / 1.5f;


                if (Vector2.Distance(transform.position, followedObject.transform.position) < 0.5)
                {
                    speed = 0;
                }
                GetComponent<Rigidbody2D>().velocity = Axis * speed;
                break;
        }

    }
}
