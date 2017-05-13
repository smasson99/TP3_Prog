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
    private static Random rnd = new Random();
    

    public CharacterType Type
    {
      get
      {
        return type;
      }
    }

    public Projectile(CharacterType type, float posX, float posY, uint nbVertices, Color color, float speed, float size, float angle)
    : base(posX, posY, nbVertices, color, speed)
    {
      //Initialisation des variables
      Angle = angle;
      this.type = type;
      ProjectileSpeed = speed;
      this.color = color;
      ////Initialisation visuelle du projectile
      this[0] = new Vector2f(-size, -size);
      this[2] = new Vector2f(size, size);
      this[3] = new Vector2f(size, -size);
      this[1] = new Vector2f(-size, size);
    }

    public bool Update(Single deltaT, GW gw)
    {
      //Si le projectile sort de la limite du jeu, alors retirer celui-ci du jeu
      if (gw.Contains(this) == false)
      {
        return false;
      }
      //Sinon, on ajoute de nouvelles particules tout en le faisant avancer.
      else
      {
        //gw.AddParticle(new Particle(Position.X, Position.Y, 4, new Color(color.R, color.G, color.B, (byte)rnd.Next(25, 255 + 1)), 3.50f, true, 0.25f));
        Advance(ProjectileSpeed);
        return true;
      }
    }
  }
}
