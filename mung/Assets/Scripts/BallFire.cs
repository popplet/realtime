using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFire : MonoBehaviour {
    public float ballAngle;
    public GameObject obj;
    public GameObject target;
    public bool powerUp = false;
    public float firePower;
    
    private void Awake()
    {
        firePower = 0.0f;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(powerUp)
        {
            powerSet();
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void Fire()
    {
            this.GetComponent<Rigidbody>().AddForce(new Vector3 (1,0,1)*firePower,ForceMode.Impulse);

    }
    public void powerSet()
    {
        
        firePower += 0.1f*Time.deltaTime;
        if (firePower > 5.0f)
            firePower = 0.0f;
    }
    public void powUp()
    {
        if (!powerUp) powerUp = true;
        else powerUp = false;
    }
}
