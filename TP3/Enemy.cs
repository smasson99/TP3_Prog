using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace TP3
{
  public class Enemy:Character
  {
    //Propriétés
    protected static Random rnd = new Random();
    private bool isSpawning;
    protected int nbUpdates;
    protected Color enemyColor;

    protected bool IsSpawning
    {
      get
      {
        return isSpawning;
      }
      set
      {
        isSpawning = value;
      }
    }

    protected Enemy(Single posX, Single posY, UInt32 nbVertices, Single size, Color color, Single speed)
    :base(posX, posY, nbVertices, color, speed, CharacterType.ENNEMI)
    {
      //Initialisation des variables
      isSpawning = true;
      enemyColor = color;
      nbUpdates = 0;
    }
    public virtual bool Update(Single deltaT, GW gw)
    {
      //A COMPLETE
      nbUpdates++;
      return true;
    }
  }
}
