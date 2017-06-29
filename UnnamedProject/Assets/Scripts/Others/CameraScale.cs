using UnityEngine;
using UnityEngine.UI;

public class CameraScale : MonoBehaviour
{
	public Camera cam;
	public Slider scaleSlider;
	private void Start()
	{
		UpdateCameraScale();
	}
	public void UpdateCameraScale()
	{
		cam.orthographicSize = Screen.currentResolution.height / scaleSlider.value;
	}
}