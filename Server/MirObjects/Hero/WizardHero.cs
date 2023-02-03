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

namespace Server.MirObjects
{
    public class WizardHero : HeroObject
    {
        public WizardHero(CharacterInfo info, PlayerObject owner) : base(info, owner) { }
        public int SurroundedCount;
        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;

            if (HasRangedSpell)
                return TargetDistance <= ViewRange;

            return Target.CurrentLocation != CurrentLocation && Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);
        }
        protected override void ProcessFriend()
        {
            if (!CanCast) return;

            if (Target != null)
            {
                UserMagic magic = GetMagic(Spell.魔法盾);
                if (CanUseMagic(magic) && !HasBuff(BuffType.MagicShield))
                {
                    BeginMagic(magic.Spell, Direction, ObjectID, CurrentLocation);
                    return;
                }

                magic = GetMagic(Spell.深延术);
                if (CanUseMagic(magic) && !HasBuff(BuffType.MagicBooster))
                {
                    BeginMagic(magic.Spell, Direction, ObjectID, CurrentLocation);
                    return;
                }
            }
        }

        protected override void ProcessAttack()
        {
            if (!CanCast || Target == null || Target.Dead) return;
            TargetDistance = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);
            SurroundedCount = FindAllTargets(2, CurrentLocation).Count;
            if (!HasRangedSpell) return;

            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            UserMagic magic;

            if (TargetDistance == 1)
            {
                if (Target.Level < Level)
                {
                    magic = GetMagic(Spell.抗拒火环);
                    if (CanUseMagic(magic))
                    {
                        BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                        return;
                    }
                }
            }

            if (TargetDistance < 3 && SurroundedCount > 1)
            {
                magic = GetMagic(Spell.火龙气焰);
                if (CanUseMagic(magic))
                {
                    BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                    return;
                }

                magic = GetMagic(Spell.地狱雷光);
                if (CanUseMagic(magic))
                {
                    BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                    return;
                }
            }

            magic = GetMagic(Spell.灭天火);
            if (CanUseMagic(magic))
            {
                BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                return;
            }

            magic = GetMagic(Spell.噬血术);
            if (CanUseMagic(magic))
            {
                BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                return;
            }

            magic = GetMagic(Spell.寒冰掌);
            if (CanUseMagic(magic))
            {
                BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                return;
            }

            magic = GetMagic(Spell.雷电术);
            if (CanUseMagic(magic))
            {
                BeginMagic(magic.Spell, Direction, Target.ObjectID, Target.CurrentLocation);
                return;
            }

            magic = GetMagic(Spell.火球术);
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
                Attack();

                if (Target != null && Target.Dead)
                {
                    FindTarget();
                }

                return;
            }

            if (!HasRangedSpell)
                MoveTo(Target.CurrentLocation);
        }

        private bool HasRangedSpell => Info.Magics.Select(x => x.Spell).Intersect(Globals.RangedSpells).Any();
    }
}
