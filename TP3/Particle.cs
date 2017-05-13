using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace TP3
{
  public class Particle : Movable
  {
    private Color color;
    static float ProjectileSpeed;
    private static Random rnd = new Random();
    static float reductionAlpha;
    DateTime tempsVie;

    /// <summary>
    /// Constructeur initialisant les variables et l'affichage d'une particule
    /// </summary>
    /// <param name="posX">Position en X de la particule</param>
    /// <param name="posY">Position en Y de la particule</param>
    /// <param name="nbVertices">Nombre de côtés de la particule</param>
    /// <param name="color">Couleur de la particule</param>
    /// <param name="speed">Vitesse de déplacement de la particule</param>
    /// <param name="size">Taille de la particule</param>
    /// <param name="angle">L'angle de la particule</param>
    public Particle(float posX, float posY, uint nbVertices, Color color, float speed, float size, float angle)
    : base(posX, posY, nbVertices, color, speed)
    {
      //Initialisation des variables
      Angle = angle;
      ProjectileSpeed = speed;
      this.color = color;
      reductionAlpha = 5.00f;
      ////Initialisation visuelle du projectile
      this[0] = new Vector2f(-size, -size);
      this[2] = new Vector2f(size, size);
      this[3] = new Vector2f(size, -size);
      this[1] = new Vector2f(-size, size);
      //Initialisation de la durée de vie de la particule
      tempsVie = DateTime.Now.AddSeconds(1.25f);
    }

    public bool Update(Single deltaT, GW gw)
    {
      //Si le projectile sort de la limite du jeu, alors retirer celui-ci du jeu
      if (gw.Contains(this) == false || color.A <= 25)
      {
        return false;
      }
      //Sinon, on ajoute de nouvelles particules tout en le faisant avancer.
      else
      {
        if (rnd.Next(1, 15 +1) == 1)
        {
          Angle += 7.50f;
        }
        else if (rnd.Next(1, 15 + 1) == 1)
        {
          Angle -= 7.50f;
        }
        Advance(ProjectileSpeed);
        color.A -= (byte)(reductionAlpha);
        return true;
      }
    }
  }
}