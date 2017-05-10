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
          //En fonction de la langue, retourner le mot qui convient
          if ((int)currentlanguage == 0) //Français
          {
            int index = lignes[position].IndexOf('>')+1;
            resultat = lignes[position].Substring(index, lignes[position].Length-index);
            int index2 = resultat.IndexOf('-');
            resultat = resultat.Substring(0, index2);
          }
          else if ((int)currentlanguage == 1) //Anglais
          {
            int index = lignes[position].IndexOf('>') + 1;
            resultat = lignes[position].Substring(index, lignes[position].Length - index);
            int index2 = resultat.LastIndexOf('-')+1;
            resultat = resultat.Substring(index2, resultat.Length-index2);
          }
        }
      }
      return resultat;
    }
  }
}
