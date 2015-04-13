using ChessDataMining;
using DataMining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessMiningApp.Models
{
    public class FactDTO
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public IFact<ChessGame> Parse()
        {
            var constructorArgs = new Object[] { Value };

            if (Type == "SimpleFact")
            {
                constructorArgs = new Object[] { Name, Value };
            }

            var handle = Activator.CreateInstance("ChessDataMining", "ChessDataMining.Facts." + Type, true, 0, null, constructorArgs, null, new Object[0]);
            return handle.Unwrap() as IFact<ChessGame>;
        }
    }
}