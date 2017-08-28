using UnityEngine;
using UnityEngine.UI;

public class SelectBlock : MonoBehaviour
{
	public GameObject selectTexture;
	public static bool isStayBlockEnable = true;
	float lastX = -1, lastY = -1;

	private void Update()
	{
		float scaleFactor = Map.UI.transform.localScale.x;
		float SPRITE_SIZE = Map.SPRITE_SIZE * scaleFactor * Map.map.GetComponent<RectTransform>().localScale.x;
		selectTexture.GetComponent<RectTransform>().sizeDelta = new Vector2(SPRITE_SIZE / scaleFactor, SPRITE_SIZE / scaleFactor);
		float menuPanelHeight = 120 * scaleFactor;
		float menuPanelHeight2 = 1000 * scaleFactor;

		//if mouse in menu panel
		if (Input.mousePosition.y <= menuPanelHeight || Input.mousePosition.y >= menuPanelHeight2)
		{
			isStayBlockEnable = false;
		}

		//transform selection rect and calculate world coordinate in array
		float x_ = Input.mousePosition.x - gameObject.transform.position.x + gameObject.transform.parent.gameObject.GetComponent<RectTransform>().rect.x, y_ = Input.mousePosition.y - gameObject.transform.position.y;
		x_ /= SPRITE_SIZE;
		y_ /= SPRITE_SIZE;
		x_ = (int)x_;
		y_ = (int)y_;
		//Debug.Log(x_ + " " + y_);
		selectTexture.transform.position = new Vector2(x_ * SPRITE_SIZE + gameObject.transform.position.x, y_ * SPRITE_SIZE + gameObject.transform.position.y);
		Map.map.GetComponent<ScrollRect>().enabled = true;
		if (Input.GetMouseButtonUp(1))
		{
			showBlockInfoDialog(x_, y_);
			return;
		}
		if (Input.GetKey(KeyCode.Escape))
		{
			BlockInfoPanel.hidePanel();
		}
		if (multiplyBlockStayMode(x_, y_))
			return;
		//if multiplay stay mode is not availble
		lastX = x_;
		lastY = y_;
		stayBlock(x_, y_);
	}

	public void showBlockInfoDialog(float x_, float y_)
	{
		string name = ((int)(x_) / Map.segmentWidth) + "x" + ((int)(y_) / Map.segmentHeight);
		BlockInfoPanel.showLayerInfo(Map.beginFullMap[name][(int)y_ % Map.segmentHeight][(int)x_ % Map.segmentWidth]);
	}
	//Add block to texture
	public void stayBlock(float x_, float y_)
	{

		if (Input.GetMouseButtonUp(0))
		{
			if (isStayBlockEnable == false)
			{
				isStayBlockEnable = true;
				return;
			}
			selectTexture.transform.position = new Vector2(-10000, -10000);
			if (x_ >= 0 && Map.segmentCountX * Map.segmentWidth > x_ && y_ >= 0 && y_ < Map.segmentCountY * Map.segmentHeight)
			{
				Map.DrawTexture((int)x_, (int)y_, Menu.cur_block);
			}
		}
	}
	//Add several blocks to texture
	public bool multiplyBlockStayMode(float x_, float y_)
	{
		if (Input.GetKey(KeyCode.LeftControl))
		{
			Map.map.GetComponent<ScrollRect>().enabled = false;
			if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl) && !(x_ == lastX && y_ == lastY))
			{
				selectTexture.transform.position = new Vector2(-10000, -10000);
				if (x_ >= 0 && Map.segmentCountX * Map.segmentWidth > x_ && y_ >= 0 && y_ < Map.segmentCountY * Map.segmentHeight)
				{
					Map.DrawTexture((int)x_, (int)y_, Menu.cur_block);
				}
			}
			lastX = x_;
			lastY = y_;
			return true;
		}
		return false;
	}
	//bool varible that responsible for stay block enable
	public void disableBlockPlace()
	{
		isStayBlockEnable = false;
	}
}
