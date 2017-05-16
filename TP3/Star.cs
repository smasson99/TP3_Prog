using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using System.Drawing;

namespace TP3
{
  public class Star
  {
    //Toutes les propriétés
    #region:Proprietes
    //Les constantes
    /// <summary>
    /// Constante représentant la largeur de l'étoile
    /// </summary>
    public const int STAR_WIDTH = 4;
    /// <summary>
    /// Constante représentant la hauteur de l'étoile
    /// </summary>
    public const int STAR_HEIGHT = 4;
    //Les propriétés statiques
    /// <summary>
    /// Propriété statique représentant les couleurs possibles des étoiles affichées à l'écran
    /// </summary>
    private static Color[] colors = new Color[] { Color.White, new Color(226, 122, 18), new Color(232, 244, 249), new Color(255, 170, 0), new Color(255, 93, 0) };
    /// <summary>
    /// Propriété statique représentant la forme de base de l'étoile
    /// </summary>
    private static RectangleShape shape;
    /// <summary>
    /// Propriété statique aléatoire de la classe
    /// </summary>
    private static Random rnd = new Random();
    //Les propriétés normales
    /// <summary>
    /// Entier représentant l'index de la couleur de l'étoile dans le tableau des couleurs possibles
    /// </summary>
    private int colorType;
    /// <summary>
    /// Propriété représentant la position en X et en Y de l'étoile
    /// </summary>
    private Vector2f position;
    /// <summary>
    /// Propriété représentant la vitesse de déplacement de l'étoile
    /// </summary>
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
    //Toutes les méthodes
    #region:methodes
    /// <summary>
    /// Constructeur dont le rôle est d'initialiser les variables de base de l'étoile
    /// </summary>
    /// <param name="posX">Position en X de l'étoile</param>
    /// <param name="posY">Position en Y de l'étoile</param>
    /// <param name="speed">Vitesse de déplacement de l'étoile</param>
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
    /// <summary>
    /// Fonction dont le rôle est d'afficher à l'écran l'étoile
    /// </summary>
    /// <param name="window">L'écran du jeu</param>
    public void Draw(RenderWindow window)
    {
      //Donner une couleur à l'étoile
      shape.FillColor = colors[colorType];
      shape.Position = new Vector2f(PositionX, PositionY);
      if (rnd.Next(0, 100) < 5)
      {
        shape.FillColor = colors[rnd.Next(0, colors.Length)];
      }
      window.Draw(shape);
    }
    /// <summary>
    /// Fonction dont le rôle est de rénitialiser la position et la couleur de l'étoile si elle 
    /// vient à sortir des limites de l'écran
    /// </summary>
    public void Respawn()
    {
      if (PositionX > GW.WIDTH)
      {
        colorType = rnd.Next(0, colors.Length);
        shape.FillColor = colors[colorType];
        PositionX = 0;
        PositionY = rnd.Next(0, GW.HEIGHT);
      }
      else if (PositionX < 0)
      {
        colorType = rnd.Next(0, colors.Length);
        shape.FillColor = colors[colorType];
        PositionX = GW.WIDTH;
        PositionY = rnd.Next(0, GW.HEIGHT);
      }
      else if (PositionY < 0)
      {
        colorType = rnd.Next(0, colors.Length);
        shape.FillColor = colors[colorType];
        PositionY = GW.HEIGHT;
        PositionX = rnd.Next(0, GW.WIDTH);
      }
      else if (PositionY > GW.HEIGHT)
      {
        colorType = rnd.Next(0, colors.Length);
        shape.FillColor = colors[colorType];
        PositionY = 0;
        PositionX = rnd.Next(0, GW.WIDTH);
      }
    }
    /// <summary>
    /// Fonction dont le rôle est de mettre à jour l'étoile
    /// </summary>
    /// <param name="deltaT">La vitesse de rafraichissement du jeu</param>
    /// <param name="direction">La direction vers laquelle l'étoile se déplace</param>
    public void Update(Single deltaT, Vector2f direction)
    {
      PositionX = PositionX - direction.X *deltaT;
      PositionY = PositionY - direction.Y *deltaT;
      shape.Position = new Vector2f(PositionX, PositionY);
      Respawn();
    }
    #endregion
  }
}
