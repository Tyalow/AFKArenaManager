using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFKArenaManager
{
    internal class CharacterListHandler 
    {
        public SortedSet<Character> CharacterList { get; private set; }

        public CharacterListHandler(string characterDataPath)
        {
            CharacterList = LoadCharacters(characterDataPath);
        }
        public SortedSet<Character> LoadCharacters(string characterDataPath)
        {
            SortedSet<Character> characterList = new SortedSet<Character>(new ByCharacterName());
            string[] characterArray = File.ReadAllLines(characterDataPath);

            foreach (string character in characterArray)
            {
                string[] characterValues = character.Split(',');
                if (characterValues.Length == 8)
                {
                    int gearInt;
                    int furnInt;
                    int sigInt;
                    int engInt;
                    try
                    {
                        gearInt = Int32.Parse(characterValues[5]);
                        furnInt = Int32.Parse(characterValues[6]);
                        sigInt = Int32.Parse(characterValues[7]);
                        engInt = Int32.Parse(characterValues[8]);
                        characterList.Add(new Character(characterValues[0], characterValues[1],
                        characterValues[2], characterValues[3], characterValues[4], gearInt, furnInt, sigInt, engInt));
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return characterList;
        }
        public void SaveCharacters(string characterDataPath)
        {
            string[] saveString = new string[CharacterList.Count];

            for(int i = 0; i < CharacterList.Count; i++)
            {
                saveString[i] = CharacterList.ElementAt(i).ToString();
            }
            File.WriteAllLines(characterDataPath, saveString);
        }
        public void AddNewCharacter(Character newChar)
        {
            CharacterList.Add(newChar);
        }

    }
}
