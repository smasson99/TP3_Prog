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
    //Toutes les propriétés
    #region:proprietes
    //Propriétés privées
    private Color color;
    private DateTime tempsVie;
    //Propriétés statiques
    private static float ProjectileSpeed;
    private static Random rnd = new Random();
    private static float reductionAlpha;
    //Propriétés protégées
    protected bool hasLifeLine;
    #endregion
    //Toutes les méthodes
    #region:methodes
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
      //Initialisation des variables de base
      Angle = angle;
      ProjectileSpeed = speed;
      this.color = color;
      reductionAlpha = 5.00f;
      hasLifeLine = false;
      ////Initialisation visuelle du projectile
      this[0] = new Vector2f(-5*size, -size);
      this[2] = new Vector2f(5*size, size);
      this[3] = new Vector2f(5*size, -size);
      this[1] = new Vector2f(-5*size,size);
      //Initialisation de la durée de vie de la particule
      tempsVie = DateTime.Now.AddSeconds(1.25f);
    }
    /// <summary>
    /// Fonction dont le rôle est de mettre à jour le projectile et de dire au jeu
    /// si celui-ci peut être retiré ou non
    /// </summary>
    /// <param name="deltaT">La vitesse de rafraichissement du jeu</param>
    /// <param name="gw">Instance représentant le jeu</param>
    /// <returns>Booléen indiquant si le projectile peut être retiré du jeu(FAUX) ou non (VRAI)</returns>
    public bool Update(Single deltaT, GW gw)
    {
      //Si le projectile sort de la limite du jeu ou devient invisible, alors retirer celui-ci du jeu
      if (gw.Contains(this) == false || color.A <= 25)
      {
        return false;
      }
      //Sinon si le projectile est limité à une durée de vie, alors s'assurer de le retirer au bon moment
      else if (hasLifeLine && DateTime.Now > tempsVie)
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
    #endregion
  }
}