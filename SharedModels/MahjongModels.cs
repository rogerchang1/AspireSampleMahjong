namespace SharedModels
{

    public record HandModel(string Hand, bool IsRiichi, BlockModel[] Blocks, bool IsDoubleRiichi, bool IsIppatsu, bool IsHoutei, bool IsHaitei, bool IsRinshan, bool IsChankan, Wind SeatWind, Wind RoundWind, Agari Agari, int DoraCount, string WinTile) { }

    public record BlockModel(string Tile, BlockType Type) { }

    public record ScoreModel(int Han, int Fu, int SinglePayment, Dictionary<string, int> AllPayment, List<string> YakuList, List<string> HanBreakdown, List<string> FuBreakdown) { }

    public enum BlockType
    {
        UNKNOWN,
        PON,
        CHI,
        OPENKAN,
        CLOSEDKAN,
    }

    public enum Wind
    {
        EAST,
        SOUTH,
        WEST,
        NORTH,
    }

    public enum Agari
    {
        TSUMO,
        RON,
    }

    public enum Yaku
    {
        Riichi,
        Ippatsu,
        Haitei,
        Houtei,
        Rinshan,
        Chankan,
        DoubleRiichi,
        Pinfu,
        Tanyao,
        Tsumo,
        YakuhaiHaku,
        YakuhaiHatsu,
        YakuhaiChun,
        YakuhaiTon,
        YakuhaiNan,
        YakuhaiSha,
        YakuhaiPei,
        Iipeikou,
        Ittsuu,
        SanshokuDoujun,
        Chanta,
        Junchan,
        Chiitoi,
        Toitoi,
        Sanankou,
        Sankantsu,
        SanshoukuDoukou,
        Honroutou,
        Shousangen,
        Honitsu,
        Chinitsu,
        Ryanpeikou,
        KazoeYakuman,
        KokushiMusou,
        Suuankou,
        Daisangen,
        Shousuushii,
        Daisuushii,
        Tsuuiisou,
        Chinroutou,
        Ryuuiisou,
        ChuurenPoutou,
        Suukantsu,
        Tenhou,
        Chiihou,
        NagashiMangan
    }

}