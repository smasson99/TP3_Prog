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
    public static float HeroSpeed = 0.2f;
    private bool isAlive;
    public const int LIFE_AT_BEGINING = 8000;
    private int life;
    //private ConvexShape shape;

    //private double length;
    //private double height;

    static Color HeroColor = Color.Green;
    private int nbBombs;
    Music soundBomb;

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
    public override bool IsAlive
    {
      get
      {
        return isAlive;
      }
    }


    public Hero(Single posX, Single posY)
    :base(posX, posY, 3, HeroColor, HeroSpeed,CharacterType.HERO)
    {
      //Initialisation des varibles de base
      life = LIFE_AT_BEGINING;
      isAlive = true;
      nbBombs = 3;
      soundBomb = new Music(@"data//Fire_smartbomb.wav");
      //Création de la forme du joueur
      //length = 100;
      //height = 100;
      
      ////Initialisation visuelle du joueur
      //height = (float)Math.Sqrt(length * length - length * length * 0.25);
      this[0]= new Vector2f(-10,20);
      this[1]= new Vector2f(70,0);
      this[2]=new Vector2f(-10,-20);

    }
    public bool Update(Single deltaT, GW gw)
    {
      //A COMPLETER
      if(Keyboard.IsKeyPressed(Keyboard.Key.Up))
      {
        Advance(5);
      }
      if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
      {
        Advance(-5);
      }
      if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
      {
        Rotate(-5);
      }
      if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
      {
        Rotate(5);
      }
      return true;
    }
    private void FireBomb(GW gw)
    {
      //A COMPLETER
    }
  }
}
