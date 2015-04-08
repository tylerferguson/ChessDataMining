using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessMiningApp.Models
{
    public class AssociationRuleDTO
    {
        public string Value { get; set; }
        public int AbsoluteSupport { get; set; }
        public Double RelativeSupport { get; set; }
        public Double Confidence { get; set; }
        public Double LiftCorrelation { get; set; }
    }
}