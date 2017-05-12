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
    private string file;
    private Dictionary<string, string> dictionnaire;
    private static StringTable instance;
    private Language[] laguages = new Language[] { Language.English, Language.French };

    //Méthodes
    public static StringTable GetInstance()
    {
        if (instance == null)
            instance = new StringTable();
        return instance;
    }
    private StringTable()
    {
      dictionnaire = new Dictionary<string, string>();
    }
    public ErrorCode Parse(string readFile)
    {
      try
      {
        File.Exists(readFile);
      }
      catch (NotSupportedException)
      {
        return ErrorCode.BAD_FILE_FORMAT;
        throw;
      }
      ErrorCode reaction = ErrorCode.OK;
      if (readFile == null || readFile.Length == 0)
      {
        return ErrorCode.MISSING_FIELD;
      }
      else
      {
        //Initialisation des variables nécessaires
        string[] lignes = readFile.Split( new string[] {"ID_", "\r\n"}, StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 0; i < lignes.Length; i++)
        {
          //Trie du tableau pour trouver l'ID courant
          string ligneActuelle = lignes[i];
          string idCourant = "ID_" + ligneActuelle.Substring(0, ligneActuelle.IndexOf('='));
          //Raccourcir la chaine
          int index = ligneActuelle.IndexOf('>') + 1;
          ligneActuelle = ligneActuelle.Substring(index, ligneActuelle.Length - index);
          //Ajouter au dictionnaire la chaine selon l'ID
          dictionnaire.Add(idCourant, ligneActuelle);
        }
      }
      return reaction;
    }
    public string GetValue(Language currentlanguage, string id)
    {
      string ligneVoulue = dictionnaire[id];
      string[] motTraduction = ligneVoulue.Split(new string[] {"---"}, StringSplitOptions.RemoveEmptyEntries);
      return motTraduction[(int)currentlanguage];
    }
  }
}
