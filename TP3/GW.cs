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
    private bool playerIdle;
    private Language currentLanguage;
    public const int WIDTH = 1024;
    public const int HEIGHT = 768;
    public const uint FRAME_LIMIT = 60;
    public float speedBuff = 1.00f;
    const float DELTA_T = 1.0f / (float)FRAME_LIMIT;
    private static Random r = new Random();

    // SFML
    RenderWindow window = null;
    Font font = new Font("Data/emulogic.ttf");
    Text text = null;

    // Propriétés pour la partie
    private DateTime debut = DateTime.Now;
    float totalTime = 0;

    // Il en manque BEAUCOUP

    //Ajout de propriétés C#
    public float SpeedBuff
    {
      get
      {
        return speedBuff;
      }
      set
      {
        speedBuff = value;
      }
    }
    public bool PlayerIdle
    {
      get
      {
        return playerIdle;
      }
    }

    //Ajout des listes du contenu du jeu
    List<Star> stars = new List<Star>();
    List<Projectile> projectiles = new List<Projectile>();
    List<Projectile> projectilesADetruire = new List<Projectile>();
    List<Particle> particules = new List<Particle>();
    List<Particle> particulesADetruire = new List<Particle>();
    List<Enemy> ennemis = new List<Enemy>();
    List<Enemy> ennemisADetruire = new List<Enemy>();

    //Ajout du joueur:
    public Hero hero = new Hero(WIDTH/2, HEIGHT/2);

    private void OnClose(object sender, EventArgs e)
    {
      RenderWindow window = (RenderWindow)sender;
      window.Close();
    }
    
    private void OnKeyPressed(object sender, KeyEventArgs e)
    {
      if (e.Code == Keyboard.Key.F4)
      {
        currentLanguage = Language.French;
      }
      else if (e.Code == Keyboard.Key.F5)
      {
        currentLanguage = Language.English;
      }
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
      //Ajout des étoiles dans le jeu
      for (int i=0; i < 250; i++)
      {
        stars.Add(new Star((r.Next(0, WIDTH)), r.Next(0, r.Next(0, HEIGHT)), DELTA_T));
      }
      // ppoulin
      // Chargement de la StringTable. A décommenter au moment opportun
      if( ErrorCode.OK == StringTable.GetInstance().Parse(File.ReadAllText("Data/st.txt")) )
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
      // Parcourir les listes appropriées pour faire afficher les éléments souhaités.
      foreach (Star etoile in stars)
      {
       etoile.Draw(window);
      }
      foreach (Projectile projectile in projectiles)
      {
        projectile.Draw(window);
      }
      foreach (Particle particule in particules)
      {
        particule.Draw(window);
      }
      //Afficher le héro:
      hero.Draw(window);
      //Afficher les ennemis
      foreach (Enemy ennemi in ennemis)
      {
        ennemi.Draw(window);
      }

      // Affichage des statistiques:
      // Temps total
      text.Position = new Vector2f(0, 10);
      text.DisplayedString = string.Format("{1} = {0,-5}", ((int)(totalTime/100)).ToString(), StringTable.GetInstance().GetValue(currentLanguage, "ID_TOTAL_TIME"));
      window.Draw(text);

      // Points de vie
      text.Position = new Vector2f(0, 50);
      text.DisplayedString = string.Format("{1} = {0,-4}", hero.Life.ToString(), StringTable.GetInstance().GetValue(currentLanguage,"ID_LIFE"));
      window.Draw(text);
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

    private void SpawnEnemies(int nbNormalEnemies, int nbBasics)
    {
      //Vérifier d'abord si le nombre d'ennemis non-basiques conviennent à 5
      //Initialisation des variables
      int basicsCourants = 0;
      foreach (Enemy ennemi in ennemis)
      {
        if (ennemi is BasicEnemy)
        {
          basicsCourants++;
        }
      }
      if (basicsCourants < nbBasics)
      {
        //Initialisation des positions
        Vector2f[] startPositions = new Vector2f[] { new Vector2f(0, r.Next(0, HEIGHT)), new Vector2f(WIDTH, r.Next(0, HEIGHT)),
        new Vector2f(r.Next(0, WIDTH), 0), new Vector2f(r.Next(0, WIDTH), HEIGHT) };
        //Trouver l'angle de l'ennemi et sa position aléatoire
        Vector2f position = startPositions[r.Next(0, startPositions.Length)];
        float angle;
        if (position.X == 0)
          angle = 0.00f;
        else if (position.X == WIDTH)
          angle = -180.0f;
        else if (position.Y == 0)
          angle = 90f;
        else //position.Y == HEIGHT
          angle = -90;
        ennemis.Add(new BasicEnemy(position.X, position.Y, angle));
      }
    }

    public void AddBomb()
    {
      
    }

    /// <summary>
    /// Fonction dont le rôle est d'ajouter un projectile à la
    /// liste des projectiles courants
    /// </summary>
    /// <param name="projectile"></param>
    public void AddProjectile(Projectile projectile)
    {
      projectiles.Add(projectile);
    }
    public void AddParticle(Particle particule)
    {
      particules.Add(particule);
    }

    public bool Update()
    {
      // A compléter
      #region Init
      // Vidage de toutes les listes contenant les ennemis et projectiles à ajouter et enlever.
      foreach (Projectile toDelete in projectilesADetruire)
      {
        if (projectilesADetruire.Contains(toDelete))
        {
          projectiles.Remove(toDelete);
        }
      }
      foreach (Particle toDelete in particulesADetruire)
      {
        if (particulesADetruire.Contains(toDelete))
          particules.Remove(toDelete);
      }
      foreach (Enemy toDelete in ennemisADetruire)
      {
        if (ennemisADetruire.Contains(toDelete))
        {
          ennemis.Remove(toDelete);
        }
      }
      //Sauver la mémoire en rénitialisant la liste
      projectilesADetruire = new List<Projectile>();
      particulesADetruire = new List<Particle>();
      ennemisADetruire = new List<Enemy>();
      #endregion
      #region Utilisation des bombes
      // Écrire le code pertinent pour faire exploser les bombes
      #endregion
      #region Updates
      //Le décompte du jeu
      if (DateTime.Now >= debut.AddSeconds(1))
      {
        totalTime++;
      }
      // Étoiles 
      if (Keyboard.IsKeyPressed(Keyboard.Key.Up) || Keyboard.IsKeyPressed(Keyboard.Key.Down) || Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.S))
      {
        speedBuff = 4.50f;
        playerIdle = false;
      }
      else if (Keyboard.IsKeyPressed(Keyboard.Key.Left) || Keyboard.IsKeyPressed(Keyboard.Key.Right) || Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.D))
        speedBuff = 0.50f;
      else
      {
        playerIdle = true;
        speedBuff = 1.00f;
      }
      foreach (Star etoile in stars)
      {
        etoile.Update(SpeedBuff, hero.Direction);
      }
      #endregion
      #region Gestion des collisions
      //Collisions du joueur avec les ennemis
      foreach (Enemy ennemi in ennemis)
      {
        if (hero.Intersects(ennemi))
        {
          ennemisADetruire.Add(ennemi);
          hero.Life = hero.Life - 10;
          ennemi.PlayDeath();
        }
      }
      //Collisions des projectiles du joueur avec les ennemis
      foreach (Projectile projectile in projectiles)
      {
        foreach (Enemy ennemi in ennemis)
        {
          if (projectile.Type == CharacterType.HERO && projectile.Intersects(ennemi))
          {
            ennemisADetruire.Add(ennemi);
            projectilesADetruire.Add(projectile);
            ennemi.PlayDeath();
          }
        }
      }
      #endregion
      #region Retraits
      // Projectiles
      foreach (Projectile projectile in projectiles)
      {
        bool nePasDetruire = projectile.Update(DELTA_T, this);
        if (nePasDetruire == false)
        {
          projectilesADetruire.Add(projectile);
        }
      }
      //Personnages
      hero.Update(DELTA_T, this);
      foreach (Enemy ennemi in ennemis)
      {
        bool nePasDetruire = ennemi.Update(DELTA_T, this);
        if (nePasDetruire == false)
        {
          ennemis.Add(ennemi);
        }
      }
      //Particules
      foreach (Particle particule in particules)
      {
        bool nePasDetruire = particule.Update(this);
        if (nePasDetruire == false)
        {
          particulesADetruire.Add(particule);
        }
      }
      #endregion
      #region Spawning des nouveaux ennemis
      // On veut avoir au minimum 5 ennemis (n'incluant pas les triangles). Il faut les ajouter ici
      SpawnEnemies(5, 2);
      #endregion
      #region Ajouts
      // Ajouts des projectiles, ennemis, etc
      #endregion

      // Retourner true si le héros est en vie, false sinon.
      return hero.IsAlive;
    }
  }
}