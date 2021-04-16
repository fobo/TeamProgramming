using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolWaypoint : MonoBehaviour
{
    //This class may be used later for customizing behavior 
    //between points (such as changing an object to approach mode)
    float maximumApproachTime = -1; //Used to determine how long before an object gives up on approaching this waypoint
    public enum Behavior
    {
        CONTINUE,
        STOP,
        REVERSE,
    };

    public Behavior behavior = Behavior.CONTINUE;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }

    public void ExecuteBehavior(GameObject tourist)
    {
        MovementPatternController mpc = tourist.GetComponent<MovementPatternController>();
        if (mpc == null) return;
        switch (behavior)
        {
            case Behavior.CONTINUE:
                Continue(tourist,mpc);
                break;
            case Behavior.STOP:
                Stop(tourist, mpc);
                break;
            case Behavior.REVERSE:
                Reverse(tourist, mpc);
                break;
        }
    }

    public void Continue(GameObject tourist, MovementPatternController mpc)
    {
        mpc.changeToNextWaypoint();
    }

    public void Stop(GameObject tourist, MovementPatternController mpc)
    {
        mpc.setWaypointIndex(-1);
        mpc.patternType = MovementPatternController.PatternType.GUARD;
        mpc.home = gameObject.transform.position;
    }

    public void Reverse(GameObject tourist, MovementPatternController mpc)
    {
        if(mpc.getWaypointIndex() == 0)
        {
            //So that reverse doesn't work on entry points into patrol and cause weird behavior
            Continue(tourist, mpc);
            return;
        }
        mpc.patrolRoute.Reverse();
        //The current index of this waypoint is now changed to ((Length - 1) - previous_index)
        //then you would add 1 to get the next index which means we can just omit the -1 from before
        mpc.setWaypointIndex(mpc.patrolRoute.Count - mpc.getWaypointIndex());
    }
}
