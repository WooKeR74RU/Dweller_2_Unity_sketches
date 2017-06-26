using UnityEngine;
using UnityEngine.UI;

public class CameraScale : MonoBehaviour
{
	public Camera cam;
	public Slider ScaleSlider;
	void Start()
	{
		UpdateCameraScale();
	}

	public void UpdateCameraScale()
	{
		float ss = Screen.currentResolution.height;
		cam.orthographicSize = Screen.currentResolution.height / ScaleSlider.value;
	}
}