using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MobQuirkRunSpeed : MonoBehaviour {
    private Collider other;
    public GameObject mobcontroller;
	// Use this for initialization
	void Start () {
	
	}
	
    public IEnumerator SpeedUp(RigidbodyFirstPersonController.MovementSettings settings)
    {
        Debug.Log("You picked up a speed quirk");
        settings.ForwardSpeed *= 1.5f;
        settings.BackwardSpeed *= 1.5f;
        settings.StrafeSpeed *= 1.5f;
        Debug.Log("Forward speed is now" + settings.ForwardSpeed);

        yield return new WaitForSeconds(5f);
        //Debug.Log("Resetting to default values");
        //mobcontroller.GetComponent<RigidbodyFirstPersonController>().movementSettings.ForwardSpeed /= 1.5f;
        //mobcontroller.GetComponent<RigidbodyFirstPersonController>().movementSettings.BackwardSpeed /= 1.5f;
        //mobcontroller.GetComponent<RigidbodyFirstPersonController>().movementSettings.StrafeSpeed /= 1.5f;
        yield return null;

    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(SpeedUp(mobcontroller.GetComponent<RigidbodyFirstPersonController>().movementSettings));
        }
    }
	void Update ()
    {
	   
	}
}
