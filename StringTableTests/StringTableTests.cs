using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP3;

namespace StringTableTests
{
  /// <summary>
  /// Tous les tests de la classe StringTable
  /// </summary>
  [TestClass]
  public class StringTableTests
  {
    //Les tets normaux
    #region:testsNormaux
    /// <summary>
    /// Cas où le fichier se fait bien interprêter
    /// </summary>
    [TestMethod]
    public void StringTable_Normal01()
    {
      //Initialiser les variables
      ErrorCode resultat = StringTable.GetInstance().Parse(File.ReadAllText("Data/st.txt"));
      //Valider
      Assert.AreEqual(ErrorCode.OK, resultat);
      //Clean Up
      //Rien à faire!
    }
    /// <summary>
    /// Cas où un fichier serait mal entré
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void StringTable_Normal02()
    {
      //Initialiser les variables
      ErrorCode resultat = StringTable.GetInstance().Parse(File.ReadAllText("Data/LE_PETIT_ROBERT_JUNIOR.tttytt"));
      //Valider
      Assert.AreEqual(ErrorCode.BAD_FILE_FORMAT, resultat);
      //Clean Up
      //Rien à faire!
    }
    /// <summary>
    /// Cas où le champ pour l'emplacement du fichier serait vide
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void StringTable_Normal03()
    {
      //Initialiser les variables
      ErrorCode resultat = StringTable.GetInstance().Parse(File.ReadAllText(""));
      //Valider
      Assert.AreEqual(ErrorCode.MISSING_FIELD, resultat);
      //Clean Up
      //Rien à faire!
    }
    #endregion
    //Les tests spécifiques
    #region:testSpecifiques
    /// <summary>
    /// Cas où un l'item est null
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void StringTable_Specifique01()
    {
      //Initialiser les variables
      ErrorCode resultat = StringTable.GetInstance().Parse(File.ReadAllText(null));
      //Valider
      Assert.AreEqual(ErrorCode.MISSING_FIELD, resultat);
      //Clean Up
      //Rien à faire!
    }
    /// <summary>
    /// Cas où un item est manquant
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void StringTable_Specifique02()
    {
      //Je ne sais pas ce que vous entendez par "item manquant"
    }
    /// <summary>
    /// Cas où deux items sont manquants
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void StringTable_Specifique03()
    {
      //Je ne sais pas ce que vous entendez par "item manquant"
    }
    #endregion
    //Les tests de GetValue
    #region:testsGetValue
    /// <summary>
    /// Cas où l'indentifiant entré est valide
    /// </summary>
    [TestMethod]
    public void StringTable_GetValue01()
    {
      //Initialiser les variables
      string resultat = StringTable.GetInstance().GetValue(Language.French, "ID_TOTAL_TIME");
      //Valider
      Assert.AreEqual("Temps total", resultat);
      //Clean Up
      //Rien à faire!
    }
    /// <summary>
    /// Cas où l'indentifiant entré est invalide
    /// </summary>
    [TestMethod]
    public void StringTable_GetValue02()
    {
      //Initialiser les variables
      string resultat = StringTable.GetInstance().GetValue(Language.French, "ID_BABLAAKAJLA");
      //Valider
      Assert.AreEqual("ID INVALIDE", resultat);
      //Clean Up
      //Rien à faire!
    }
    #endregion
  }
}
