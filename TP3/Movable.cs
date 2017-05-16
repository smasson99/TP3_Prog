using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace TP3
{
  public class Movable : Drawable
  {
    //Toutes les propriétés
    #region:proprietes
    /// <summary>
    /// Propriété C# représentant l'angle de la forme
    /// </summary>
    public override float Angle
    {
      get
      {
        return base.Angle;
      }

      set
      {
        base.Angle = value;
        Direction = new Vector2f((float)Math.Cos(Math.PI * Angle / 180.0f), (float)Math.Sin(Math.PI * Angle / 180.0f));
      }
    }
    /// <summary>
    /// Propriété C# représentant la taille de la forme
    /// </summary>
    public float Size { get { return Math.Max(BoundingBox.Height, BoundingBox.Width); } }
    /// <summary>
    /// Propriété C# représentant la dirrection vers laquelle pointe la forme
    /// </summary>
    public Vector2f Direction { get; set; }
    /// <summary>
    /// Propriété C# indiquant si la forme est encore en vie(VRAI) ou non(FAUX)
    /// </summary>
    public virtual bool IsAlive { get; protected set; }
    /// <summary>
    /// Propriété C# représentant la vitesse de déplacement de base de la forme
    /// </summary>
    public float Speed { get; set; }
    #endregion
    //Toutes les méthodes
    #region:methodes
    /// <summary>
    /// Constructeur dont le rôle est d'initialiser les variables de base
    /// </summary>
    /// <param name="posX">Position de la forme en X</param>
    /// <param name="posY">Position de la forme en Y</param>
    /// <param name="nbVertices">Le nombre de côtés de la forme</param>
    /// <param name="color">La couleur de la forme</param>
    /// <param name="speed">La vitesse de déplacement de la forme</param>
    public Movable(float posX, float posY, uint nbVertices, Color color, float speed)
    :base(posX, posY, nbVertices, color)
    {
      //Initialisation des variables de base
      Angle = 0;
      IsAlive = true;
      Speed = speed;
    }
    /// <summary>
    /// Méthode dont le rôle est d'éffectuer une rotation de la forme
    /// </summary>
    /// <param name="angleInDegrees">Angle ajouté à la forme en degrés</param>
    protected void Rotate(float angleInDegrees)
    {
      Angle = Angle + angleInDegrees;
    }
    /// <summary>
    /// Fonction dont le rôle est de faire avancer la forme d'un nombre de pixels à l'écran
    /// </summary>
    /// <param name="nbPixels">Le nombre de pixels dont la forme doit être avancée</param>
    protected virtual void Advance(float nbPixels)
    {
      Position = Position + Direction * nbPixels;
    }
    #endregion
  }
}
