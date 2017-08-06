using System;

public class Pair<FT, ST> : IEquatable<Pair<FT, ST>>
{
	public FT first;
	public ST second;
	public Pair()
	{}
	public Pair(FT first, ST second)
	{
		this.first = first;
		this.second = second;
	}
	public override string ToString()
	{
		return "[" + first.ToString() + ", " + second.ToString() + "]";
	}
	public bool Equals(Pair<FT, ST> other)
	{
		return first.Equals(other.first) && second.Equals(other.second);
	}
	public override bool Equals(object other)
	{
		if (other is Pair<FT, ST>)
			return Equals(other as Pair<FT, ST>);
		return false;
	}
	public override int GetHashCode()
	{
		int hash = 19;
		hash = hash * 37 + first.GetHashCode();
		hash = hash * 37 + second.GetHashCode();
		return hash;
	}
}

public class Vector<Type>
{
	public Type[] array;
	public Vector()
	{
		array = new Type[0];
	}
	public Vector(int size)
	{
		array = new Type[size];
	}
	public Type this[int index]
	{
		get
		{
			return array[index];
		}
		set
		{
			array[index] = value;
		}
	}
	public Type At(int index)
	{
		return array[index];
	}
	public Type Front
	{
		get
		{
			return array[0];
		}
		set
		{
			array[0] = value;
		}
	}
	public Type Back
	{
		get
		{
			return array[array.Length - 1];
		}
		set
		{
			array[array.Length - 1] = value;
		}
	}
	public void Insert(int index, Type item)
	{
		Array.Resize(ref array, array.Length + 1);
		for (int i = array.Length - 1; i > index; i--)
			array[i] = array[i - 1];
		array[index] = item;
	}
	public void RemoveAt(int index)
	{
		for (int i = index; i < array.Length - 1; i++)
			array[i] = array[i + 1];
		Array.Resize(ref array, array.Length - 1);
	}
	public void Add(Type item)
	{
		Array.Resize(ref array, array.Length + 1);
		array[array.Length - 1] = item;
	}
	public void PushBack(Type item)
	{
		Add(item);
	}
	public void PushFront(Type item)
	{
		Insert(0, item);
	}
	public void Clear()
	{
		array = new Type[0];
	}
	public int Count
	{
		get
		{
			return array.Length;
		}
	}
	public Type[] ToArray()
	{
		return array;
	}
}