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
    public static float HeroSpeed = 0.2f;
    static Color HeroColor = Color.Green;
    //Propriétés privées
    private bool isAlive;
    private int life;
    private int nbBombs;
    private Music soundBomb;
    private static Random rnd = new Random();
    //Propriétés publiques
    public const int LIFE_AT_BEGINING = 8000;
    
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
      
      ////Initialisation visuelle du joueur
      this[0]= new Vector2f(-6,20);
      this[1]= new Vector2f(50, 0);
      this[2]= new Vector2f(-6,-20);
    }
    public bool Update(Single deltaT, GW gw)
    {
      //A COMPLETER
      //Initialisation de l'effet de particule
      gw.AddParticle(new Particle(Position.X-12.50f, Position.Y-10.50f, 4, new Color(HeroColor.R, HeroColor.G, HeroColor.B, (byte)rnd.Next(25, 225+1)), 25.00f, true, 0.07f));
      if (gw.PlayerIdle)
      {
        Advance(2);
      }
      if (Keyboard.IsKeyPressed(Keyboard.Key.Up) || Keyboard.IsKeyPressed(Keyboard.Key.W))
      {
        Advance(4.50f);
      }
      if (Keyboard.IsKeyPressed(Keyboard.Key.Down) || Keyboard.IsKeyPressed(Keyboard.Key.S))
      {
        Advance(-4.50f);
      }
      if (Keyboard.IsKeyPressed(Keyboard.Key.Left) || Keyboard.IsKeyPressed(Keyboard.Key.A))
      {
        Rotate(-4.50f);
      }
      if (Keyboard.IsKeyPressed(Keyboard.Key.Right) || Keyboard.IsKeyPressed(Keyboard.Key.D))
      {
        Rotate(4.50f);
      }
      if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
      {
        Fire(gw, deltaT);
      }
      return isAlive;
    }
    private void FireBomb(GW gw)
    {
      //A COMPLETER
    }
  }
}
