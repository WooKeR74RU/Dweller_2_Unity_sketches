using System;
using System.Collections.Generic;

public class Pair<FT, ST> : IEquatable<Pair<FT, ST>>
{
	private FT first;
	private ST second;
	public Pair()
	{}
	public Pair(FT a, ST b)
	{
		first = a;
		second = b;
	}
	public bool Equals(Pair<FT, ST> other)
	{
		return EqualityComparer<FT>.Default.Equals(first, other.first) && EqualityComparer<ST>.Default.Equals(second, other.second);
	}
	public override bool Equals(object other)
	{
		if (other is Pair<FT, ST>)
			return Equals((Pair<FT, ST>)other);
		return false;
	}
	public override int GetHashCode()
	{
		return EqualityComparer<FT>.Default.GetHashCode(first) * 2 ^ EqualityComparer<ST>.Default.GetHashCode(second);
	}
}