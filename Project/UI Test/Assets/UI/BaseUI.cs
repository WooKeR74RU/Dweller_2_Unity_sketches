using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public interface BaseUI
{
	void setSize(float width, float height);
	void setWidth(float width);
	void setHeight(float height);
	void setPadding(float l, float r, float t, float d);
}
