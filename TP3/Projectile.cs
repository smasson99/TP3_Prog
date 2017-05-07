using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace TP3
{
  public class Projectile:Movable
  {
    private CharacterType type;
    static float ProjectileSpeed;

    public CharacterType Type
    {
      get
      {
        return type;
      }
    }


    public Projectile(CharacterType type, float posX, float posY, uint nbVertices, Color color, float speed, float angle)
    : base(posX, posY, nbVertices, color, speed)
    {
      //Initialisation des variables
      Angle = angle;
      this.type = type;
      ProjectileSpeed = speed;
      ////Initialisation visuelle du projectile
      this[0] = new Vector2f(0, 0);
      this[1] = new Vector2f(7, 0);
      this[2] = new Vector2f(0, 7);
      this[3] = new Vector2f(7, 7);
    }

    public bool Update(Single deltaT, GW gw)
    {
      if (Position.X < 0 || Position.X > GW.WIDTH || Position.Y < 0 || Position.Y > GW.HEIGHT)
      {
        return false;
      }
      else
      {
        Advance(5);
        return true;
      }
    }
  }
}
