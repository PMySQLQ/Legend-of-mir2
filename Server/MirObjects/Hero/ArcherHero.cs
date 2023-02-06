using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using C = ClientPackets;
using Server.MirDatabase;
using Server.MirEnvir;
using Server.MirNetwork;
using S = ServerPackets;
using System.Text.RegularExpressions;
using Server.MirObjects.Monsters;
using static System.Collections.Specialized.BitVector32;

namespace Server.MirObjects
{
    public class ArcherHero : HeroObject
    {
        public long FearTime, TeleportTime;
        public ArcherHero(CharacterInfo info, PlayerObject owner) : base(info, owner) { }

        protected virtual byte AttackRange
        {
            get
            {
                return 8;
            }
        }
        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;

            if (HasRangedSpell)
                return TargetDistance <= ViewRange;

            return Target.CurrentLocation != CurrentLocation && Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
        }


        protected override void ProcessAttack()
        {
            if (Target == null || Target.Dead) return;
            TargetDistance = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);
            if (!CanCast) return;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            UserMagic magic;

            magic = GetMagic(Spell.天日闪);
            if (CanUseMagic(magic))
            {
                BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                return;
            }
        }

        protected override void ProcessTarget()
        {
            if (CanCast && NextMagicSpell != Spell.None)
            {
                Magic(NextMagicSpell, NextMagicDirection, NextMagicTargetID, NextMagicLocation);
                NextMagicSpell = Spell.None;
            }
            if (Target == null || !CanAttack) return;

            if (!HasRangedSpell && InAttackRange())
            {
                RangeAttack(Direction, Target.CurrentLocation, Target.ObjectID);

                if (Target != null && Target.Dead)
                {
                    FindTarget();
                }
                return;
            }

            int dist = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

            if (dist >= AttackRange)
                MoveTo(Target.CurrentLocation);
            else
            {
                MirDirection dir = Functions.DirectionFromPoint(Target.CurrentLocation, CurrentLocation);

                if (Walk(dir)) return;

                switch (Envir.Random.Next(2)) //No favour
                {
                    case 0:
                        for (int i = 0; i < 7; i++)
                        {
                            dir = Functions.NextDir(dir);

                            if (Walk(dir))
                                return;
                        }
                        break;
                    default:
                        for (int i = 0; i < 7; i++)
                        {
                            dir = Functions.PreviousDir(dir);

                            if (Walk(dir))
                                return;
                        }
                        break;
                }
            }

            if (!HasRangedSpell)
                MoveTo(Target.CurrentLocation);
        }

        private bool HasRangedSpell => Info.Magics.Select(x => x.Spell).Intersect(Globals.RangedSpells).Any();
    }
}
