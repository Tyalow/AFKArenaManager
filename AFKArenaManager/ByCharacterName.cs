using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFKArenaManager
{
    internal class ByCharacterName : IComparer<Character>
    {
        CaseInsensitiveComparer caseiComp = new CaseInsensitiveComparer();

        public int Compare(Character x, Character y)
        {
            return caseiComp.Compare(x.CharacterName, y.CharacterName);
        }
    }
}
