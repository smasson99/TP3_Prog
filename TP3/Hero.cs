using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
namespace TP3
{
  public class Hero:Character
  {
    #region:Propriétés
    //Propriétés statiques
    public static float HeroSpeed = 0.20f;
    static Color HeroColor = Color.Cyan;
    private static Random rnd = new Random();
    //Propriétés privées
    private bool isAlive;
    private int life;
    private int nbBombs;
    private Music soundBomb;
    private DateTime bombReload;
    //Propriétés publiques et constantes
    public const int LIFE_AT_BEGINING = 3500;
    
    //Propriétés C#
    /// <summary>
    /// Propriété C# représentant la vie restante du joueur 
    /// </summary>
    public int Life
    {
      get
      {
        return life;
      }
      set
      {
        life = value;
      }
    }
    /// <summary>
    /// Propriété C# en lecture seule indiquant si le joueur est en vie ou non
    /// </summary>
    public override bool IsAlive
    {
      get
      {
        return isAlive;
      }
    }
    #endregion

    /// <summary>
    /// Constructeur dont le rôle est d'instancier le joueur
    /// </summary>
    /// <param name="posX">Position en X du joueur</param>
    /// <param name="posY">Position en Y du joueur</param>
    public Hero(Single posX, Single posY)
    :base(posX, posY, 3, HeroColor, HeroSpeed,CharacterType.HERO)
    {
      //Initialisation des varibles de base
      life = LIFE_AT_BEGINING;
      isAlive = true;
      nbBombs = 3;
      soundBomb = new Music(@"data//Fire_smartbomb.wav");
      soundBomb.Volume = 20.00f;
      bombReload = DateTime.Now;
      
      //Initialisation visuelle du joueur
      this[0]= new Vector2f(-7,20);
      this[1]= new Vector2f(55, 0);
      this[2]= new Vector2f(-7,-20);
    }
    /// <summary>
    /// Fonction dont le rôle est d'interprêter les commandes entrées par l'utilisateur et
    /// de mettre le joueur à jour en fonction de celles-cis. Le visuel et du joueur et la 
    /// validation du booléen "isAlive" reposent aussi sur cette fonction.
    /// </summary>
    /// <param name="deltaT">Vitesse de rafraichissement du jeu</param>
    /// <param name="gw">Représente l'instance du jeu</param>
    /// <returns>Booléen indiquant si le joueur est en vie(TRUE) ou non(FALSE)</returns>
    public bool Update(Single deltaT, GW gw)
    {
      //Initialisation de l'effet de particule
      gw.AddParticle(new Particle(Position.X, Position.Y, 4, new Color(HeroColor.R, HeroColor.G, HeroColor.B,
      (byte)rnd.Next(25, 255 + 1)), 5.35f, 1.25f, -rnd.Next(180-(int)Angle-5, 180-(int)Angle+5 + 1)));
      //Passif
      if (gw.PlayerIdle)
      {
        Advance(2);
      }
      //Accélération
      if (Keyboard.IsKeyPressed(Keyboard.Key.Up) || Keyboard.IsKeyPressed(Keyboard.Key.W))
      {
        Advance(4.50f);
      }
      //Reculon
      if (Keyboard.IsKeyPressed(Keyboard.Key.Down) || Keyboard.IsKeyPressed(Keyboard.Key.S))
      {
        Advance(-4.50f);
      }
      //Rotation anti-horaire
      if (Keyboard.IsKeyPressed(Keyboard.Key.Left) || Keyboard.IsKeyPressed(Keyboard.Key.A))
      {
        Rotate(-4.50f);
      }
      //Rotation horaire
      if (Keyboard.IsKeyPressed(Keyboard.Key.Right) || Keyboard.IsKeyPressed(Keyboard.Key.D))
      {
        Rotate(4.50f);
      }
      //Tir
      if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
      {
        Fire(gw, deltaT);
      }
      //Lancé de bombe
      if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
      {
        FireBomb(gw);
      }
      //Vérifier si le joueur est toujours en vie
      isAlive = this.Life > 0;
      return isAlive;
    }
    /// <summary>
    /// Fonction dont le rôle est de lancer une bombe autour du joueur et de détruire tous les ennemis courants
    /// </summary>
    /// <param name="gw">Instance représentant l'instance du jeu</param>
    private void FireBomb(GW gw)
    {
      if (bombReload < DateTime.Now && nbBombs > 0)
      {
        soundBomb.Play();
        for (int i = 0; i < 360; i++)
        {
          gw.AddBomb(new Projectile(CharacterType.HERO, Position.X, Position.Y, 4, new Color(HeroColor.R, HeroColor.G, HeroColor.B,
          (byte)rnd.Next(25, 255 + 1)), 7.50f, 3.50f, i));
        }
        nbBombs--;
        bombReload = DateTime.Now.AddSeconds(1.5f);
      }
    }
  }
}
