using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Server.MirEnvir;
using S = ServerPackets;


namespace Server.MirObjects
{
    public class SpellObject : MapObject
    {
        public override ObjectType Race
        {
            get { return ObjectType.Spell; }
        }

        public override string Name { get; set; }
        public override int CurrentMapIndex { get; set; }
        public override Point CurrentLocation { get; set; }
        public override MirDirection Direction { get; set; }
        public override ushort Level { get; set; }
        public override bool Blocking
        {
            get
            {
                return false;
            }
        }

        public long TickTime, StartTime;

        public MapObject Caster;//施法者，技能释放者

        public int Value, TickSpeed, BonusDmg;
        public Spell Spell;
        public Point CastLocation;
        public bool Show, Decoration;

        //ExplosiveTrap
        public int ExplosiveTrapID;
        public int ExplosiveTrapCount;
        public bool DetonatedTrap;

        public override int Health
        {
            get { throw new NotSupportedException(); }
        }
        public override int MaxHealth
        {
            get { throw new NotSupportedException(); }
        }


        public override void Process()
        {

            if (Decoration) return;

            if (Caster != null && Caster.Node == null) Caster = null;

            if (Envir.Time > ExpireTime || ((Spell == Spell.火墙 || Spell == Spell.Portal || Spell == Spell.爆阱 || Spell == Spell.苏生术) && Caster == null) || (Spell == Spell.困魔咒 && Target != null) || (Spell == Spell.捕缚术 && Target != null))
            {
                if (Spell == Spell.困魔咒 && Target != null || Spell == Spell.捕缚术 && Target != null)
                {
                    MonsterObject ob = (MonsterObject)Target;

                    if (Envir.Time < ExpireTime && ob.ShockTime != 0) return;
                }

                if (Spell == Spell.苏生术 && Caster != null)
                {
                    ((HumanObject)Caster).ReincarnationReady = true;
                    ((HumanObject)Caster).ReincarnationExpireTime = Envir.Time + 6000;
                }

                if ((Spell == Spell.天霜冰环 || Spell == Spell.天上秘术) &&  Caster != null)
                {
                    ((HumanObject)Caster).ActiveBlizzard = false;
                }

                CurrentMap.RemoveObject(this);
                Despawn();
                return;
            }

            if (Spell == Spell.火墙)
            {
                if (CurrentMap != Caster?.CurrentMap)
                {
                    CurrentMap.RemoveObject(this);
                    Despawn();
                    return;
                }
            }

            if (Spell == Spell.苏生术 && !((HumanObject)Caster).ActiveReincarnation)
            {
                CurrentMap.RemoveObject(this);
                Despawn();
                return;
            }
            if (Spell == Spell.爆阱 && FindObject(Caster.ObjectID, 20) == null && Caster != null)
            {
                CurrentMap.RemoveObject(this);
                Despawn();
                ((HumanObject)Caster).Enqueue(new S.SpellToggle { Spell = Spell.爆阱, CanUse = false });
                return;
            }


            if (Spell == Spell.爆阱 && Caster != null && ((HumanObject)Caster).detonate == true)
            {
                DetonateTrapNow();
                CurrentMap.RemoveObject(this);
                Despawn();
                return;
            }

            if (Envir.Time < TickTime) return;
            TickTime = Envir.Time + TickSpeed;

            Cell cell = CurrentMap.GetCell(CurrentLocation);
            for (int i = 0; i < cell.Objects.Count; i++)
                if (cell != null)
                {
                    ProcessSpell(cell.Objects[i]);
                }

            if ((Spell == Spell.MapLava) || (Spell == Spell.MapLightning)) Value = 0;
        }
        public void ProcessSpell(MapObject ob)
        {
            if (Envir.Time < StartTime) return;
            switch (Spell)
            {
                case Spell.火墙:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;

                        if (!ob.IsAttackTarget(Caster)) return;
                        ob.Attacked(((HumanObject)Caster), Value, DefenceType.MAC, false);
                    }
                    break;
                case Spell.治愈术: //SafeZone
                    {
                        if (ob.Race != ObjectType.Player && (ob.Race != ObjectType.Monster || ob.Master == null || ob.Master.Race != ObjectType.Player)) return;
                        if (ob.Dead || ob.HealAmount != 0 || ob.PercentHealth == 100) return;

                        ob.HealAmount += 25;
                        Broadcast(new S.ObjectEffect { ObjectID = ob.ObjectID, Effect = SpellEffect.Healing });
                    }
                    break;
                case Spell.毒雾:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;

                        if (!ob.IsAttackTarget(Caster)) return;
                        ob.Attacked(((HumanObject)Caster), Value, DefenceType.MAC, false);
                        if (!ob.Dead)
                            ob.ApplyPoison(new Poison
                            {
                                Duration = 12,
                                Owner = Caster,
                                PType = PoisonType.Green,
                                TickSpeed = TickSpeed,
                                Value = (Caster.Stats[Stat.MinSC] + Caster.Stats[Stat.MaxSC]) / 2 + BonusDmg
                            }, Caster, false, false);
                    }
                    break;
                case Spell.天霜冰环:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (Caster != null && ((HumanObject)Caster).ActiveBlizzard == false) return;
                        if (!ob.IsAttackTarget(Caster)) return;
                        ob.Attacked(((HumanObject)Caster), Value, DefenceType.MAC, false);
                        if (!ob.Dead && Envir.Random.Next(8) == 0)
                            ob.ApplyPoison(new Poison
                            {
                                Duration = 5 + Envir.Random.Next(Caster.Stats[Stat.Freezing]),
                                Owner = Caster,
                                PType = PoisonType.Slow,
                                TickSpeed = 2000,
                            }, Caster);
                    }
                    break;
                case Spell.流星火雨:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (Caster != null && ((HumanObject)Caster).ActiveBlizzard == false) return;
                        if (!ob.IsAttackTarget(Caster)) return;
                        ob.Attacked(((HumanObject)Caster), Value, DefenceType.MAC, false);
                    }
                    break;
                case Spell.爆阱:
                    {
                        if (((HumanObject)Caster).detonate == false) return;
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (!ob.IsAttackTarget(Caster)) return;
                        if (DetonatedTrap) return;
                        DetonateTrapNow();
                        //ob.Attacked(((HumanObject)Caster), Value, DefenceType.MAC, false);
                    }
                    break;
                case Spell.阴阳五行阵:
                    if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                    if (ob.Dead) return;

                    if (ob.IsAttackTarget(Caster))
                    {
                        ob.Attacked(((HumanObject)Caster), Value * 10 / 8, DefenceType.MAC, false);
                        Broadcast(new S.ObjectEffect { ObjectID = ob.ObjectID, Effect = SpellEffect.Healing3 });
                    }

                    else if (ob.IsFriendlyTarget(Caster))
                    {
                        //if (ob.HealAmount != 0 || ob.PercentHealth == 100) return;
                        if (ob.HealAmount > Value * 2 || ob.PercentHealth == 100) return;
                        ob.HealAmount += (ushort)(Value * 2);
                        Broadcast(new S.ObjectEffect { ObjectID = ob.ObjectID, Effect = SpellEffect.Healing2 });
                    }
                    break;
                case Spell.MapLava:
                case Spell.MapLightning:
                    {
                        if (ob is PlayerObject player)
                        {
                            if (player.Account.AdminAccount && player.Observer) return;
                            player.Struck(Value, DefenceType.MAC);
                        }                 
                    }
                    break;
                case Spell.MapQuake1:
                case Spell.MapQuake2:
                    {
                        if (Value == 0) return;
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        ob.Struck(Value, DefenceType.MAC);
                    }
                    break;
                case Spell.GeneralMeowMeowThunder:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (ob == Caster) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        if (Value == 0) return;
                        ob.Struck(Value, DefenceType.MAC);
                    }
                    break;
                case Spell.TreeQueenRoot:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (ob == Caster) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        if (Value == 0) return;
                        ob.Struck(Value, DefenceType.MAC);
                    }
                    break;
                case Spell.TreeQueenMassRoots:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (ob == Caster) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        if (Value == 0) return;
                        ob.Struck(Value, DefenceType.MAC);
                    }
                    break;
                case Spell.TreeQueenGroundRoots:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (ob == Caster) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        ob.Struck(Value, DefenceType.MAC);

                        if (Envir.Random.Next(3) > 0)
                        {
                            ob.ApplyPoison(new Poison { PType = PoisonType.Paralysis, Duration = 5, TickSpeed = 1000 }, this);
                        }
                    }
                    break;
                case Spell.StoneGolemQuake:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (ob == Caster) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        if (Value == 0) return;
                        ob.Struck(Value, DefenceType.AC);
                    }
                    break;
                case Spell.EarthGolemPile:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (ob == Caster) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        if (Value == 0) return;
                        ob.Struck(Value, DefenceType.AC);
                    }
                    break;
                case Spell.TucsonGeneralRock:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (ob == Caster) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        if (Value == 0) return;
                        ob.Struck(Value, DefenceType.AC);
                    }
                    break;
                case Spell.Portal:
                    {
                        if (ob.Race != ObjectType.Player) return;
                        if (Caster != ob && (Caster == null || (Caster.GroupMembers == null) || (!Caster.GroupMembers.Contains((HumanObject)ob)))) return;

                        var portal = Envir.Spells.SingleOrDefault(ob => ob != this && ob.Node != null
                            && ob.Spell == Spell.Portal
                            && ob.Caster == Caster);

                        if (portal != null)
                        {
                            Point newExit = Functions.PointMove(portal.CurrentLocation, ob.Direction, 1);

                            if (!portal.CurrentMap.ValidPoint(newExit)) return;

                            ob.Teleport(portal.CurrentMap, newExit, false);
                        }

                        Value -= 1;

                        if (Value < 1)
                        {
                            ExpireTime = 0;
                        }
                    }
                    break;
                case Spell.FlyingStatueIceTornado:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (ob == Caster) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        ob.Struck(Value, DefenceType.MAC);

                        if (Envir.Random.Next(8) == 0)
                        {
                            ob.ApplyPoison(new Poison { PType = PoisonType.Slow, Duration = 5, TickSpeed = 1000 }, this);
                        }
                    }
                    break;
                case Spell.DarkOmaKingNuke:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (ob == Caster) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        ob.Struck(Value, DefenceType.AC);
                        ob.ApplyPoison(new Poison { PType = PoisonType.Dazed, Duration = 5, TickSpeed = 1000 }, this);
                    }
                    break;
                case Spell.HornedSorcererDustTornado:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (ob == Caster) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        if (Value == 0) return;
                        ob.Struck(Value, DefenceType.AC);
                    }
                    break;
                case Spell.HornedCommanderRockFall:
                case Spell.HornedCommanderRockSpike:
                    {
                        if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster) return;
                        if (ob.Dead) return;
                        if (ob == Caster) return;
                        if (!ob.IsAttackTarget(Caster)) return;

                        if (Value == 0) return;
                        ob.Struck(Value, DefenceType.AC);
                    }
                    break;
            }
        }
        protected List<MapObject> FindAllTargets(int dist, Point location, bool needSight = true)
        {
            List<MapObject> targets = new List<MapObject>();
            for (int d = 0; d <= dist; d++)
            {
                for (int y = location.Y - d; y <= location.Y + d; y++)
                {
                    if (y < 0) continue;
                    if (y >= CurrentMap.Height) break;

                    for (int x = location.X - d; x <= location.X + d; x += Math.Abs(y - location.Y) == d ? 1 : d * 2)
                    {
                        if (x < 0) continue;
                        if (x >= CurrentMap.Width) break;

                        Cell cell = CurrentMap.GetCell(x, y);
                        if (!cell.Valid || cell.Objects == null) continue;

                        for (int i = 0; i < cell.Objects.Count; i++)
                        {
                            MapObject ob = cell.Objects[i];
                            switch (ob.Race)
                            {
                                case ObjectType.Monster:
                                case ObjectType.Player:
                                case ObjectType.Hero:
                                    if (!ob.IsAttackTarget(Caster)) continue;
                                    //if (ob.Hidden && (!CoolEye || Level < ob.Level) && needSight ) continue;
                                    if (ob.Race == ObjectType.Player)
                                    {
                                        PlayerObject player = ((PlayerObject)ob);
                                        if (player.GMGameMaster) continue;
                                    }
                                    targets.Add(ob);
                                    continue;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }
            return targets;
        }
        public void DetonateTrapNow()
        {
            DetonatedTrap = true;
            Broadcast(GetInfo());
            ExpireTime = Envir.Time + 1000;

            ((HumanObject)Caster).detonate = false;
            ((HumanObject)Caster).Enqueue(new S.SpellToggle { Spell = Spell.爆阱, CanUse = false });

            Point location = CurrentLocation;

            List<MapObject> targets = FindAllTargets(1, location);

            for (int i = 0; i < targets.Count; i++)
            {
                Target = targets[i];
                Target.Attacked((HumanObject)Caster, Value, DefenceType.MAC, false);
            }
        }

        public override void SetOperateTime()
        {
            long time = Envir.Time + 2000;

            if (TickTime < time && TickTime > Envir.Time)
                time = TickTime;

            if (OwnerTime < time && OwnerTime > Envir.Time)
                time = OwnerTime;

            if (ExpireTime < time && ExpireTime > Envir.Time)
                time = ExpireTime;

            if (PKPointTime < time && PKPointTime > Envir.Time)
                time = PKPointTime;

            if (LastHitTime < time && LastHitTime > Envir.Time)
                time = LastHitTime;

            if (EXPOwnerTime < time && EXPOwnerTime > Envir.Time)
                time = EXPOwnerTime;

            if (BrownTime < time && BrownTime > Envir.Time)
                time = BrownTime;

            for (int i = 0; i < ActionList.Count; i++)
            {
                if (ActionList[i].Time >= time && ActionList[i].Time > Envir.Time) continue;
                time = ActionList[i].Time;
            }

            for (int i = 0; i < PoisonList.Count; i++)
            {
                if (PoisonList[i].TickTime >= time && PoisonList[i].TickTime > Envir.Time) continue;
                time = PoisonList[i].TickTime;
            }

            for (int i = 0; i < Buffs.Count; i++)
            {
                if (Buffs[i].NextTime >= time && Buffs[i].NextTime > Envir.Time) continue;
                time = Buffs[i].NextTime;
            }

            if (OperateTime <= Envir.Time || time < OperateTime)
                OperateTime = time;
        }

        public override void Process(DelayedAction action)
        {
            throw new NotSupportedException();
        }
        public override bool IsAttackTarget(HumanObject attacker)
        {
            throw new NotSupportedException();
        }
        public override bool IsAttackTarget(MonsterObject attacker)
        {
            throw new NotSupportedException();
        }
        public override int Attacked(HumanObject attacker, int damage, DefenceType type = DefenceType.ACAgility, bool damageWeapon = true)
        {
            throw new NotSupportedException();
        }
        public override int Attacked(MonsterObject attacker, int damage, DefenceType type = DefenceType.ACAgility)
        {
            throw new NotSupportedException();
        }

        public override int Struck(int damage, DefenceType type = DefenceType.ACAgility)
        {
            throw new NotSupportedException();
        }
        public override bool IsFriendlyTarget(HumanObject ally)
        {
            throw new NotSupportedException();
        }
        public override bool IsFriendlyTarget(MonsterObject ally)
        {
            throw new NotSupportedException();
        }
        public override void ReceiveChat(string text, ChatType type)
        {
            throw new NotSupportedException();
        }

        public override Packet GetInfo()
        {
            switch (Spell)
            {
                case Spell.治愈术:
                    return null;
                case Spell.毒雾:
                case Spell.天霜冰环:
                case Spell.流星火雨:
                case Spell.阴阳五行阵:
                case Spell.StoneGolemQuake:
                case Spell.EarthGolemPile:
                case Spell.TreeQueenMassRoots:
                case Spell.TreeQueenGroundRoots:
                case Spell.FlyingStatueIceTornado:
                case Spell.DarkOmaKingNuke:
                case Spell.HornedSorcererDustTornado:
                case Spell.HornedCommanderRockFall:
                case Spell.HornedCommanderRockSpike:
                    if (!Show)
                        return null;

                    return new S.ObjectSpell
                    {
                        ObjectID = ObjectID,
                        Location = CastLocation,
                        Spell = Spell,
                        Direction = Direction
                    };
                case Spell.爆阱:
                    return new S.ObjectSpell
                    {
                        ObjectID = ObjectID,
                        Location = CurrentLocation,
                        Spell = Spell,
                        Direction = Direction,
                        Param = DetonatedTrap
                    };
                default:
                    return new S.ObjectSpell
                    {
                        ObjectID = ObjectID,
                        Location = CurrentLocation,
                        Spell = Spell,
                        Direction = Direction
                    };
            }

        }

        public override void ApplyPoison(Poison p, MapObject Caster = null, bool NoResist = false, bool ignoreDefence = true)
        {
            throw new NotSupportedException();
        }
        public override void Die()
        {
            throw new NotSupportedException();
        }
        public override int Pushed(MapObject pusher, MirDirection dir, int distance)
        {
            throw new NotSupportedException();
        }
        public override void SendHealth(HumanObject player)
        {
            throw new NotSupportedException();
        }

        public override void Spawned()
        {
            base.Spawned();

            Envir.Spells.Add(this);
        }

        public override void Despawn()
        {
            base.Despawn();

            Envir.Spells.Remove(this);

            if (Spell == Spell.苏生术 && Caster != null && Caster.Node != null)
            {
                ((HumanObject)Caster).ActiveReincarnation = false;
                ((HumanObject)Caster).Enqueue(new S.CancelReincarnation { });
            }

            if (Spell == Spell.爆阱 && Caster != null)
                ((HumanObject)Caster).ExplosiveTrapDetonated(ExplosiveTrapID, ExplosiveTrapCount);

            if (Spell == Spell.Portal && Caster != null)
            {
                var portal = Envir.Spells.SingleOrDefault(ob => ob.Node != null && ob != this
                    && ob.Spell == Spell.Portal    
                    && ob.Caster == Caster);

                if (portal != null)
                {
                    portal.ExpireTime = 0;
                    portal.Process();
                }
            }
        }

        public override void BroadcastInfo()
        {
            if ((Spell != Spell.爆阱) || (Caster == null))
                base.BroadcastInfo();

            Packet p;
            if (CurrentMap == null) return;

            for (int i = CurrentMap.Players.Count - 1; i >= 0; i--)
            {
                PlayerObject player = CurrentMap.Players[i];
                if (Functions.InRange(CurrentLocation, player.CurrentLocation, Globals.DataRange))
                {
                    if ((Caster == null) || (player == null)) continue;

                    if ((player == Caster) || player.IsFriendlyTarget(Caster))
                    {
                        p = GetInfo();

                        if (p != null)
                            player.Enqueue(p);
                    }
                }
            }
        }
    }
}
