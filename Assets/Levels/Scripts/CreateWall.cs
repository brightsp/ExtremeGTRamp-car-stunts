using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CreateWall : MonoBehaviour {

	public float scalevalue=1;
	public bool Createwallnow;
	public int Rows=10,Columns=10;
	public GameObject Wallobj;
	public Wallsetup _wallsetupref;
	// Update is called once per frame
	void Update ()
	{
		if (Createwallnow)
		{
			Createwallnow = false;
			Createnow ();
		}
			
	}

	void Createnow()
	{
		GameObject Parentobj = new GameObject ();
		Parentobj.name = "Wall";
		_wallsetupref.Allbricks.Clear ();
		Vector3 pos=Vector3.zero;
		for (int i = 0; i < Rows; i++) 
		{
			for (int j = 0; j < Columns; j++) 
			{
				GameObject obj = Instantiate (Wallobj);
				pos.x = (i * obj.transform.localScale.x);
				pos.y= (j * obj.transform.localScale.y);
				obj.transform.position = pos;
				obj.transform.parent = Parentobj.transform;
				_wallsetupref.Allbricks.Add (obj.GetComponent<Rigidbody> ());

			}

		}
	}
}
