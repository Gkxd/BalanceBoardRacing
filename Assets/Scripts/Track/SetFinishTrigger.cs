using UnityEngine;
using System.Collections;

public class SetFinishTrigger : MonoBehaviour {

    public BoxCollider finishCollider;

    //public Text timer;

    void OnTriggerExit()
    {
        /* If tree to ensure directionality of lap increments */
        /* finish is initially deactivated */
        /* Passing through set activates finish iff finish is deactivated */
        /* Passing through set when finish is active deactivates finish */
        /* Passing through finish deactivates finish */
        if (finishCollider.enabled == true) 
        {
            finishCollider.enabled = false;
        }
        else
        {
            finishCollider.enabled = true;
        }
        
    }

}
