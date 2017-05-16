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
    //Toutes les propriétés
    #region:proprietes
    //Propriétés privées
    /// <summary>
    /// Propriété représentant le type de personnage ayant lancé le projectile
    /// </summary>
    private CharacterType type;
    /// <summary>
    /// Propriété représentant la couleur du projectile
    /// </summary>
    private Color color;
    //Propriétés statiques
    /// <summary>
    /// Propriété représentant la vitesse de déplacement du projectile
    /// </summary>
    private static float ProjectileSpeed;
    /// <summary>
    /// Propriété représentant la variable aléatoire de la classe
    /// </summary>
    private static Random rnd = new Random();
    //Propriétés C#
    /// <summary>
    /// Propriétés C# représentant le type de personnage ayant lancé le projectile
    /// </summary>
    public CharacterType Type
    {
      get
      {
        return type;
      }
    }
    #endregion
    //Toutes les méthodes
    #region:methodes
    /// <summary>
    /// Constructeur dont le rôle est d'initialiser les variables de base et le visuel du projectile
    /// </summary>
    /// <param name="type">Le type de personnage ayant lancé le projectile</param>
    /// <param name="posX">La position du projectile en X</param>
    /// <param name="posY">La position du projectile en Y</param>
    /// <param name="nbVertices">Le nombre de côtés de la forme du projectile</param>
    /// <param name="color">La couleur du projectile</param>
    /// <param name="speed">La vitesse de déplacement du projectile</param>
    /// <param name="size">La taille du projectile</param>
    /// <param name="angle">L'angle dans lequel le projectile a été tiré</param>
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

    /// <summary>
    /// Méthode dont le rôle est de mettre à jour le projectile et de dire au jeu s'il
    /// peut supprimer le projectile ou non
    /// </summary>
    /// <param name="deltaT">La vitesse de rafraichissement du jeu</param>
    /// <param name="gw">Instance représentant le jeu</param>
    /// <returns>Booléen indiquant si le projectile peut être détruit(FALSE) ou non(TRUE)</returns>
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
        gw.AddParticle(new Particle(Position.X, Position.Y, 4, new Color(color.R, color.G, color.B,
        (byte)rnd.Next(25, 255 + 1)), 5.35f, 0.55f, -rnd.Next(180 - (int)Angle - 5, 180 - (int)Angle + 5 + 1)));
        Advance(ProjectileSpeed);
        return true;
      }
    }
    #endregion
  }
}
