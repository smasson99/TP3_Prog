using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

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
    public string GetValue(Language currentlanguage, string id)
    {
      //Initialisation des variables nécessaires
      string[] lignes = File.ReadAllLines(file);
      string resultat = "";
      //Trie du tableau pour trouver l'ID correspondant
      for (int position = 0; position < lignes.Length; position++)
      {
        if (lignes[position].Substring(0, lignes[position].IndexOf('=')) == id)
        {
          if ((int)currentlanguage == 0) //Français
          {
            int length = lignes[position].Length;
            int pos01 = lignes[position].IndexOf('>') +1;
            int pos02 = lignes[position].IndexOf('-') - 1;
            //resultat = lignes[position].Substring(lignes[position].IndexOf('>') + 1, (lignes[position].IndexOf('-')-1));
          }
          else if ((int)currentlanguage == 1) //Anglais
          {
            resultat = lignes[position].Substring(lignes[position].LastIndexOf('-') + 1, lignes[position].Length - 1);
          }
        }
      }
      

      return resultat; 

    }
  }
}
