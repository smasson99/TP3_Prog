using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace TP3
{
  public class Star
  {
    #region:Proprietes
    //Les constantes
    public const int STAR_WIDTH = 4;
    public const int STAR_HEIGHT = 4;
    //Les propriétés statiques
    private static Color[] colors = new Color[] { Color.Blue, Color.Green, Color.Red, Color.Yellow };
    private static RectangleShape shape;
    private static Random rnd = new Random();
    //Les propriétés normales
    private int colorType;
    private Vector2f position;
    private float speed;
    
    //Les propriétés C#
    /// <summary>
    /// Propriété C# qui représente la position en X de l'étoile
    /// </summary>
    public Single PositionX
    {
      get
      { return position.X; }
      set
      {
        position.X = value;
      }
    }
    /// <summary>
    /// Propriété C# qui représente la position en Y de l'étoile
    /// </summary>
    public Single PositionY
    {
      get
      { return position.Y; }
      set
      {
        position.Y = value;
      }
    }
    /// <summary>
    /// Propriété C# qui représente la vitesse de déplacement de l'étoile
    /// </summary>
    public Single Speed
    {
      get
      {
        return speed;
      }
      set
      {
        speed = value;
      }
    }
    #endregion

    #region:Fonctions
    public Star(Single posX, Single posY, Single speed)
    {
      //Initialisation des variables de base
      PositionX = posX;
      PositionY = posY;
      this.speed = speed;
      //Création de la shape de l'étoile
      shape = new RectangleShape(new Vector2f(STAR_WIDTH, STAR_HEIGHT));
      //Choix du type de couleur aléatoire
      colorType = rnd.Next(0, colors.Length);
    }
    public void Draw(RenderWindow window)
    {
      //Donner une couleur à l'étoile
      shape.FillColor = colors[colorType];
      shape.Position = new Vector2f(PositionX, PositionY);
      window.Draw(shape);
    }
    public void Respawn()
    {
      //À COMPLETE
    }
    public void Update(Single deltaT, Vector2f direction)
    {
      //À COMPLETE
    }
    #endregion
  }
}
