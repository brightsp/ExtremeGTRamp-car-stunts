using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitcam : MonoBehaviour {

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		
		Follownow ();
		Rotatenow ();
	}

	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;


	public float Xmin = 0f;
	public float Xmax= 0f;

	float distanceMin = .5f;
	float distanceMax = 15f;



	Vector3 negDistance ;
	Vector3 position ;
	Quaternion rotation;
	float Speedofcam=1,Desiredcamspeed=5;
	public Transform target;
	float x=15,y;




	void Rotatenow()
	{


		#if UNITY_EDITOR

		if (target) 
		{
			x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

			y = ClampAngle(y, yMinLimit, yMaxLimit);



		}

		#endif



		if (target) 
		{

		if(Input.touchCount<0)
		{
		return;
		}

		for(int i=0;i<Input.touchCount;i++)
		{
		x+=Input.GetTouch(i).deltaPosition.x*xSpeed*distance*0.004f;
		y -= Input.GetTouch(i).deltaPosition.y * ySpeed * 0.006f;

		}

		y = ClampAngle(y, yMinLimit, yMaxLimit);



		}

	
	}

	void Follownow()
	{

		Speedofcam = Mathf.Lerp (Speedofcam, Desiredcamspeed, 1 * Time.deltaTime);
		rotation = Quaternion.Euler(y, x, 0);

		distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);

		negDistance = new Vector3(0.0f, 0.0f, -distance);
		position = rotation * negDistance + target.position;



		transform.rotation = rotation;
		transform.position = position;
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}

}
