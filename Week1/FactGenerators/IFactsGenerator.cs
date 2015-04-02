using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining
{
    public interface IFactsGenerator<T>
    {
        List<IFact<T>> Generate(List<IFact<T>> excludedFacts, T transaction);
    }
}
