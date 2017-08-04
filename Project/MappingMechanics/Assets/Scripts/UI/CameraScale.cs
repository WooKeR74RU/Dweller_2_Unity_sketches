using UnityEngine;
using UnityEngine.UI;

public class CameraScale : MonoBehaviour
{
	public Slider scaleSlider;
	private void Start()
	{
		scaleSlider.minValue = GlobalData.minOrthographicSize;
		scaleSlider.maxValue = GlobalData.maxOrthographicSize;
	}
	public void UpdateCameraScale()
	{
		GlobalData.camera.orthographicSize = GlobalData.maxOrthographicSize + GlobalData.minOrthographicSize - scaleSlider.value;
	}
}