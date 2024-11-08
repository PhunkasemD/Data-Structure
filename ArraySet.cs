using System;
using Collections;

namespace Sets
{
	public class ArraySet : ArrayCollection,Set
	{
		public ArraySet(int cap) : base(cap) { }

		public new void add(object e) {

			if (!base.contains(e)) base.add(e);
		}
	}
}

