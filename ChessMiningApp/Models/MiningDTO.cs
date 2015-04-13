using ChessDataMining;
using DataMining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessMiningApp.Models
{
    public class MiningDTO
    {
        public List<ChessGame> Games;
        public Double Minsup;
        public Double Minconf;
        public List<FactDTO> projectionFacts;
        public List<FactDTO> targetFacts;
    }
}