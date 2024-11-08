using System;
using Collections;
namespace Sets
{
	public class LinkedSet : LinkedHeaderCollection,Set
    {
        public new void add(object e)
        {

            if (!base.contains(e))
                base.add(e);
        }
    }
}

