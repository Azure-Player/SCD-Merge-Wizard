using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScdMergeWizard.Database
{
    public class MyDbObject
    {
        public string Name;
        public EDbObjectType Type;
    }

    public enum EDbObjectType
    {
        Table,
        View,
        Synonym
    }
}
