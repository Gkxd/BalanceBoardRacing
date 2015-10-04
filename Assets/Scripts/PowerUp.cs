using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public enum Mode {
        BoostPad,
        Charge,
    }

    public Mode mode;
    public float value;
    public bool respawn = true;
    public float respawnTimer = 100;

    private MeshRenderer rend;
    private Collider coll; 

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<PlayerController>() != null)
        {
            PlayerController controller = other.GetComponent<PlayerController>();

            switch (mode) //functionality depending on mode
            {
                case Mode.Charge: //increase players stored boost by value
                    if (controller.boost < 100) //boost not already full
                    {
                        controller.boost += value;
                        if (controller.boost > 100)
                        {
                            controller.boost = 100;
                        }
                        Hide();
                        Invoke("Show", respawnTimer);
                    }
                    break;


                case Mode.BoostPad: //give instant speed boost
                    controller.padBoost += value;
                    controller.boosting = true; 
                    break;
            }
        }
    }

    void Hide()
    {
        rend.enabled = false;
        coll.enabled = false;
    }

    void Show()
    {
        rend.enabled = true;
        coll.enabled = true;
    }
    
}
