using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDB.Core.Data.Tables
{
    public class FindParameters
    {
        public string _nameElements {get; set;}
        public Predicate<Element>[] _conditions;
        public FindParameters(string nameElementsIn, params Predicate<Element>[] conditionsIn)
        {
            _nameElements = nameElementsIn;
            _conditions = conditionsIn;
        }
        public bool Check(Element elementIn)
        {
            if(elementIn.Name != _nameElements)
                return false;

            foreach(var condition in _conditions)
            {
                if(condition.Invoke(elementIn) == false)
                    return false;
            }
            return true;
        }
    }
}