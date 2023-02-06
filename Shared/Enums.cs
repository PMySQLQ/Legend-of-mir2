using System;
using System.Drawing;
using System.IO;
using System.Threading;

public enum MouseCursor : byte
{
    None,
    Default,
    Attack,
    AttackRed,
    NPCTalk,
    TextPrompt,
    Trash,
    Upgrade
}

public enum PanelType : byte
{
    Buy,
    BuySub,
    Craft,

    Sell,
    Repair,
    SpecialRepair,
    Consign,
    Refine,
    CheckRefine,
    Disassemble,
    Downgrade,
    Reset,
    CollectRefine,
    ReplaceWedRing,
}

public enum MarketItemType : byte
{
    Consign,
    Auction,
    GameShop
}

public enum MarketPanelType : byte
{
    Market,
    Consign,
    Auction,
    GameShop
}

public enum BlendMode : sbyte
{
    NONE = -1,
    NORMAL = 0,
    LIGHT = 1,
    LIGHTINV = 2,
    INVNORMAL = 3,
    INVLIGHT = 4,
    INVLIGHTINV = 5,
    INVCOLOR = 6,
    INVBACKGROUND = 7
}

public enum DamageType : byte
{
    Hit = 0,
    Miss = 1,
    Critical = 2,
    Heal = 3,
    Mana = 4,
    MagicalHit = 5,
    GreenPoison = 6,
    Bleeding = 7,
    Burning = 8,
    Block = 9,
    Resist = 10,
}

[Flags]
public enum GMOptions : byte
{
    None = 0,
    GameMaster = 0x0001,
    Observer = 0x0002,
    Superman = 0x0004
}

public enum AwakeType : byte
{
    None = 0,
    DC,
    MC,
    SC,
    AC,
    MAC,
    HPMP,
}

[Flags]
public enum LevelEffects : byte
{
    None = 0,
    Mist = 0x0001,
    RedDragon = 0x0002,
    BlueDragon = 0x0004
}

public enum OutputMessageType : byte
{
    Normal,
    Quest,
    Guild
}

public enum ItemGrade : byte
{
    无 = 0,
    普通 = 1,
    稀有 = 2,
    传奇 = 3,
    史诗 = 4,
    英雄 = 5,
}

public enum WearType : byte
{
    All = 0,
    Hero = 1,
    Player = 2,
}
public enum RefinedValue : byte
{
    None = 0,
    DC = 1,
    MC = 2,
    SC = 3,
}

public enum QuestType : byte
{
    一般 = 0,
    每日 = 1,
    重复 = 2,
    Story = 3
}

public enum QuestIcon : byte
{
    None = 0,
    QuestionWhite = 1,
    ExclamationYellow = 2,
    QuestionYellow = 3,
    ExclamationBlue = 5,
    QuestionBlue = 6,
    ExclamationGreen = 52,
    QuestionGreen = 53
}

public enum QuestState : byte
{
    Add,
    Update,
    Remove
}

public enum QuestAction : byte
{
    TimeExpired
}

public enum DefaultNPCType : byte
{
    Login,
    LevelUp,
    UseItem,
    MapCoord,
    MapEnter,
    Die,
    Trigger,
    CustomCommand,
    OnAcceptQuest,
    OnFinishQuest,
    Daily,
    Client
}

public enum IntelligentCreatureType : byte
{
    None = 99,
    BabyPig = 0,
    Chick = 1,
    Kitten = 2,
    BabySkeleton = 3,
    Baekdon = 4,
    Wimaen = 5,
    BlackKitten = 6,
    BabyDragon = 7,
    OlympicFlame = 8,
    BabySnowMan = 9,
    Frog = 10,
    BabyMonkey = 11,
    AngryBird = 12,
    Foxey = 13,
    MedicalRat = 14,
}

//2 blank mob files
public enum Monster : ushort
{
    Guard = 0,
    TaoistGuard = 1,
    Guard2 = 2,
    Hen = 3,
    Deer = 4,
    Scarecrow = 5,
    HookingCat = 6,
    RakingCat = 7,
    Yob = 8,
    Oma = 9,
    CannibalPlant = 10,
    ForestYeti = 11,
    SpittingSpider = 12,
    ChestnutTree = 13,
    EbonyTree = 14,
    LargeMushroom = 15,
    CherryTree = 16,
    OmaFighter = 17,
    OmaWarrior = 18,
    CaveBat = 19,
    CaveMaggot = 20,
    Scorpion = 21,
    Skeleton = 22,
    BoneFighter = 23,
    AxeSkeleton = 24,
    BoneWarrior = 25,
    BoneElite = 26,
    Dung = 27,
    Dark = 28,
    WoomaSoldier = 29,
    WoomaFighter = 30,
    WoomaWarrior = 31,
    FlamingWooma = 32,
    WoomaGuardian = 33,
    WoomaTaurus = 34, //BOSS
    WhimperingBee = 35,
    GiantWorm = 36,
    Centipede = 37,
    BlackMaggot = 38,
    Tongs = 39,
    EvilTongs = 40,
    EvilCentipede = 41,
    BugBat = 42,
    BugBatMaggot = 43,
    WedgeMoth = 44,
    RedBoar = 45,
    BlackBoar = 46,
    SnakeScorpion = 47,
    WhiteBoar = 48,
    EvilSnake = 49,
    BombSpider = 50,
    RootSpider = 51,
    SpiderBat = 52,
    VenomSpider = 53,
    GangSpider = 54,
    GreatSpider = 55,
    LureSpider = 56,
    BigApe = 57,
    EvilApe = 58,
    GrayEvilApe = 59,
    RedEvilApe = 60,
    CrystalSpider = 61,
    RedMoonEvil = 62,
    BigRat = 63,
    ZumaArcher = 64,
    ZumaStatue = 65,
    ZumaGuardian = 66,
    RedThunderZuma = 67,
    ZumaTaurus = 68, //BOSS
    DigOutZombie = 69,
    ClZombie = 70,
    NdZombie = 71,
    CrawlerZombie = 72,
    ShamanZombie = 73,
    Ghoul = 74,
    KingScorpion = 75,
    KingHog = 76,
    DarkDevil = 77,
    BoneFamiliar = 78,
    Shinsu = 79,
    Shinsu1 = 80,
    SpiderFrog = 81,
    HoroBlaster = 82,
    BlueHoroBlaster = 83,
    KekTal = 84,
    VioletKekTal = 85,
    Khazard = 86,
    RoninGhoul = 87,
    ToxicGhoul = 88,
    BoneCaptain = 89,
    BoneSpearman = 90,
    BoneBlademan = 91,
    BoneArcher = 92,
    BoneLord = 93, //BOSS
    Minotaur = 94,
    IceMinotaur = 95,
    ElectricMinotaur = 96,
    WindMinotaur = 97,
    FireMinotaur = 98,
    RightGuard = 99,
    LeftGuard = 100,
    MinotaurKing = 101, //BOSS
    FrostTiger = 102,
    Sheep = 103,
    Wolf = 104,
    ShellNipper = 105,
    Keratoid = 106,
    GiantKeratoid = 107,
    SkyStinger = 108,
    SandWorm = 109,
    VisceralWorm = 110,
    RedSnake = 111,
    TigerSnake = 112,
    Yimoogi = 113,
    GiantWhiteSnake = 114,
    BlueSnake = 115,
    YellowSnake = 116,
    HolyDeva = 117,
    AxeOma = 118,
    SwordOma = 119,
    CrossbowOma = 120,
    WingedOma = 121,
    FlailOma = 122,
    OmaGuard = 123,
    YinDevilNode = 124,
    YangDevilNode = 125,
    OmaKing = 126, //BOSS
    BlackFoxman = 127,
    RedFoxman = 128,
    WhiteFoxman = 129,
    TrapRock = 130,
    GuardianRock = 131,
    ThunderElement = 132,
    CloudElement = 133,
    GreatFoxSpirit = 134, //BOSS
    HedgeKekTal = 135,
    BigHedgeKekTal = 136,
    RedFrogSpider = 137,
    BrownFrogSpider = 138,
    ArcherGuard = 139,
    KatanaGuard = 140,
    ArcherGuard2 = 141,
    Pig = 142,
    Bull = 143,
    Bush = 144,
    ChristmasTree = 145,
    HighAssassin = 146,
    DarkDustPile = 147,
    DarkBrownWolf = 148,
    Football = 149,
    GingerBreadman = 150,
    HalloweenScythe = 151,
    GhastlyLeecher = 152,
    CyanoGhast = 153,
    MutatedManworm = 154,
    CrazyManworm = 155,
    MudPile = 156,
    TailedLion = 157,
    Behemoth = 158, //BOSS
    DarkDevourer = 159,
    PoisonHugger = 160,
    Hugger = 161,
    MutatedHugger = 162,
    DreamDevourer = 163,
    Treasurebox = 164,
    SnowPile = 165,
    Snowman = 166,
    SnowTree = 167,
    GiantEgg = 168,
    RedTurtle = 169,
    GreenTurtle = 170,
    BlueTurtle = 171,
    Catapult1 = 172, //SPECIAL TODO
    Catapult2 = 173, //SPECIAL TODO
    OldSpittingSpider = 174,
    SiegeRepairman = 175, //SPECIAL TODO
    BlueSanta = 176,
    BattleStandard = 177,
    Blank1 = 178,
    RedYimoogi = 179,
    LionRiderMale = 180, //Not Monster - Skin / Transform
    LionRiderFemale = 181, //Not Monster - Skin / Transform
    Tornado = 182,
    FlameTiger = 183,
    WingedTigerLord = 184, //BOSS
    TowerTurtle = 185,
    FinialTurtle = 186,
    TurtleKing = 187, //BOSS
    DarkTurtle = 188,
    LightTurtle = 189,  
    DarkSwordOma = 190,
    DarkAxeOma = 191,
    DarkCrossbowOma = 192,
    DarkWingedOma = 193,
    BoneWhoo = 194,
    DarkSpider = 195, //AI 8
    ViscusWorm = 196,
    ViscusCrawler = 197,
    CrawlerLave = 198,
    DarkYob = 199,
    FlamingMutant = 200,
    StoningStatue = 201, //BOSS
    FlyingStatue = 202,
    ValeBat = 203,
    Weaver = 204,
    VenomWeaver = 205,
    CrackingWeaver = 206,
    ArmingWeaver = 207,
    CrystalWeaver = 208,
    FrozenZumaStatue = 209,
    FrozenZumaGuardian = 210,
    FrozenRedZuma = 211,
    GreaterWeaver = 212,
    SpiderWarrior = 213,
    SpiderBarbarian = 214,
    HellSlasher = 215,
    HellPirate = 216,
    HellCannibal = 217,
    HellKeeper = 218, //BOSS
    HellBolt = 219, 
    WitchDoctor = 220,
    ManectricHammer = 221,
    ManectricClub = 222,
    ManectricClaw = 223,
    ManectricStaff = 224,
    NamelessGhost = 225,
    DarkGhost = 226,
    ChaosGhost = 227,
    ManectricBlest = 228,
    ManectricKing = 229,
    Blank2 = 230,
    IcePillar = 231,
    FrostYeti = 232,
    ManectricSlave = 233,
    TrollHammer = 234,
    TrollBomber = 235,
    TrollStoner = 236,
    TrollKing = 237, //BOSS
    FlameSpear = 238,
    FlameMage = 239,
    FlameScythe = 240,
    FlameAssassin = 241,
    FlameQueen = 242, //BOSS
    HellKnight1 = 243,
    HellKnight2 = 244,
    HellKnight3 = 245,
    HellKnight4 = 246,
    HellLord = 247, //BOSS
    WaterGuard = 248,
    IceGuard = 249,
    ElementGuard = 250,
    DemonGuard = 251,
    KingGuard = 252,
    Snake10 = 253,
    Snake11 = 254,
    Snake12 = 255,
    Snake13 = 256,
    Snake14 = 257,
    Snake15 = 258,
    Snake16 = 259,
    Snake17 = 260,
    DeathCrawler = 261, 
    BurningZombie = 262, 
    MudZombie = 263, 
    FrozenZombie = 264, 
    UndeadWolf = 265,
    DemonWolf = 266, 
    WhiteMammoth = 267, 
    DarkBeast = 268, 
    LightBeast = 269,//AI 112
    BloodBaboon = 270, //AI 112
    HardenRhino = 271,
    AncientBringer = 272, 
    FightingCat = 273,
    FireCat = 274, //AI 44
    CatWidow = 275, //AI 112
    StainHammerCat = 276, 
    BlackHammerCat = 277, 
    StrayCat = 278, 
    CatShaman = 279, 
    Jar1 = 280,
    Jar2 = 281,
    SeedingsGeneral = 282, 
    RestlessJar = 283,
    GeneralMeowMeow = 284, //BOSS
    Bunny = 285,
    Tucson = 286,
    TucsonFighter = 287, //AI 44
    TucsonMage = 288, 
    TucsonWarrior = 289, 
    Armadillo = 290, 
    ArmadilloElder = 291, 
    TucsonEgg = 292, //EFFECT 0/1
    PlaguedTucson = 293,
    SandSnail = 294, 
    CannibalTentacles = 295, 
    TucsonGeneral = 296, //BOSS
    GasToad = 297, 
    Mantis = 298, 
    SwampWarrior = 299, 

    AssassinBird = 300, 
    RhinoWarrior = 301,
    RhinoPriest = 302, 
    ElephantMan = 303, 
    StoneGolem = 304,
    EarthGolem = 305,
    TreeGuardian = 306, 
    TreeQueen = 307,
    PeacockSpider = 308,
    DarkBaboon = 309, //AI 112
    TwinHeadBeast = 310, //AI 112
    OmaCannibal = 311, 
    OmaBlest = 312, 
    OmaSlasher = 313, 
    OmaAssassin = 314, 
    OmaMage = 315, 
    OmaWitchDoctor = 316,
    LightningBead = 317, //Effect 0, AI 149
    HealingBead = 318, //Effect 1, AI 149
    PowerUpBead = 319, //Effect 2, AI 14
    DarkOmaKing = 320, //BOSS
    CaveStatue = 321,
    Mandrill = 322,
    PlagueCrab = 323,
    CreeperPlant = 324,
    FloatingWraith = 325, //AI 8
    ArmedPlant = 326,
    AvengerPlant = 327,
    Nadz = 328,
    AvengingSpirit = 329,
    AvengingWarrior = 330,
    AxePlant = 331,
    WoodBox = 332,
    ClawBeast = 333, //AI 8
    DarkCaptain = 334, //BOSS
    SackWarrior = 335,
    WereTiger = 336, //AI 112
    KingHydrax = 337,
    Hydrax = 338,
    HornedMage = 339,
    BlueSoul = 340,
    HornedArcher = 341,
    ColdArcher = 342,
    HornedWarrior = 343,
    FloatingRock = 344,
    ScalyBeast = 345,
    HornedSorceror = 346,
    BoulderSpirit = 347,
    HornedCommander = 348, //BOSS

    MoonStone = 349,
    SunStone = 350,
    LightningStone = 351,
    Turtlegrass = 352,
    ManTree = 353,
    Bear = 354,  //Effect 1, AI 112
    Leopard = 355,
    ChieftainArcher = 356,
    ChieftainSword = 357, //BOSS TODO
    StoningSpider = 358, //Archer Spell mob (not yet coded)
    VampireSpider = 359, //Archer Spell mob
    SpittingToad = 360, //Archer Spell mob
    SnakeTotem = 361, //Archer Spell mob
    CharmedSnake = 362, //Archer Spell mob
    FrozenSoldier = 363,
    FrozenFighter = 364, //AI 44
    FrozenArcher = 365, //AI 8
    FrozenKnight = 366,
    FrozenGolem = 367,
    IcePhantom = 368, //TODO - AI needs revisiting (blue explosion and snakes)
    SnowWolf = 369,
    SnowWolfKing = 370, //BOSS
    WaterDragon = 371,
    BlackTortoise = 372,
    Manticore = 373, //TODO
    DragonWarrior = 374, //Done (DG)
    DragonArcher = 375, //TODO - Wind Arrow spell
    Kirin = 376, // Done (jxtulong)
    Guard3 = 377,
    ArcherGuard3 = 378,
    Bunny2 = 379,
    FrozenMiner = 380, // Done (jxtulong)
    FrozenAxeman = 381, // Done (jxtulong)
    FrozenMagician = 382, // Done (jxtulong)
    SnowYeti = 383, // Done (jxtulong)
    IceCrystalSoldier = 384, // Done (jxtulong)
    DarkWraith = 385, // Done (jxtulong)
    DarkSpirit = 386, // Use AI 8 (AxeSkeleton)
    CrystalBeast = 387,
    RedOrb = 388,
    BlueOrb = 389,
    YellowOrb = 390,
    GreenOrb = 391,
    WhiteOrb = 392,
    FatalLotus = 393,
    AntCommander = 394,
    CargoBoxwithlogo = 395, // Done - Use CargoBox AI.
    Doe = 396, // TELEPORT = EFFECT 9
    Reindeer = 397, //frames not added
    AngryReindeer = 398,
    CargoBox = 399, // Done - Basically a Pinata.

    Ram1 = 400,
    Ram2 = 401,
    Kite = 402,
    PurpleFaeFlower = 403,
    Furball = 404,
    GlacierSnail = 405,
    FurbolgWarrior = 406,
    FurbolgArcher = 407,
    FurbolgCommander = 408,
    RedFaeFlower = 409,
    FurbolgGuard = 410,
    GlacierBeast = 411,
    GlacierWarrior = 412,
    ShardGuardian = 413,
    WarriorScroll = 414, // Use AI "HoodedSummonerScrolls" - Info.Effect = 0
    TaoistScroll = 415, // Use AI "HoodedSummonerScrolls" - Info.Effect = 1
    WizardScroll = 416, // Use AI "HoodedSummonerScrolls" - Info.Effect = 2
    AssassinScroll = 417, // Use AI "HoodedSummonerScrolls" - Info.Effect = 3
    HoodedSummoner = 418, //Summons Scrolls
    HoodedIceMage = 419,
    HoodedPriest = 420,
    ShardMaiden = 421,
    KingKong = 422,
    WarBear = 423,
    ReaperPriest = 424,
    ReaperWizard = 425,
    ReaperAssassin = 426,
    LivingVines = 427,
    BlueMonk = 428,
    MutantBeserker = 429,
    MutantGuardian = 430,
    MutantHighPriest = 431,
    MysteriousMage = 432,
    FeatheredWolf = 433,
    MysteriousAssassin = 434,
    MysteriousMonk = 435,
    ManEatingPlant = 436,
    HammerDwarf = 437,
    ArcherDwarf = 438,
    NobleWarrior = 439,
    NobleArcher = 440,
    NoblePriest = 441,
    NobleAssassin = 442,
    Swain = 443,
    RedMutantPlant = 444,
    BlueMutantPlant = 445,
    UndeadHammerDwarf = 446,
    UndeadDwarfArcher = 447,
    AncientStoneGolem = 448,
    Serpentirian = 449,

    Butcher = 450,
    Riklebites = 451,
    FeralTundraFurbolg = 452,
    FeralFlameFurbolg = 453,
    ArcaneTotem = 454,
    SpectralWraith = 455,
    BabyMagmaDragon = 456,
    BloodLord = 457,
    SerpentLord = 458,
    MirEmperor = 459,
    MutantManEatingPlant = 460,
    MutantWarg = 461,
    GrassElemental = 462,
    RockElemental = 463,

    //Special
    EvilMir = 900,
    EvilMirBody = 901,
    DragonStatue = 902,
    HellBomb1 = 903,
    HellBomb2 = 904,
    HellBomb3 = 905,

    //Siege
    Catapult = 940,
    ChariotBallista = 941,
    Ballista = 942,
    Trebuchet = 943,
    CanonTrebuchet = 944,

    //Gates
    SabukGate = 950,
    PalaceWallLeft = 951,
    PalaceWall1 = 952,
    PalaceWall2 = 953,
    GiGateSouth = 954,
    GiGateEast = 955,
    GiGateWest = 956,
    SSabukWall1 = 957,
    SSabukWall2 = 958,
    SSabukWall3 = 959,
    NammandGate1 = 960, //Not Coded
    NammandGate2 = 961, //Not Coded
    SabukWallSection = 962, //Not Coded
    NammandWallSection = 963, //Not Coded
    FrozenDoor = 964, //Not Coded

    //Flags 1000 ~ 1100

    //Creatures
    BabyPig = 10000,//Permanent
    Chick = 10001,//Special
    Kitten = 10002,//Permanent
    BabySkeleton = 10003,//Special
    Baekdon = 10004,//Special
    Wimaen = 10005,//Event
    BlackKitten = 10006,//unknown
    BabyDragon = 10007,//unknown
    OlympicFlame = 10008,//unknown
    BabySnowMan = 10009,//unknown
    Frog = 10010,//unknown
    BabyMonkey = 10011,//unknown
    AngryBird = 10012,
    Foxey = 10013,
    MedicalRat = 10014,
}

public enum MirAction : byte
{
    Standing,
    Walking,
    Running,
    Pushed,
    DashL,
    DashR,
    DashFail,
    Stance,
    Stance2,
    Attack1,
    Attack2,
    Attack3,
    Attack4,
    Attack5,
    AttackRange1,
    AttackRange2,
    AttackRange3,
    Special,
    Struck,
    Harvest,
    Spell,
    Die,
    Dead,
    Skeleton,
    Show,
    Hide,
    Stoned,
    Appear,
    Revive,
    SitDown,
    Mine,
    Sneek,
    DashAttack,
    Lunge,

    WalkingBow,
    RunningBow,
    Jump,

    MountStanding,
    MountWalking,
    MountRunning,
    MountStruck,
    MountAttack,

    FishingCast,
    FishingWait,
    FishingReel
}

public enum CellAttribute : byte
{
    Walk = 0,
    HighWall = 1,
    LowWall = 2,
}

public enum LightSetting : byte
{
    Normal = 0,
    黎明 = 1,
    白天 = 2,
    半晚 = 3,
    夜晚 = 4
}

public enum MirGender : byte
{
    Male = 0,
    Female = 1
}

public enum MirClass : byte
{

    战士 = 0,
    法师 = 1,
    道士 = 2,
    刺客 = 3,
    弓手 = 4,
    碧血战士 = 5,
    虹玄法师 = 6,
    翊仙道士 = 7,
    飞燕刺客 = 8,
    暗鬼弓手 = 9,
}

public enum MirDirection : byte
{
    Up = 0,
    UpRight = 1,
    Right = 2,
    DownRight = 3,
    Down = 4,
    DownLeft = 5,
    Left = 6,
    UpLeft = 7
}

public enum ObjectType : byte
{
    None = 0,
    Player = 1,
    Item = 2,
    Merchant = 3,
    Spell = 4,
    Monster = 5,
    Deco = 6,
    Creature = 7,
    Hero = 8
}

public enum ChatType : byte
{
    Normal = 0,
    Shout = 1,
    System = 2,
    Hint = 3,
    Announcement = 4,
    Group = 5,
    WhisperIn = 6,
    WhisperOut = 7,
    Guild = 8,
    Trainer = 9,
    LevelUp = 10,
    System2 = 11,
    Relationship = 12,
    Mentor = 13,
    Shout2 = 14,
    Shout3 = 15,
    LineMessage = 16,
}

public enum ItemType : byte
{
    无 = 0,
    武器 = 1,
    护甲 = 2,
    头盔 = 4,
    项链 = 5,
    手镯 = 6,
    戒指 = 7,
    护身符 = 8,
    腰带 = 9,
    鞋 = 10,
    守护石 = 11,
    火把 = 12,
    药剂 = 13,
    矿物 = 14,
    肉 = 15,
    合成材料 = 16,
    卷轴 = 17,
    宝玉 = 18,
    坐骑 = 19,
    书籍 = 20,
    脚本 = 21,
    缰绳 = 22,
    铃铛 = 23,
    马鞍 = 24,
    色带 = 25,
    面具 = 26,
    食物 = 27,
    鱼钩 = 28,
    浮标 = 29,
    鱼饵 = 30,
    探测器 = 31,
    绕线器 = 32,
    鱼 = 33,
    任务 = 34,
    觉醒 = 35,
    宠物 = 36,
    变身 = 37,
    装饰 = 38,
    插座 = 39,
    MonsterSpawn = 40,
    SiegeAmmo = 41, //TODO
    SealedHero = 42
}

public enum MirGridType : byte
{
    None = 0,
    Inventory = 1,
    Equipment = 2,
    Trade = 3,
    Storage = 4,
    BuyBack = 5,
    DropPanel = 6,
    Inspect = 7,
    TrustMerchant = 8,
    GuildStorage = 9,
    GuestTrade = 10,
    Mount = 11,
    Fishing = 12,
    QuestInventory = 13,
    AwakenItem = 14,
    Mail = 15,
    Refine = 16,
    Renting = 17,
    GuestRenting = 18,
    Craft = 19,
    Socket = 20,
    HeroEquipment = 21, 
    HeroInventory = 22,
    HeroHPItem = 23,
    HeroMPItem = 24
}

public enum EquipmentSlot : byte
{
    Weapon = 0,
    Armour = 1,
    Helmet = 2,
    Torch = 3,
    Necklace = 4,
    BraceletL = 5,
    BraceletR = 6,
    RingL = 7,
    RingR = 8,
    Amulet = 9,
    Belt = 10,
    Boots = 11,
    Stone = 12,
    Mount = 13
}

public enum MountSlot : byte
{
    Reins = 0,
    Bells = 1,
    Saddle = 2,
    Ribbon = 3,
    Mask = 4
}

public enum FishingSlot : byte
{
    Hook = 0,
    Float = 1,
    Bait = 2,
    Finder = 3,
    Reel = 4
}

public enum AttackMode : byte
{
    Peace = 0,
    Group = 1,
    Guild = 2,
    EnemyGuild = 3,
    RedBrown = 4,
    All = 5
}

public enum PetMode : byte
{
    Both = 0,
    MoveOnly = 1,
    AttackOnly = 2,
    None = 3,
}

[Flags]
public enum PoisonType : ushort
{
    None = 0,
    Green = 1,
    Red = 2,
    Slow = 4,
    Frozen = 8,
    Stun = 16,
    Paralysis = 32,
    DelayedExplosion = 64,
    Bleeding = 128,
    LRParalysis = 256,
    Blindness = 512,
    Dazed = 1024
}

[Flags]

public enum BindMode : short
{
    None = 0,
    DontDeathdrop = 1,//0x0001
    DontDrop = 2,//0x0002
    DontSell = 4,//0x0004
    DontStore = 8,//0x0008
    DontTrade = 16,//0x0010
    DontRepair = 32,//0x0020
    DontUpgrade = 64,//0x0040
    DestroyOnDrop = 128,//0x0080
    BreakOnDeath = 256,//0x0100
    BindOnEquip = 512,//0x0200
    NoSRepair = 1024,//0x0400
    NoWeddingRing = 2048,//0x0800
    UnableToRent = 4096,
    UnableToDisassemble = 8192,
    NoMail = 16384,
    NoHero = -32768
}

[Flags]
public enum SpecialItemMode : short
{
    None = 0,
    Paralize = 0x0001,
    Teleport = 0x0002,
    ClearRing = 0x0004,
    Protection = 0x0008,
    Revival = 0x0010,
    Muscle = 0x0020,
    Flame = 0x0040,
    Healing = 0x0080,
    Probe = 0x0100,
    Skill = 0x0200,
    NoDuraLoss = 0x0400,
    Blink = 0x800,
}

[Flags]
public enum RequiredClass : ushort
{
    战士 = 1 << 0,
    法师 = 1 << 1,
    道士 = 1 << 2,
    刺客 = 1 << 3,
    弓手 = 1 << 4,

    战法道 = 战士 | 法师 | 道士,

    非仙全职业 = 战士 | 法师 | 道士 | 刺客 | 弓手,

    普通去弓全职业 = 战士 | 法师 | 道士 | 刺客,

    碧血战士 = 1 << 5,
    虹玄法师 = 1 << 6,
    翊仙道士 = 1 << 7,
    飞燕刺客 = 1 << 8,
    暗鬼弓手 = 1 << 9,

    飞升战法道 = 碧血战士 | 虹玄法师 | 翊仙道士,

    飞升全职业 = 碧血战士 | 虹玄法师 | 翊仙道士 | 飞燕刺客 | 暗鬼弓手,

    飞升去弓全职业 = 碧血战士 | 虹玄法师 | 翊仙道士 | 飞燕刺客,


    战仙 = 战士 | 碧血战士,
    法仙 = 法师 | 虹玄法师,
    道仙 = 道士 | 翊仙道士,
    刺仙 = 刺客 | 飞燕刺客,
    弓仙 = 弓手 | 暗鬼弓手,

    所有战法道 = 碧血战士 | 虹玄法师 | 翊仙道士 | 战士 | 法师 | 道士,


    所有去弓 = 普通去弓全职业 | 飞升去弓全职业,

    None = 非仙全职业 | 飞升全职业

}

[Flags]
public enum RequiredGender : byte
{
    男 = 1,
    女 = 2,
    None = 男 | 女
}

public enum RequiredType : byte
{
    Level = 0,
    MaxAC = 1,
    MaxMAC = 2,
    MaxDC = 3,
    MaxMC = 4,
    MaxSC = 5,
    MaxLevel = 6,
    MinAC = 7,
    MinMAC = 8,
    MinDC = 9,
    MinMC = 10,
    MinSC = 11,
}

public enum ItemSet : byte
{
    None = 0,
    Spirit = 1,
    Recall = 2,
    RedOrchid = 3,
    RedFlower = 4,
    Smash = 5,
    HwanDevil = 6,
    Purity = 7,
    FiveString = 8,
    Mundane = 9,
    NokChi = 10,
    TaoProtect = 11,
    Mir = 12,
    Bone = 13,
    Bug = 14,
    WhiteGold = 15,
    WhiteGoldH = 16,
    RedJade = 17,
    RedJadeH = 18,
    Nephrite = 19,
    NephriteH = 20,
    Whisker1 = 21,
    Whisker2 = 22,
    Whisker3 = 23,
    Whisker4 = 24,
    Whisker5 = 25,
    Hyeolryong = 26,
    Monitor = 27,
    Oppressive = 28,
    Paeok = 29,
    Sulgwan = 30,
    BlueFrost = 31,
    DarkGhost = 38,
    BlueFrostH = 39
}

public enum Spell : byte
{
    None = 0,

    //Warrior
    基本剑术 = 1,
    攻杀剑术 = 2,
    刺杀剑术 = 3,
    半月弯刀 = 4,
    野蛮冲撞 = 5,
    双龙斩 = 6,
    捕绳剑 = 7,
    烈火剑法 = 8,
    狮子吼 = 9,
    狂风斩 = 10,
    空破闪 = 11,
    护身气幕 = 12,
    剑气爆 = 13,
    天务 = 14,
    日闪 = 15,
    血龙剑法 = 16,
    金刚不坏 = 17,


    //Wizard
    火球术 = 31,
    抗拒火环 = 32,
    诱惑之光 = 33,
    大火球 = 34,
    地狱火 = 35,
    雷电术 = 36,
    瞬息移动 = 37,
    爆裂火焰 = 38,
    火墙 = 39,
    疾光电影 = 40,
    寒冰掌 = 41,
    地狱雷光 = 42,
    魔法盾 = 43,
    圣言术 = 44,
    噬血术 = 45,
    冰咆哮 = 46,
    灭天火 = 47,
    分身术 = 48,
    火龙气焰 = 49,
    天霜冰环 = 50,
    深延术 = 51,
    天上秘术 = 52,
    冰焰术 = 53,
    雷仙风 = 54,
    StormEscape = 55,

    //Taoist
    治愈术 = 61,
    精神力战法 = 62,
    施毒术 = 63,
    灵魂火符 = 64,
    召唤骷髅 = 65,
    隐身术 = 67,
    集体隐身术 = 68,
    幽灵盾 = 69,
    心灵启示 = 70,
    神圣战甲术 = 71,
    气功波 = 72,
    困魔咒 = 73,
    净化术 = 74,
    群体治疗术 = 75,
    迷魂术 = 76,
    无极真气 = 77,
    召唤神兽 = 78,
    苏生术 = 79,
    精魂召唤术 = 80,
    诅咒术 = 81,
    烦恼 = 82,
    毒雾 = 83,
    先天气功 = 84,
    血龙水 = 85,
    阴阳五行阵 = 86,

    //Assassin
    绝命剑法 = 91,
    风剑术 = 92,
    体迅风 = 93,
    拔刀术 = 94,
    风身术 = 95,
    迁移剑 = 96,
    烈风击 = 97,
    捕缚术 = 98,
    猛毒剑气 = 99,
    月影术 = 100,
    吸气 = 101,
    轻身步 = 102,
    烈火身 = 103,
    血风击 = 104,
    月华乱舞 = 105,
    月影雾 = 106,

    //Archer
    必中闪 = 121,
    天日闪 = 122,
    无我闪 = 123,
    爆阱 = 124,
    爆闪 = 125,
    气功术 = 126,
    风弹步 = 127,
    万斤闪 = 128,
    气流术 = 129,
    地柱钉 = 130,
    金刚术 = 131,
    吸血地精 = 132,
    吸血地闪 = 133,
    痹魔阱 = 134,
    毒魔闪 = 135,
    邪爆闪 = 136,
    蛇柱阱 = 137,
    血龙闪 = 138,
    OneWithNature = 139,
    BindingShot = 140,
    MentalState = 141,

    //Custom
    Blink = 151,
    Portal = 152,
    BattleCry = 153,
    FireBounce = 154,
    MeteorShower = 155,

    //Map Events
    DigOutZombie = 200,
    Rubble = 201,
    MapLightning = 202,
    MapLava = 203,
    MapQuake1 = 204,
    MapQuake2 = 205,
    DigOutArmadillo = 206,
    GeneralMeowMeowThunder = 207,
    StoneGolemQuake = 208,
    EarthGolemPile = 209,
    TreeQueenRoot = 210,
    TreeQueenMassRoots = 211,
    TreeQueenGroundRoots = 212,
    TucsonGeneralRock = 213,
    FlyingStatueIceTornado = 214,
    DarkOmaKingNuke = 215,
    HornedSorcererDustTornado = 216,
    HornedCommanderRockFall = 217,
    HornedCommanderRockSpike = 218
}

public enum SpellEffect : byte
{
    None,
    FatalSword,
    Teleport,
    Healing,
    RedMoonEvil,
    TwinDrakeBlade,
    MagicShieldUp,
    MagicShieldDown,
    GreatFoxSpirit,
    Entrapment,
    Reflect,
    Critical,
    Mine,
    ElementalBarrierUp,
    ElementalBarrierDown,
    DelayedExplosion,
    MPEater,
    Hemorrhage,
    Bleeding,
    AwakeningSuccess,
    AwakeningFail,
    AwakeningMiss,
    AwakeningHit,
    StormEscape,
    TurtleKing,
    Behemoth,
    Stunned,
    IcePillar,
    KingGuard,
    KingGuard2,    
    DeathCrawlerBreath,
    FlamingMutantWeb,
    FurbolgWarriorCritical,
    HumUpEffect,//stupple
    Tester    
}


public enum BuffType : byte
{
    None = 0,

    //Magics
    TemporalFlux,
    Hiding,
    Haste,
    SwiftFeet,
    Fury,
    SoulShield,
    BlessedArmour,
    LightBody,
    UltimateEnhancer,
    ProtectionField,
    Rage,
    Curse,
    MoonLight,
    DarkBody,
    Concentration,
    VampireShot,
    PoisonShot,
    CounterAttack,
    MentalState,
    EnergyShield,
    MagicBooster,
    PetEnhancer,
    ImmortalSkin,
    MagicShield,
    ElementalBarrier,
    HumUp, //stupple

    //Monster
    HornedArcherBuff = 50,
    ColdArcherBuff,
    GeneralMeowMeowShield,
    RhinoPriestDebuff,
    PowerBeadBuff,
    HornedWarriorShield,
    HornedCommanderShield,
    Blindness,

    //Special
    GameMaster = 100,
    General,
    Exp,
    Drop,
    Gold,
    BagWeight,
    Transform,
    Lover,
    Mentee,
    Mentor,
    Guild,
    Prison,
    Rested,
    Skill,
    ClearRing,

    //Stats
    Impact = 200,
    Magic,
    Taoist,
    Storm,
    HealthAid,
    ManaAid,
    Defence,
    MagicDefence,
    WonderDrug,
    Knapsack,
}

[Flags]
public enum BuffProperty : byte
{
    None = 0,
    RemoveOnDeath = 1,
    RemoveOnExit = 2,
    Debuff = 4,
    PauseInSafeZone = 8
}

public enum BuffStackType : byte
{
    None,
    ResetDuration,
    StackDuration,
    StackStat,
    StackStatAndDuration,
    Infinite,
    ResetStat,
    ResetStatAndDuration
}

public enum DefenceType : byte
{
    ACAgility,
    AC,
    MACAgility,
    MAC,
    Agility,
    Repulsion,
    None
}

public enum ServerPacketIds : short
{
    Connected,
    ClientVersion,
    Disconnect,
    KeepAlive,
    NewAccount,
    ChangePassword,
    ChangePasswordBanned,
    Login,
    LoginBanned,
    LoginSuccess,
    NewCharacter,
    NewCharacterSuccess,
    DeleteCharacter,
    DeleteCharacterSuccess,
    StartGame,
    StartGameBanned,
    StartGameDelay,
    MapInformation,
    NewMapInfo,
    WorldMapSetup,
    SearchMapResult,
    UserInformation,
    UserSlotsRefresh,
    UserLocation,
    ObjectPlayer,
    ObjectHero,
    ObjectRemove,
    ObjectTurn,
    ObjectWalk,
    ObjectRun,
    Chat,
    ObjectChat,
    NewItemInfo,
    NewHeroInfo,
    NewChatItem,
    MoveItem,
    EquipItem,
    MergeItem,
    RemoveItem,
    RemoveSlotItem,
    TakeBackItem,
    StoreItem,
    SplitItem,
    SplitItem1,
    DepositRefineItem,
    RetrieveRefineItem,
    RefineCancel,
    RefineItem,
    DepositTradeItem,
    RetrieveTradeItem,
    UseItem,
    DropItem,
    TakeBackHeroItem,
    TransferHeroItem,
    PlayerUpdate,
    PlayerInspect,
    LogOutSuccess,
    LogOutFailed,
    ReturnToLogin,
    TimeOfDay,
    ChangeAMode,
    ChangePMode,
    ObjectItem,
    ObjectGold,
    GainedItem,
    GainedGold,
    LoseGold,
    GainedCredit,
    LoseCredit,
    ObjectMonster,
    ObjectAttack,
    Struck,
    ObjectStruck,
    DamageIndicator,
    DuraChanged,
    HealthChanged,
    HeroHealthChanged,
    DeleteItem,
    Death,
    ObjectDied,
    ColourChanged,
    ObjectColourChanged,
    ObjectGuildNameChanged,
    GainExperience,
    GainHeroExperience,
    LevelChanged,
    HeroLevelChanged,
    ObjectLeveled,
    ObjectHarvest,
    ObjectHarvested,
    ObjectNpc,
    NPCResponse,
    ObjectHide,
    ObjectShow,
    Poisoned,
    ObjectPoisoned,
    MapChanged,
    ObjectTeleportOut,
    ObjectTeleportIn,
    TeleportIn,
    NPCGoods,
    NPCSell,
    NPCRepair,
    NPCSRepair,
    NPCRefine,
    NPCCheckRefine,
    NPCCollectRefine,
    NPCReplaceWedRing,
    NPCStorage,
    SellItem,
    CraftItem,
    RepairItem,
    ItemRepaired,
    ItemSlotSizeChanged,
    ItemSealChanged,
    NewMagic,
    RemoveMagic,
    MagicLeveled,
    Magic,
    MagicDelay,
    MagicCast,
    ObjectMagic,
    ObjectEffect,
    ObjectProjectile,
    RangeAttack,
    Pushed,
    ObjectPushed,
    ObjectName,
    UserStorage,
    SwitchGroup,
    DeleteGroup,
    DeleteMember,
    GroupInvite,
    AddMember,
    Revived,
    ObjectRevived,
    SpellToggle,
    ObjectHealth,
    ObjectMana,
    MapEffect,
    AllowObserve,
    ObjectRangeAttack,
    AddBuff,
    RemoveBuff,
    PauseBuff,
    ObjectHidden,
    RefreshItem,
    ObjectSpell,
    UserDash,
    ObjectDash,
    UserDashFail,
    ObjectDashFail,
    NPCConsign,
    NPCMarket,
    NPCMarketPage,
    ConsignItem,
    MarketFail,
    MarketSuccess,
    ObjectSitDown,
    InTrapRock,
    BaseStatsInfo,
    HeroBaseStatsInfo,
    UserName,
    ChatItemStats,
    GuildNoticeChange,
    GuildMemberChange,
    GuildStatus,
    GuildInvite,
    GuildExpGain,
    GuildNameRequest,
    GuildStorageGoldChange,
    GuildStorageItemChange,
    GuildStorageList,
    GuildRequestWar,
    HeroCreateRequest,
    NewHero,
    HeroInformation,
    UpdateHeroSpawnState,
    UnlockHeroAutoPot,
    SetAutoPotValue,
    SetAutoPotItem,
    SetHeroBehaviour,
    ManageHeroes,
    ChangeHero,
    DefaultNPC,
    NPCUpdate,
    NPCImageUpdate,
    MarriageRequest,
    DivorceRequest,
    MentorRequest,
    TradeRequest,
    TradeAccept,
    TradeGold,
    TradeItem,
    TradeConfirm,
    TradeCancel,
    MountUpdate,
    EquipSlotItem,
    FishingUpdate,
    ChangeQuest,
    CompleteQuest,
    ShareQuest,
    NewQuestInfo,
    GainedQuestItem,
    DeleteQuestItem,
    CancelReincarnation,
    RequestReincarnation,
    UserBackStep,
    ObjectBackStep,
    UserDashAttack,
    ObjectDashAttack,
    UserAttackMove,
    CombineItem,
    ItemUpgraded,
    SetConcentration,
    SetElemental,
    RemoveDelayedExplosion,
    ObjectDeco,
    ObjectSneaking,
    ObjectLevelEffects,
    SetBindingShot,
    SendOutputMessage,
    NPCAwakening,
    NPCDisassemble,
    NPCDowngrade,
    NPCReset,
    AwakeningNeedMaterials,
    AwakeningLockedItem,
    Awakening,
    ReceiveMail,
    MailLockedItem,
    MailSendRequest,
    MailSent,
    ParcelCollected,
    MailCost,
    HumUpPlayer,//stupple
    ResizeInventory,
    ResizeStorage,
    NewIntelligentCreature,
    UpdateIntelligentCreatureList,
    IntelligentCreatureEnableRename,
    IntelligentCreaturePickup,
    NPCPearlGoods,
    TransformUpdate,
    FriendUpdate,
    LoverUpdate,
    MentorUpdate,
    GuildBuffList,
    NPCRequestInput,
    GameShopInfo,
    GameShopStock,
    Rankings,
    Opendoor,
    GetRentedItems,
    ItemRentalRequest,
    ItemRentalFee,
    ItemRentalPeriod,
    DepositRentalItem,
    RetrieveRentalItem,
    UpdateRentalItem,
    CancelItemRental,
    ItemRentalLock,
    ItemRentalPartnerLock,
    CanConfirmItemRental,
    ConfirmItemRental,
    NewRecipeInfo,
    OpenBrowser,
    PlaySound,
    SetTimer,
    ExpireTimer,
    UpdateNotice,
    Roll,
    SetCompass,
    GroupMembersMap,
    SendMemberLocation,
    PlayerTeleportList,//Fixed-point


}

public enum ClientPacketIds : short
{
    ClientVersion,
    Disconnect,
    KeepAlive,
    NewAccount,
    ChangePassword,
    Login,
    NewCharacter,
    DeleteCharacter,
    StartGame,
    LogOut,
    Turn,
    Walk,
    Run,
    Chat,
    MoveItem,
    StoreItem,
    TakeBackItem,
    MergeItem,
    EquipItem,
    RemoveItem,
    RemoveSlotItem,
    SplitItem,
    UseItem,
    DropItem,
    DepositRefineItem,
    RetrieveRefineItem,
    RefineCancel,
    RefineItem,
    CheckRefine,
    ReplaceWedRing,
    DepositTradeItem,
    RetrieveTradeItem,
    TakeBackHeroItem,
    TransferHeroItem,
    DropGold,
    PickUp,
    RequestMapInfo,
    TeleportToNPC,
    SearchMap,
    Inspect,
    Observe,
    ChangeAMode,
    ChangePMode,
    ChangeTrade,
    Attack,
    RangeAttack,
    Harvest,
    CallNPC,
    BuyItem,
    SellItem,
    CraftItem,
    RepairItem,
    BuyItemBack,
    SRepairItem,
    MagicKey,
    Magic,
    SwitchGroup,
    AddMember,
    DellMember,
    GroupInvite,
    NewHero,
    SetAutoPotValue,
    SetAutoPotItem,
    SetHeroBehaviour,
    ChangeHero,
    TownRevive,
    SpellToggle,
    ConsignItem,
    MarketSearch,
    MarketRefresh,
    MarketPage,
    MarketBuy,
    MarketGetBack,
    MarketSellNow,
    RequestUserName,
    RequestChatItem,
    EditGuildMember,
    EditGuildNotice,
    GuildInvite,
    GuildNameReturn,
    RequestGuildInfo,
    GuildStorageGoldChange,
    GuildStorageItemChange,
    GuildWarReturn,
    MarriageRequest,
    MarriageReply,
    ChangeMarriage,
    DivorceRequest,
    DivorceReply,
    AddMentor,
    MentorReply,
    AllowMentor,
    CancelMentor,
    TradeRequest,
    TradeReply,
    TradeGold,
    TradeConfirm,
    TradeCancel,
    EquipSlotItem,
    FishingCast,
    FishingChangeAutocast,
    AcceptQuest,
    FinishQuest,
    AbandonQuest,
    ShareQuest,

    AcceptReincarnation,
    CancelReincarnation,
    CombineItem,

    AwakeningNeedMaterials,
    AwakeningLockedItem,
    Awakening,
    DisassembleItem,
    DowngradeAwakening,
    ResetAddedItem,

    SendMail,
    ReadMail,
    CollectParcel,
    DeleteMail,
    LockMail,
    MailLockedItem,
    MailCost,

    UpdateIntelligentCreature,
    IntelligentCreaturePickup,
    RequestIntelligentCreatureUpdates,

    AddFriend,
    RemoveFriend,
    RefreshFriends,
    AddMemo,
    GuildBuffUpdate,
    NPCConfirmInput,
    GameshopBuy,

    ReportIssue,
    GetRanking,
    Opendoor,

    GetRentedItems,
    ItemRentalRequest,
    ItemRentalFee,
    ItemRentalPeriod,
    DepositRentalItem,
    RetrieveRentalItem,
    CancelItemRental,
    ItemRentalLockFee,
    ItemRentalLockItem,
    ConfirmItemRental,
    MemoryLocation,//Fixed-point
    PositionMove,//Fixed-point
}

public enum ConquestType : byte
{
    Request = 0,
    Auto = 1,
    Forced = 2,
}

public enum ConquestGame : byte
{
    CapturePalace = 0,
    KingOfHill = 1,
    Random = 2,
    Classic = 3,
    ControlPoints = 4
}

[Flags]
public enum GuildRankOptions : byte
{
    CanChangeRank = 1,
    CanRecruit = 2,
    CanKick = 4,
    CanStoreItem = 8,
    CanRetrieveItem = 16,
    CanAlterAlliance = 32,
    CanChangeNotice = 64,
    CanActivateBuff = 128
}

public enum DoorState : byte
{
    Closed = 0,
    Opening = 1,
    Open = 2,
    Closing = 3
}

public enum IntelligentCreaturePickupMode : byte
{
    Automatic = 0,
    SemiAutomatic = 1,
}

public enum HeroSpawnState : byte
{
    None = 0,
    Unsummoned = 1,
    Summoned = 2,
    Dead = 3
}

public enum HeroBehaviour : byte
{
    Attack = 0,
    CounterAttack = 1,
    Follow = 2,
    Custom = 3
}

public enum SpellToggleState: sbyte
{
    None = -1,
    False = 0,
    True = 1
}

#region Map transmission
public class NPCTeleportInfo  //Map transmission
{
    public string Name;
    public Point Location;
    public bool NoBigMapTeleport;
    public NPCTeleportInfo()
    {
    }
    public NPCTeleportInfo(BinaryReader reader)
    {
        Name = reader.ReadString();
        Location = new Point(reader.ReadInt32(), reader.ReadInt32());
        NoBigMapTeleport = reader.ReadBoolean();
    }
    public void Save(BinaryWriter writer)
    {
        writer.Write(this.Name);
        writer.Write(this.Location.X);
        writer.Write(this.Location.Y);
        writer.Write(NoBigMapTeleport);
    }

}

public class PlayerTeleportInfo
{
    public string Name;
    public string MapName;
    public Point Location;
    public int ColorIndex;

    public PlayerTeleportInfo()
    {
    }

    public PlayerTeleportInfo(BinaryReader reader)
    {
        Name = reader.ReadString();
        MapName = reader.ReadString();
        Location = new Point(reader.ReadInt32(), reader.ReadInt32());
        ColorIndex = reader.ReadInt32();
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(Name);
        writer.Write(MapName);
        writer.Write(Location.X);
        writer.Write(Location.Y);
        writer.Write(ColorIndex);
    }
}

#endregion