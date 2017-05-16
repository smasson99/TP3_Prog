using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace TP3
{
    public class StringTable
    {
    //Toutes les propriétés
    #region:proprietes
    /// <summary>
    /// Représente l'unique instance de la classe
    /// </summary>
    private static StringTable instance;
    /// <summary>
    /// Propriété représentant le dictionnaire des mots à traduire
    /// </summary>
    private Dictionary<string, string> dictionnaire;
    /// <summary>
    /// Tableau de languages listant toutes les langues disponibles pour la 
    /// traduction
    /// </summary>
    private Language[] laguages = new Language[] { Language.English, Language.French };
    #endregion

    //Toutes les méthodes
    #region:methodes
    /// <summary>
    /// Constructeur dont le rôle est de s'assurer qu'il n'y ait pas plus d'une seule instance 
    /// à la fois
    /// </summary>
    /// <returns>L'unique instance de la classe</returns>
    public static StringTable GetInstance()
    {
      //S'assurer que l'instance de la classe soit unique
      if (instance == null)
          instance = new StringTable();
      return instance;
    }
    /// <summary>
    /// Constructeur dont le rôle est d'initialiser les variables de base
    /// </summary>
    private StringTable()
    {
      //Initialiser les variables de base
      dictionnaire = new Dictionary<string, string>();
    }
    /// <summary>
    /// Fonction dont le but est de traiter un fichier texte entré en paramètre et de 
    /// retourner un message d'erreur convenant aux critères non-respectés
    /// </summary>
    /// <param name="readFile">Lien du fichier à lire</param>
    /// <returns>Le code d'erreur suite au traitement</returns>
    public ErrorCode Parse(string readFile)
    {
      try
      {
        File.Exists(readFile);
      }
      catch (FileNotFoundException e)
      {
        return ErrorCode.BAD_FILE_FORMAT;
        throw(e);
      }
      catch (ArgumentException e)
      {
        return ErrorCode.MISSING_FIELD;
        throw(e);
      }
      ErrorCode reaction = ErrorCode.OK;
      if (readFile.Length == 0)
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
    /// <summary>
    /// Méthode dont le rôle est d'accéder au dictionnaire et de retourner le mot en fonction 
    /// de la langue entrée en paramètre
    /// </summary>
    /// <param name="currentlanguage">Représente la langue du mot désiré</param>
    /// <param name="id">Représente l'ID du mot recherché</param>
    /// <returns>Le mot selon la langue entrée</returns>
    public string GetValue(Language currentlanguage, string id)
    {
      try
      {
        //Trouver la ligne voulue
        string ligneVoulue = dictionnaire[id];
        //Initialiser le tableau contenant le mot désiré, dans toutes les langues disponibles
        string[] motTraduction = ligneVoulue.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries);
        //Retourner le mot selon la langue entrée en paramètre
        return motTraduction[(int)currentlanguage];
      }
      catch (KeyNotFoundException e)
      {
        return "ID INVALIDE";
        throw(e);
      }
    }
    #endregion
  }
}
