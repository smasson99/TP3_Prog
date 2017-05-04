using System;
using System.IO;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System.Collections.Generic;
namespace TP3
{
  public class GW
  {
    // Constantes et propriétés statiques
    public const int WIDTH = 1024;
    public const int HEIGHT = 768;
    public const uint FRAME_LIMIT = 60;
    const float DELTA_T = 1.0f / (float)FRAME_LIMIT;
    private static Random r = new Random();

    // SFML
    RenderWindow window = null;
    Font font = new Font("Data/emulogic.ttf");
    Text text = null;

    // Propriétés pour la partie
    float totalTime = 0;    
        
    // Il en manque BEAUCOUP

    
    private void OnClose(object sender, EventArgs e)
    {
      RenderWindow window = (RenderWindow)sender;
      window.Close();
    }
    
    private void OnKeyPressed(object sender, KeyEventArgs e)
    {

    }

    public GW()
    {
      text = new Text("", font); 
      window = new RenderWindow(new SFML.Window.VideoMode(WIDTH, HEIGHT), "GW");
      window.Closed += new EventHandler(OnClose);
      window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
      window.SetKeyRepeatEnabled(false);
      window.SetFramerateLimit(FRAME_LIMIT);
      
      
    }


    public void Run()
    {
      // ppoulin
      // Chargement de la StringTable. A décommenter au moment opportun
      //if( ErrorCode.OK == StringTable.GetInstance().Parse(File.ReadAllText("Data/st.txt")) )
      {
        window.SetActive();
        
        while (window.IsOpen)
        {
          window.Clear(Color.Black);
          window.DispatchEvents();
          if (false == Update())
            break;
          Draw();
          window.Display();
        }
      }
    }
    
    public void Draw()
    {
       


       // Parcourez les listes appropriées pour faire afficher les éléments demandés.

       

      // Affichage des statistiques. A décommenter au moment opportun
      // Temps total
      //text.Position = new Vector2f(0, 10);
      //text.DisplayedString = string.Format("{1} = {0,-5}", ((int)(totalTime)).ToString(), StringTable.GetInstance().GetValue(CurrentLanguage, "ID_TOTAL_TIME"));
      //window.Draw(text);

      // Points de vie
      //text.Position = new Vector2f(0, 50);
      //text.DisplayedString = string.Format("{1} = {0,-4}", hero.Life.ToString(), StringTable.GetInstance().GetValue(CurrentLanguage,"ID_LIFE"));
      //window.Draw(text);
    }

    /// <summary>
    /// Détermine si un Movable est situé à l'intérieur de la surface de jeu
    /// Peut être utilisée pour déterminer si les projectiles sont sortis du jeu
    /// ou si le héros ou un ennemi s'apprête à sortir.
    /// </summary>
    /// <param name="m">Le Movable à tester</param>
    /// <returns>true si le Movable est à l'intérieur, false sinon</returns>
    public bool Contains(Movable m)
    {
      FloatRect r = new FloatRect(0, 0, GW.WIDTH, GW.HEIGHT);
      return r.Contains(m.Position.X, m.Position.Y);
    }

    private void SpawnEnemies(int nbEnemies)
    {
      // A compléter
    }

    public void AddBomb()
    {
      
    }

    public bool Update()
    {
      // A compléter
      #region Init
      // Vidage de toutes les listes contenant les ennemis et projectiles à ajouter et enlever.
      #endregion
      #region Utilisation des bombes
      // Écrire le code pertinent pour faire exploser les bombes
      #endregion
      #region Updates
      // Étoiles      

      // Personnages et projectiles      
      
      #endregion
      #region Gestion des collisions
      #endregion           
      #region Retraits
      // Retrait des ennemis détruits et des projectiles inutiles
      #endregion
      #region Spawning des nouveaux ennemis
      // On veut avoir au minimum 5 ennemis (n'incluant pas les triangles). Il faut les ajouter ici
      #endregion
      #region Ajouts
      // Ajouts des projectiles, ennemis, etc
      #endregion
      
      // ppoulin 
      // A COMPLETER
      // Retourner true si le héros est en vie, false sinon.
      return true;
    }
  }
}
