using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvashandler : MonoBehaviour {

	public Canvas Thiscanvas;
	public CanvasScaler Thiscanvasscaler;
	public GraphicRaycaster Thisgraphicraycaster;

	void OnEnable()
	{
		Setstatus (true);
	}

	void OnDisable()
	{
		Setstatus (false);

	}

	void Setstatus(bool status)
	{
		Thiscanvas.enabled = status;
		Thiscanvasscaler.enabled = status;
		Thisgraphicraycaster.enabled = status;

	}
}
