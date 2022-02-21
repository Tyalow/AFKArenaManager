using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AFKArenaManager
{
    public enum Faction
    {
        Wilder,
        Mauler,
        Graveborn,
        Lightbearer,
        Celestial,
        Hypogean,
        Dimensional
    }
    public enum Attribute
    {
        Strength,
        Agility,
        Intellect
    }
    public enum Quality
    {
        Elite,
        ElitePlus,
        Legendary,
        LegendaryPlus,
        Mythic,
        MythicPlus,
        Ascended,
        AscendedOne,
        AscendedTwo,
        AscendedThree,
        AscendedFour,
        AscendedFive
    }

    internal class Character
    {
        

        //Set Properties
        public string CharacterName { get; private set; }
        public Faction CharacterFaction { get; private set; }
        public Attribute CharacterAttribute { get; private set; }
        public ImageSource Thumbnail { get; private set; }
        public string ThumbnailPath { get; private set; }

        //Updatable Properties
        public Quality CharacterQuality { get; private set; }
        public int GearCount { get; private set; }
        public int FurnitureCount { get; private set; }
        public int SignatureLevel { get; private set; }
        public int EngravingLevel { get; private set; }

        //Character Power
        public int powerValue { get; set; } = 0;

        //EventHandler for updates to character values
        public event EventHandler<EventArgs> CharacterUpdated;

        //Optional arguments are for loaded in versus default.
        public Character(string name, string faction, string attribute, string thumbnailPath, string quality = "Elite", int gearCount = 0, int furnCount = 0, int sigLevel = 0, int engLevel = 0)
        {
            //Character Definition
            CharacterName = name;
            CharacterFaction = (Faction)Enum.Parse(typeof(Faction), faction);
            CharacterAttribute = (Attribute)Enum.Parse(typeof(Attribute), attribute);
            ThumbnailPath = thumbnailPath;
            Thumbnail = new BitmapImage(new Uri(thumbnailPath, UriKind.Absolute));

            //Character Advancement
            CharacterQuality = (Quality)Enum.Parse(typeof(Quality), quality);
            GearCount = gearCount;
            FurnitureCount = furnCount;
            SignatureLevel = sigLevel;
            EngravingLevel = engLevel;
        }

        //Character State Modifiers
        public void IncreaseQuality()
        {
            if(CharacterQuality != Quality.AscendedFive)
            {
                CharacterQuality = (Quality)((int)CharacterQuality+1);
                OnCharacterUpdate();
            }
        }
        public void DecreaseQuality()
        {
            if (CharacterQuality != Quality.Elite)
            {
                CharacterQuality = CharacterQuality;
                OnCharacterUpdate();
            }
        }
        public void IncreaseGearCount()
        {
            if (GearCount < 4) 
            { 
                GearCount++; 
                OnCharacterUpdate(); 
            }
        }
        public void DecreaseGearCount()
        {
            if (GearCount > 0) 
            { 
                GearCount--; 
                OnCharacterUpdate(); 
            }
        }
        public void IncreaseFurnitureCount()
        {
            if (FurnitureCount == 0)
            {
                FurnitureCount = 3;
                OnCharacterUpdate();
            }
            else if (FurnitureCount == 3) 
            { 
                FurnitureCount = 9; 
                OnCharacterUpdate(); 
            }   
        }
        public void DecreaseFurnitureCount()
        {
            if (FurnitureCount == 3)
            {
                FurnitureCount = 0;
                OnCharacterUpdate();
            }
            else if (FurnitureCount == 9)
            {
                FurnitureCount = 3;
                OnCharacterUpdate();
            }
        }
        public void IncreaseSignatureLevel()
        {
            if (SignatureLevel == 0)
            {
                SignatureLevel = 10;
                OnCharacterUpdate();
            }
            else if (SignatureLevel == 10)
            {
                SignatureLevel = 20;
                OnCharacterUpdate();
            }
            else if (SignatureLevel == 20)
            {
                SignatureLevel = 30;
                OnCharacterUpdate();
            }
        }
        public void DecreaseSignatureLevel()
        {
            if (SignatureLevel == 10)
            {
                SignatureLevel = 0;
                OnCharacterUpdate();
            }
            else if (SignatureLevel == 20)
            {
                SignatureLevel = 10;
                OnCharacterUpdate();
            }
            else if (SignatureLevel == 30)
            {
                SignatureLevel = 20;
                OnCharacterUpdate();
            }
        }
        public void IncreaseEngravingLevel(int delta = 1)
        {
            if (EngravingLevel + delta <= 60)
            {
                EngravingLevel += delta;
                OnCharacterUpdate();
            }
        }
        public void DecreaseEngravingLevel(int delta = 1)
        {
            if (EngravingLevel - delta >= 0)
            {
                EngravingLevel -= delta;
                OnCharacterUpdate();
            }          
        }

        public void OnCharacterUpdate()
        {
            if(CharacterUpdated != null)
            {
                CharacterUpdated(this, new EventArgs());
            }
        }

        public override string ToString()
        {
            return $"{CharacterName},{CharacterFaction},{CharacterAttribute},{ThumbnailPath},{CharacterQuality},{GearCount},{FurnitureCount},{SignatureLevel},{EngravingLevel}";
        }



    }
}
