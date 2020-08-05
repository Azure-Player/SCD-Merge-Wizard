using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScdMergeWizard.Database
{
    public class MyDbObject : IEquatable<MyDbObject>, IComparable<MyDbObject>
    {
        public string Name;
        public EDbObjectType Type;

        public int CompareTo(MyDbObject other)
        {
            if (other == null)
                return 1;
            else
                return (this.Name.CompareTo(other.Name));
        }

        public bool Equals(MyDbObject other)
        {
            if (other == null)
                return false;

            return (this.Name.Equals(other.Name));
        }
    }

    public enum EDbObjectType
    {
        Table,
        View,
        Synonym
    }
}
