﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public interface ICandidateGenerator<T>
    {
        List<ItemSet<IFact<T>>> GenerateCandidateItemSets(List<ItemSet<IFact<T>>> frequentItemsets);
    }
}
