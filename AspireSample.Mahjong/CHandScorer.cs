using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahjong.Model;
using Mahjong;
using SharedModels;

namespace AspireSample.Mahjong
{
    public  class CHandScorer
    {
        public ScoreModel? ScoreHand(HandModel poHandModel) {
            CMapper oMapper = new CMapper();
            Hand oHand = oMapper.MapHandModelToHand(poHandModel);
            CScoreEvaluator oEvaluator = new CScoreEvaluator();
            return oMapper.MapScoreToScoreModel(oEvaluator.EvaluateScore(oHand));
        }
    }
}
