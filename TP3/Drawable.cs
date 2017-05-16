using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace TP3
{
  public abstract class Drawable
  {
    /// <summary>
    /// Propriété représentant la forme de base
    /// </summary>
    private ConvexShape shape = null;

    /// <summary>
    /// Propriété C# représentant la position en X et en Y de la forme
    /// </summary>
    public Vector2f Position { get; set;}

    /// <summary>
    /// Propriété C# représentant la couleur de la forme
    /// </summary>
    public Color Color { get { return shape.FillColor; } set { shape.FillColor = value; } }

    /// <summary>
    /// Propriété C# représentant l'angle vers lequel la forme est positionnée
    /// </summary>
    public virtual float Angle
    {
      get { return shape.Rotation; }
      set 
      {
        shape.Rotation = value;
      }
    }

    /// <summary>
    /// Constructeur dont le rôle est d'initialiser les variables de base
    /// </summary>
    /// <param name="posX">Position de la forme en X</param>
    /// <param name="posY">Position de la forme en Y</param>
    /// <param name="nbVertices">Nombre de côtés de la forme</param>
    /// <param name="color">Couleur de la forme</param>
    protected Drawable(float posX, float posY, uint nbVertices, Color color)
    {
      Position = new Vector2f(posX, posY);
      shape = new ConvexShape(nbVertices);
      shape.FillColor = color;
      Angle = 0;
    }
    
    /// <summary>
    /// Créer la forme selon les paramètres entrés
    /// </summary>
    /// <param name="index">Le numéro du sommet de la forme</param>
    /// <returns></returns>
    public Vector2f this[uint index]
    {
      get { return shape.GetPoint(index); }
      set { shape.SetPoint(index, value); }
    }

    /// <summary>
    /// Afficher à l'écran la forme
    /// </summary>
    /// <param name="window"></param>
    public virtual void Draw(RenderWindow window)
    {
      shape.Position = Position;      
      window.Draw(shape);
    }

    /// <summary>
    /// Retourne la boîte englobante associée à la forme.  Utilisée pour les collisions.
    /// </summary>
    public FloatRect BoundingBox
    {
      get { return shape.GetGlobalBounds(); }
    }

    /// <summary>
    /// Vérifie si deux éléments affichés s'entrecoupent
    /// </summary>
    /// <param name="m">Le second élément avec lequel vérifier la collision</param>
    /// <returns>true s'il y a collision, false sinon.</returns>
    public bool Intersects(Drawable m)
    {
      FloatRect r = m.BoundingBox;
      r.Left = m.Position.X;
      r.Top = m.Position.Y;
      return BoundingBox.Intersects(r);
    }

    /// <summary>
    /// Vérifie si l'objet contient le point où se trouve l'élément reçu en paramètre
    /// </summary>
    /// <param name="m">L'élément dont il faut vérifier la position</param>
    /// <returns>true si la boîte englobante de l'objet courant contient le point (la position) où se
    /// trouve l'objet reçu en paramètre</returns>
    public bool Contains(Drawable m)
    {
      return BoundingBox.Contains(m.Position.X, m.Position.Y);
    }
  }
}
