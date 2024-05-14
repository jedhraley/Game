using System;
using System.Collections.Generic;

namespace Game
{
    internal static class utterAbominations
    {
        internal static List<Spell> CheckSpells(this List<Spell> spells, Hero hero)
        {
            foreach (Spell spell in spells)
            {
                if (spell.Unlocked | spell.Anyclass | spell.HeroClass == hero.HeroClass)
                {
                }
                else
                {
                    spells.Remove(spell);
                }
            }
            return spells;
        }
        internal static void KeyCheck()
        {
            Console.WriteLine("чезакнопка");
            var key = Console.ReadKey().Key;
            Console.WriteLine(key);
            System.Threading.Thread.Sleep(1000);
        }
    }
}
