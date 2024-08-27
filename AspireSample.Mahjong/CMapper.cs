using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahjong;
using Mahjong.Model;
using static Mahjong.Enums;
using SharedModels;

namespace AspireSample.Mahjong
{
    public class CMapper
    {
        public Hand MapHandModelToHand(HandModel pHandModel)
        {
            CHandParser oHandParser = new CHandParser();
            Hand oHand = oHandParser.ParseHand(pHandModel.Hand);
            oHand.IsRiichi = pHandModel.IsRiichi;
            oHand.IsDoubleRiichi = pHandModel.IsDoubleRiichi;
            oHand.IsIppatsu = pHandModel.IsIppatsu;
            oHand.IsHaitei = pHandModel.IsHaitei;
            oHand.IsHoutei = pHandModel.IsHoutei;
            oHand.IsRinshan = pHandModel.IsRinshan;
            oHand.IsChankan = pHandModel.IsChankan;
            oHand.DoraCount = pHandModel.DoraCount;
            oHand.Agari = MapSharedModelAgariToAgari(pHandModel.Agari);
            oHand.WinTile = new Tile(pHandModel.WinTile);
            oHand.SeatWind = MapSharedModelWindToWind(pHandModel.SeatWind);
            oHand.RoundWind = MapSharedModelWindToWind(pHandModel.RoundWind);

            foreach(SharedModels.BlockModel oBlockModel in pHandModel.Blocks)
            {
                Block oBlock = new Block();
                if (oBlockModel.Type == SharedModels.BlockType.CHI)
                {
                    Tile t = new Tile(oBlockModel.Tile);
                    oBlock.Tiles.Add(t);
                    oBlock.Tiles.Add(new Tile(t.CompareValue + 1));
                    oBlock.Tiles.Add(new Tile(t.CompareValue + 2));
                    oBlock.Type = Mentsu.Shuntsu;
                    oBlock.IsOpen = true;
                    oBlock.KanType = KanType.None;
                }
                else if (oBlockModel.Type == SharedModels.BlockType.PON)
                {
                    oBlock.Tiles.Add(new Tile(oBlockModel.Tile));
                    oBlock.Tiles.Add(new Tile(oBlockModel.Tile));
                    oBlock.Tiles.Add(new Tile(oBlockModel.Tile));
                    oBlock.Type = Mentsu.Koutsu;
                    oBlock.IsOpen = true;
                    oBlock.KanType = KanType.None;
                }
                else if (oBlockModel.Type == SharedModels.BlockType.OPENKAN)
                {
                    oBlock.Tiles.Add(new Tile(oBlockModel.Tile));
                    oBlock.Tiles.Add(new Tile(oBlockModel.Tile));
                    oBlock.Tiles.Add(new Tile(oBlockModel.Tile));
                    oBlock.Tiles.Add(new Tile(oBlockModel.Tile));
                    oBlock.Type = Mentsu.Kantsu;
                    oBlock.IsOpen = true;
                    oBlock.KanType = KanType.Daiminkan; //Note: This doesn't matter for scoring purposes.
                }
                else if (oBlockModel.Type == SharedModels.BlockType.CLOSEDKAN)
                {
                    oBlock.Tiles.Add(new Tile(oBlockModel.Tile));
                    oBlock.Tiles.Add(new Tile(oBlockModel.Tile));
                    oBlock.Tiles.Add(new Tile(oBlockModel.Tile));
                    oBlock.Tiles.Add(new Tile(oBlockModel.Tile));
                    oBlock.Type = Mentsu.Kantsu;
                    oBlock.IsOpen = false;
                    oBlock.KanType = KanType.Ankan;
                }
                oHand.LockedBlocks.Add(oBlock);
            }
            return oHand;
        }

        public ScoreModel MapScoreToScoreModel(Score poScore)
        {
            if(poScore == null)
            {
                return null;
            }
            int nHan = poScore.Han;
            int nFu = poScore.Fu;
            int nSinglePayment = poScore.SinglePayment;
            Dictionary<string, int> oAllPayment = poScore.AllPayment;
            List<string> oYakuList = MapYakuList(poScore.YakuList);
            return new ScoreModel(nHan, nFu, nSinglePayment, oAllPayment, oYakuList);
            
        }

        public List<string> MapYakuList(List<Enums.Yaku> poYakuList)
        {
            List<string> oYakuList = new List<string>();
            foreach (Enums.Yaku yaku in poYakuList)
            {
                oYakuList.Add(MapYakuEnum(yaku).ToString());
            }
            return oYakuList;
        }

        public SharedModels.Yaku MapYakuEnum(Enums.Yaku peYaku)
        {
            switch (peYaku)
            {
                case Enums.Yaku.Riichi:
                    return SharedModels.Yaku.Riichi;
                case Enums.Yaku.Ippatsu:
                    return SharedModels.Yaku.Ippatsu;
                case Enums.Yaku.Haitei:
                    return SharedModels.Yaku.Haitei;
                case Enums.Yaku.Houtei:
                    return SharedModels.Yaku.Houtei;
                case Enums.Yaku.Rinshan:
                    return SharedModels.Yaku.Rinshan;
                case Enums.Yaku.Chankan:
                    return SharedModels.Yaku.Chankan;
                case Enums.Yaku.DoubleRiichi:
                    return SharedModels.Yaku.DoubleRiichi;
                case Enums.Yaku.Pinfu:
                    return SharedModels.Yaku.Pinfu;
                case Enums.Yaku.Tanyao:
                    return SharedModels.Yaku.Tanyao;
                case Enums.Yaku.Tsumo:
                    return SharedModels.Yaku.Tsumo;
                case Enums.Yaku.YakuhaiHaku:
                    return SharedModels.Yaku.YakuhaiHaku;
                case Enums.Yaku.YakuhaiHatsu:
                    return SharedModels.Yaku.YakuhaiHatsu;
                case Enums.Yaku.YakuhaiChun:
                    return SharedModels.Yaku.YakuhaiChun;
                case Enums.Yaku.YakuhaiTon:
                    return SharedModels.Yaku.YakuhaiTon;
                case Enums.Yaku.YakuhaiNan:
                    return SharedModels.Yaku.YakuhaiNan;
                case Enums.Yaku.YakuhaiSha:
                    return SharedModels.Yaku.YakuhaiSha;
                case Enums.Yaku.YakuhaiPei:
                    return SharedModels.Yaku.YakuhaiPei;
                case Enums.Yaku.Iipeikou:
                    return SharedModels.Yaku.Iipeikou;
                case Enums.Yaku.Ittsuu:
                    return SharedModels.Yaku.Ittsuu;
                case Enums.Yaku.SanshokuDoujun:
                    return SharedModels.Yaku.SanshokuDoujun;
                case Enums.Yaku.Chanta:
                    return SharedModels.Yaku.Chanta;
                case Enums.Yaku.Junchan:
                    return SharedModels.Yaku.Junchan;
                case Enums.Yaku.Chiitoi:
                    return SharedModels.Yaku.Chiitoi;
                case Enums.Yaku.Toitoi:
                    return SharedModels.Yaku.Toitoi;
                case Enums.Yaku.Sanankou:
                    return SharedModels.Yaku.Sanankou;
                case Enums.Yaku.Sankantsu:
                    return SharedModels.Yaku.Sankantsu;
                case Enums.Yaku.SanshoukuDoukou:
                    return SharedModels.Yaku.SanshoukuDoukou;
                case Enums.Yaku.Honroutou:
                    return SharedModels.Yaku.Honroutou;
                case Enums.Yaku.Shousangen:
                    return SharedModels.Yaku.Shousangen;
                case Enums.Yaku.Honitsu:
                    return SharedModels.Yaku.Honitsu;
                case Enums.Yaku.Chinitsu:
                    return SharedModels.Yaku.Chinitsu;
                case Enums.Yaku.Ryanpeikou:
                    return SharedModels.Yaku.Ryanpeikou;
                case Enums.Yaku.KazoeYakuman:
                    return SharedModels.Yaku.KazoeYakuman;
                case Enums.Yaku.KokushiMusou:
                    return SharedModels.Yaku.KokushiMusou;
                case Enums.Yaku.Suuankou:
                    return SharedModels.Yaku.Suuankou;
                case Enums.Yaku.Daisangen:
                    return SharedModels.Yaku.Daisangen;
                case Enums.Yaku.Shousuushii:
                    return SharedModels.Yaku.Shousuushii;
                case Enums.Yaku.Daisuushii:
                    return SharedModels.Yaku.Daisuushii;
                case Enums.Yaku.Tsuuiisou:
                    return SharedModels.Yaku.Tsuuiisou;
                case Enums.Yaku.Chinroutou:
                    return SharedModels.Yaku.Chinroutou;
                case Enums.Yaku.Ryuuiisou:
                    return SharedModels.Yaku.Ryuuiisou;
                case Enums.Yaku.ChuurenPoutou:
                    return SharedModels.Yaku.ChuurenPoutou;
                case Enums.Yaku.Suukantsu:
                    return SharedModels.Yaku.Suukantsu;
                case Enums.Yaku.Tenhou:
                    return SharedModels.Yaku.Tenhou;
                case Enums.Yaku.Chiihou:
                    return SharedModels.Yaku.Chiihou;
                case Enums.Yaku.NagashiMangan:
                    return SharedModels.Yaku.NagashiMangan;
                default: throw new NotImplementedException();
            }
        }
        public Enums.Agari MapSharedModelAgariToAgari(SharedModels.Agari peAgari)
        {
            switch (peAgari)
            {
                case SharedModels.Agari.TSUMO:
                    return Enums.Agari.Tsumo;
                case SharedModels.Agari.RON:
                    return Enums.Agari.Ron;
                default:
                    return Enums.Agari.Tsumo;
            }
        }

        public Enums.Wind MapSharedModelWindToWind(SharedModels.Wind peWind)
        {
            switch (peWind)
            {
                case SharedModels.Wind.EAST:
                    return Enums.Wind.East;
                case SharedModels.Wind.SOUTH:
                    return Enums.Wind.South;
                case SharedModels.Wind.WEST:
                    return Enums.Wind.West;
                case SharedModels.Wind.NORTH:
                    return Enums.Wind.North;
                default:
                    return Enums.Wind.East;
            }
        }
    }
}
