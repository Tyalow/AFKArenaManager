using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFKArenaManager
{
    internal class FormationList
    {
        List<Formation> Formations;
        CharacterListHandler CharacterListHandle;
        ContentList ContentListHandle;

        public FormationList(string filePath, CharacterListHandler characterListHandle, ContentList contentList)
        {
            CharacterListHandle = characterListHandle;
            ContentListHandle = contentList;
            Formations = new List<Formation>();
            LoadFormations(filePath);
            StartupCharacterPowerLevels();
        }

        //Save and Load functions
        public void LoadFormations(string filePath)
        {
            string[] contentArray = File.ReadAllLines(filePath);
            foreach (string content in contentArray)
            {
                //First entry formation name, second entry is primary names, third entry is secondary names
                string[] parsedContent = content.Split('.');

                string formationName = parsedContent[0];

                string[] primaryChars = parsedContent[1].Split(',');
                List<Character> primaryCharList = new List<Character>();
                LoadCharactersFromCharacterList(primaryChars, primaryCharList);

                string[] secondaryChars = parsedContent[2].Split(',');
                List<Character> secondaryCharList = new List<Character>();
                LoadCharactersFromCharacterList(secondaryChars, secondaryCharList);

                //Cases for formations with and without attached content
                if(parsedContent.Length == 4)
                {
                    string[] gameContent = parsedContent[3].Split(',');
                    List<ContentType> contentList = new List<ContentType>();
                    LoadContentFromContentList(gameContent, contentList);

                    Formations.Add(new Formation(formationName, primaryCharList, secondaryCharList, contentList));
                }
                else
                {
                    Formations.Add(new Formation(formationName, primaryCharList, secondaryCharList));
                } 
            }
        }
        public void SaveFormations(string filePath)
        {
            string[] contentString = new string[Formations.Count];

            for (int i = 0; i < Formations.Count; i++)
            {
                contentString[i] = Formations.ElementAt(i).ToString();
            }
            File.WriteAllLines(filePath, contentString);
        }
        private void LoadCharactersFromCharacterList(string[] characterArray, List<Character> characterList)
        {
            foreach (Character character in CharacterListHandle.CharacterList)
            {
                foreach(string characterName in characterArray)
                {
                    if(characterName == character.CharacterName)
                    {
                        characterList.Add(character);
                    }
                }    
            }            
        }
        private void LoadContentFromContentList(string[] gameContent, List<ContentType> contentList)
        {
            foreach(ContentType content in ContentListHandle.ContentTypes)
            {
                foreach(string eachContent in gameContent)
                {
                    if(eachContent == content.ContentName)
                    {
                        contentList.Add(content);
                    }
                }
            }
        }

        //Update methods for character power level changes
        //Startup Update
        public void StartupCharacterPowerLevels()
        {
            foreach (Formation formation in Formations)
            {
                foreach (ContentType content in formation.ContentList)
                {
                    foreach(Character character in formation.FormationPrimaryCharacters)
                    {
                        character.powerValue += content.ContentValue * 2;
                    }
                    foreach(Character character in formation.FormationSecondaryCharacters)
                    {
                        character.powerValue += content.ContentValue;
                    }
                }
            }
        }
        //Add new formation, formation updates will be handled in the Formation class itself
        public void AddFormation(Formation formation)
        {
            foreach (ContentType content in formation.ContentList)
            {
                foreach (Character character in formation.FormationPrimaryCharacters)
                {
                    character.powerValue += content.ContentValue * 2;
                }
                foreach (Character character in formation.FormationSecondaryCharacters)
                {
                    character.powerValue += content.ContentValue;
                }
            }
        }

    }
}
