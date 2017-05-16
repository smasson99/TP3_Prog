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
    //Toutes les propriétés
    #region:proprietes
    // Constantes et propriétés statiques
    /// <summary>
    /// Booléen indiquant si le joueur est en stade Idle(VRAI) ou non(FAUX)
    /// </summary>
    private bool playerIdle;
    /// <summary>
    /// Variable représentant le language courant le l'interface
    /// </summary>
    private Language currentLanguage;
    //CONSTANTES
    /// <summary>
    /// Représente la largeur de l'écran en pixels (la mienne, libre à vous de la changer)
    /// </summary>
    public const int WIDTH = 1920;
    /// <summary>
    /// Rerpésente la hauteur de l'écran en pixels (la mienne, libre à vous de la changer)
    /// </summary>
    public const int HEIGHT = 1080;
    /// <summary>
    /// Unité représentant la limitte de frame à ne pas dépasser
    /// </summary>
    public const uint FRAME_LIMIT = 60;
    /// <summary>
    /// Réel représentant la vitesse d'accélération du joueur
    /// </summary>
    public float speedBuff = 1.00f;
    /// <summary>
    /// Réel représentant la vitesse de rafraichissement sous forme de multiplicateur
    /// </summary>
    const float DELTA_T = 1.0f / (float)FRAME_LIMIT;
    /// <summary>
    /// La variable aléatoire de la classe
    /// </summary>
    private static Random r = new Random();

    // SFML
    RenderWindow window = null;
    Font font = new Font("Data/emulogic.ttf");
    Text text = null;

    // AUDIO
    /// <summary>
    /// Représente la musique à jouer lorsque la partie est terminée
    /// </summary>
    private static Music gameOver = new Music(@"data//Game_over.wav");

    // Propriétés pour la partie
    /// <summary>
    /// Le début du lancement du jeu
    /// </summary>
    private DateTime debut = DateTime.Now;
    /// <summary>
    /// Le temps d'interval de respawn/spawn des ennemis
    /// </summary>
    private DateTime tempsRespawn = DateTime.Now;
    /// <summary>
    /// Booléen indiquant si le jeu se fait lancer à l'instant(VRAI) ou non(FAUX)
    /// </summary>
    bool isStarting;
    /// <summary>
    /// Entier représentant le temps de jeu en secondes
    /// </summary>
    float totalTime = 0;

    // Il en manque BEAUCOUP

    //Ajout de propriétés C#
    /// <summary>
    /// Propriété C# représentant la vitesse d'accélération du joueur
    /// </summary>
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
    /// <summary>
    /// Propriété C# indiquant si le joueur est en stade IDLE (VRAI) ou ne l'est pas (FAUX)
    /// </summary>
    public bool PlayerIdle
    {
      get
      {
        return playerIdle;
      }
    }

    //Ajout des listes de contenu du jeu
    /// <summary>
    /// Liste représentant l'ensemble des étoiles affichées à l'écran
    /// </summary>
    List<Star> stars = new List<Star>();
    /// <summary>
    /// Liste représentant l'ensemble des projectiles actuellement dans le jeu
    /// </summary>
    List<Projectile> projectiles = new List<Projectile>();
    /// <summary>
    /// Liste représentant l'ensemble des projectiles de bombe actuellement dans le jeu
    /// </summary>
    List<Projectile> projectilesBomb = new List<Projectile>();
    /// <summary>
    /// Liste représentant l'ensemble des projectiles de bombe à détruire actuellement dans le jeu
    /// </summary>
    List<Projectile> projectilesBombADetruire = new List<Projectile>();
    /// <summary>
    /// Liste représentant l'ensemble des projectiles à détruire actuellement dans le jeu
    /// </summary>
    List<Projectile> projectilesADetruire = new List<Projectile>();
    /// <summary>
    /// Liste représentant l'ensemble des particules affichées à l'écran
    /// </summary>
    List<Particle> particules = new List<Particle>();
    /// <summary>
    /// Liste représentant l'ensemble des particules à détruire actuellement affichées à l'écran
    /// </summary>
    List<Particle> particulesADetruire = new List<Particle>();
    /// <summary>
    /// Liste représentant l'ensemble des ennemis actuellement dans le jeu
    /// </summary>
    List<Enemy> ennemis = new List<Enemy>();
    /// <summary>
    /// Liste représentant l'ensemble des ennemis à détruire actuellement dans le jeu
    /// </summary>
    List<Enemy> ennemisADetruire = new List<Enemy>();

    //Ajout du joueur:
    /// <summary>
    /// Instance représentant le joueur dans le jeu (son vaisseau ou sa forme)
    /// </summary>
    public Hero hero = new Hero(WIDTH/2, HEIGHT/2);
    #endregion
    //Toutes les méthodes
    #region:methodes
    /// <summary>
    /// Fonction dont le rôle est de s'assurer que tout soit bien fermé si l'écran est fermée
    /// </summary>
    private void OnClose(object sender, EventArgs e)
    {
      RenderWindow window = (RenderWindow)sender;
      window.Close();
    }
    /// <summary>
    /// Fonction dont le rôle est de déterminer certaines actions en fonction des touches entrées au clavier
    /// </summary>
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
    /// <summary>
    /// Constructeur dont le rôle est d'initialiser les variables de base du jeu
    /// </summary>
    public GW()
    {
      //Initialisation des variables
      isStarting = true;
      gameOver.Volume = 20.00f;
      //Préparation de la fenêtre
      text = new Text("", font); 
      window = new RenderWindow(new SFML.Window.VideoMode(WIDTH, HEIGHT), "GW");
      window.Closed += new EventHandler(OnClose);
      window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
      window.SetKeyRepeatEnabled(false);
      window.SetFramerateLimit(FRAME_LIMIT);
    }
    /// <summary>
    /// Fonction dont le rôle est de lancer le jeu
    /// </summary>
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
        //Tant que la fenêtre est ouverte
        while (window.IsOpen)
        {
          //Afficher le contenu à l'écran
          window.Clear(Color.Black);
          window.DispatchEvents();
          if (false == Update())
            break;
          Draw();
          window.Display();
        }
      }
    }
    /// <summary>
    /// Fonction dont le rôle est d'afficher le contenu à l'écran
    /// </summary>
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
      foreach (Projectile projectile in projectilesBomb)
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
    /// <summary>
    /// Fonction dont le rôle est d'ajouter un certain nombre d'ennemis basiques et normaux au jeu
    /// </summary>
    /// <param name="nbNormalEnemies">Le nombre d'ennemis normaux à ajouter au jeu</param>
    /// <param name="nbBasics">Le nombre d'ennemis basiques à ajouter au jeu</param>
    private void SpawnEnemies(int nbNormalEnemies, int nbBasics)
    {
      //Initialisation des variables
      int nbBasicsCourants = 0;
      int nbSquares = 0;
      int nbCircles = 0;
      //Compter le nombre d'ennemis selon leur type
      foreach (Enemy ennemi in ennemis)
      {
        if (ennemi is BasicEnemy)
        {
          nbBasicsCourants++;
        }
        else if (ennemi is Square)
        {
          nbSquares++;
        }
        else if (ennemi is Circle)
        {
          nbCircles++;
        }
      }
      //S'assurer qu'il ne manque pas d'ennemis basiques sur le terrain
      if (nbBasicsCourants < nbBasics)
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
      //S'assurer qu'il y ait suffisament assez d'ennemis courants au jeu
      if (nbNormalEnemies-(nbSquares) > 0)
      {
        //Initialisation des positions
        Vector2f[] startPositions = new Vector2f[] { new Vector2f(0, r.Next(0, HEIGHT)), new Vector2f(WIDTH, r.Next(0, HEIGHT)),
        new Vector2f(r.Next(0, WIDTH), 0), new Vector2f(r.Next(0, WIDTH), HEIGHT) };
        //Lister les ennemis normaux possibles:
        EnemyType[] ennemisPossibles = new EnemyType[] { EnemyType.SQUARE, EnemyType.CIRCLE};
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
        int indexREnemy = r.Next(0, ennemisPossibles.Length);
        //Dans le cas où le random a choisi le carré, ajouter un carré
        if (ennemisPossibles[indexREnemy] == EnemyType.SQUARE)
        {
          ennemis.Add(new Square(position.X, position.Y, angle));
        }
        //Dans le cas où le random a choisi le cercle, s'assurer qu'il n'y en ait pas plus que deux
        else if (nbCircles < 2 && ennemisPossibles[indexREnemy] == EnemyType.CIRCLE)
        {
          ennemis.Add(new Circle(position.X, position.Y, angle));
        }
      }
    }
    /// <summary>
    /// Fonction dont le rôle est d'ajouter un projectile de bombe à la liste des 
    /// projectiles de bombe du jeu
    /// </summary>
    /// <param name="bombProjectile">Le projectile à ajouter</param>
    public void AddBomb(Projectile bombProjectile)
    {
      //Ajouter le projectile à la liste des projectiles
      projectilesBomb.Add(bombProjectile);
    }
    /// <summary>
    /// Fonction dont le rôle est d'ajouter un projectile à la
    /// liste des projectiles courants
    /// </summary>
    /// <param name="projectile">Le projectile à ajouter</param>
    public void AddProjectile(Projectile projectile)
    {
      //Ajouter le projectile à la liste des projectiles
      projectiles.Add(projectile);
    }
    /// <summary>
    /// Fonction dont le rôle est d'ajouter une particules au jeu
    /// </summary>
    /// <param name="particule">La particule à ajouter</param>
    public void AddParticle(Particle particule)
    {
      //Ajouter la particule à la liste des particules
      particules.Add(particule);
    }
    /// <summary>
    /// Fonction dont le rôle est d'updater le jeu et tous ses composants
    /// </summary>
    /// <returns></returns>
    public bool Update()
    {
      #region Init
      //Vider de la liste des projectiles les projectiles correspondants à ceux qui doivent être détruits, idem 
      //pour les listes de particules, d'ennemis et des projectiles de bombes
      foreach (Projectile toDelete in projectilesADetruire)
      {
        if (projectilesADetruire.Contains(toDelete))
        {
          projectiles.Remove(toDelete);
        }
      }
      foreach (Projectile toDelete in projectilesBombADetruire)
      {
        if (projectilesBombADetruire.Contains(toDelete))
        {
          projectilesBomb.Remove(toDelete);
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
      projectilesBombADetruire = new List<Projectile>();
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
        speedBuff = 0.50f;
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
          hero.Life = hero.Life - r.Next(15, 22+1);
          ennemi.PlayDeath(this);
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
            ennemi.PlayDeath(this);
          }
        }
      }
      //Collisions des projectiles de bombe du joueur avec les ennemis
      foreach (Projectile projectile in projectilesBomb)
      {
        foreach (Enemy ennemi in ennemis)
        {
          if (projectile.Intersects(ennemi))
          {
            ennemisADetruire.Add(ennemi);
            ennemi.PlayDeath(this);
          }
        }
      }
      //Collisions des projectiles de l'ennemi avec le joueur
      foreach (Projectile projectile in projectiles)
      {
        if (projectile.Type == CharacterType.ENNEMI && projectile.Intersects(hero))
        {
          hero.Life -= r.Next(1, 15+1);
          projectilesADetruire.Add(projectile);
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
      //Projectiles de Bombe
      foreach (Projectile projectile in projectilesBomb)
      {
        bool nePasDetruire = projectile.Update(DELTA_T, this);
        if (nePasDetruire == false)
        {
          projectilesBombADetruire.Add(projectile);
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
        bool nePasDetruire = particule.Update(DELTA_T, this);
        if (nePasDetruire == false)
        {
          particulesADetruire.Add(particule);
        }
      }
      #endregion
      #region Spawning des nouveaux ennemis
      // On veut avoir au minimum 5 ennemis (n'incluant pas les triangles). Il faut les ajouter ici
      if (isStarting)
      {
        isStarting = false;
        SpawnEnemies(5, 2);
      }
      #endregion
      #region Ajouts
      // Ajouts des projectiles, ennemis, etc
      if (DateTime.Now >= tempsRespawn)
      {
        tempsRespawn = DateTime.Now.AddSeconds(r.Next(4, 8));
        SpawnEnemies(5, 2);
      }
      foreach (Enemy ennemi in ennemisADetruire)
      {
        if (ennemi is Square)
        {
          for (int i = 0; i < 3; i++) 
            ennemis.Add(new BasicEnemy(ennemi.Position.X, ennemi.Position.Y, ennemi.Angle));
        }
        else if (ennemi is Circle)
        {
          for (int i = 0; i < 1; i++)
            ennemis.Add(new BasicEnemy(ennemi.Position.X, ennemi.Position.Y, ennemi.Angle));
        }
      }
      #endregion
      // Retourner true si le héros est en vie, false sinon.
      if (hero.IsAlive == false)
        gameOver.Play();
      return hero.IsAlive;
    }
    #endregion
  }
}