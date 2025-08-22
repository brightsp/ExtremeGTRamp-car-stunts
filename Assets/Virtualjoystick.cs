using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Virtualjoystick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler {



	private Image Bgimage;
	public Image Joystickimage;
	private Vector3 inputvector;

	public int Sidecount = 0;

	private void Start()
	{
		Bgimage = GetComponent<Image> ();
		//Joystickimage = transform.GetChild (0).GetComponent<Image> ();
	}

	public virtual void OnDrag(PointerEventData ped)
	{

		//working 

		if (Sidecount == 0) 
		{
			Vector2 pos;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle (Bgimage.rectTransform, ped.position, ped.pressEventCamera, out pos)) 
			{
				pos.x = (pos.x / Bgimage.rectTransform.sizeDelta.x);
				pos.y = (pos.y / Bgimage.rectTransform.sizeDelta.y);

				inputvector = new Vector3 (pos.x * 2 + 1, 0, pos.y * 2 - 1);
				inputvector = (inputvector.magnitude > 1.0f) ? inputvector.normalized : inputvector;

//				Debug.Log ("pos " + pos + " input " + inputvector);

				Joystickimage.rectTransform.anchoredPosition = new Vector3 (inputvector.x * (Bgimage.rectTransform.sizeDelta.x/ 2), inputvector.z * (Bgimage.rectTransform.sizeDelta.y / 2));
				
			}
		}
		else 
		{
			Vector2 pos;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle (Bgimage.rectTransform, ped.position, ped.pressEventCamera, out pos))
			{


				pos.x = (pos.x / Bgimage.rectTransform.sizeDelta.x);
				pos.y = (pos.y / Bgimage.rectTransform.sizeDelta.y);



				inputvector = new Vector3 (1-(pos.x*2), 0,pos.y * 2 - 1);
				inputvector = (inputvector.magnitude > 1.0f) ? inputvector.normalized : inputvector;

//				Debug.Log ("pos " + pos + " input " + inputvector);
				Joystickimage.rectTransform.anchoredPosition = new Vector3 (-inputvector.x * (Bgimage.rectTransform.sizeDelta.x / 2), inputvector.z * (Bgimage.rectTransform.sizeDelta.y / 2));

			}

		}


	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);
	}



	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputvector = Vector3.zero;
		Joystickimage.rectTransform.anchoredPosition = Vector3.zero;
	}

	public float Horizontal()
	{
		if (inputvector.x != 0)
			return inputvector.x;
		else
			return Input.GetAxis ("Horizontal");
	}

	public float Vertical()
	{
		if (inputvector.z != 0)
			return inputvector.z;
		else
			return Input.GetAxis ("Vertical");
	}



}
