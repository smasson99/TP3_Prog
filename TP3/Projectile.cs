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
    private Color color;
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
      this.color = color;
      ////Initialisation visuelle du projectile
      this[0] = new Vector2f(-3.5f, -3.5f);
      this[2] = new Vector2f(3.5f, 3.5f);
      this[3] = new Vector2f(3.5f, -3.5f);
      this[1] = new Vector2f(-3.5f, 3.5f);
    }

    public bool Update(Single deltaT, GW gw)
    {
      if (Position.X < 0 || Position.X > GW.WIDTH || Position.Y < 0 || Position.Y > GW.HEIGHT)
      {
        return false;
      }
      else
      {
        gw.AddParticle(new Particle(Position.X, Position.Y, 4, color, 3.50f, true));
        Advance(ProjectileSpeed);
        return true;
      }
    }
  }
}
