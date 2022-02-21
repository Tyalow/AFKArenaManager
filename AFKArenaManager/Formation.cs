using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFKArenaManager
{
    internal class Formation
    {
        public string Name { get; set; }
        public List<Character> FormationPrimaryCharacters { get; private set; }
        public List<Character> FormationSecondaryCharacters { get; private set; }
        public List<ContentType> ContentList { get; }

        public Formation (string name, List<Character> formationPrimaryCharacters, List<Character> formationSecondaryCharacters)
        {
            Name = name;
            FormationPrimaryCharacters = formationPrimaryCharacters;
            FormationSecondaryCharacters = formationSecondaryCharacters;
        }
        public Formation (string name, List<Character> formationPrimaryCharacters, List<Character> formationSecondaryCharacters, List<ContentType> contentList)
        {
            Name = name;
            FormationPrimaryCharacters = formationPrimaryCharacters;
            FormationSecondaryCharacters = formationSecondaryCharacters;
            ContentList = contentList;
        }
        //Returns an estimation on formation value based on the relevant content types
        public int FormationValue()
        {
            int runningVal = 0;
            foreach (ContentType contenttype in ContentList)
            {
                runningVal += contenttype.ContentValue;
            }
            return runningVal;
        }
        public override string ToString()
        {
            string primaryChars = string.Join(",", FormationPrimaryCharacters.Select(x => x.CharacterName));
            string secondaryChars = string.Join(",", FormationSecondaryCharacters.Select(x => x.CharacterName));
            if(ContentList != null)
            {
                string content = string.Join(",", ContentList.Select(x => x.ContentName));
                return $"{Name}.{primaryChars}.{secondaryChars}.{content}";
            }

            return $"{Name}.{primaryChars}.{secondaryChars}";
        }
        //Power level updates to characters on a formation level
        public void AddCharacterToFormation(Character character, bool isPrimary)
        {
            if(isPrimary)
            {
                FormationPrimaryCharacters.Add(character);
                foreach(ContentType content in ContentList)
                {
                    character.powerValue += content.ContentValue * 2;
                }
            }
            else
            {
                FormationSecondaryCharacters.Add(character);
                foreach (ContentType content in ContentList)
                {
                    character.powerValue += content.ContentValue;
                }
            }
        }
        public void RemoveCharacterFromFormation(Character character, bool isPrimary)
        {
            if(isPrimary)
            {
                foreach (ContentType content in ContentList)
                {
                    character.powerValue -= content.ContentValue * 2;
                }
            }
            else
            {
                foreach (ContentType content in ContentList)
                {
                    character.powerValue -= content.ContentValue;
                }
            }
        }
    }
}
