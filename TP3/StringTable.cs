using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using System.Drawing;

namespace TP3
{
    public class StringTable
    {
        //Propriétés
        private static StringTable instance;
        private Language languageCourant;
        private string file = (@"data//st.txt");
        public static StringTable GetInstance()
        {
            if (instance == null)
                instance = new StringTable();
            return instance;
        }
        private StringTable()
        {

        }
        //public string GetValue(Language currentLanguage, string iD)
        //{
        //    string trier = file;
        //    string resultat = 

        //}
    }
}
