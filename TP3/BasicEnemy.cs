using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
namespace TP3
{
  public class BasicEnemy:Enemy
  {
    //Proprietés
    static float BasicEnemySpeed;
    static Music spawnMusic;

    //Méthodes

    static BasicEnemy()
    {
      BasicEnemySpeed = 1.00f;
      spawnMusic = new Music(@"data//Enemy_spawn_orange.wav");
    }
    public BasicEnemy(Single posX, Single posY, Single angle)
    :base(posX, posY, 3, 5.00f, Color.Red, 1.00f)
    {
      this[0] = new Vector2f(-7, 20);
      this[1] = new Vector2f(55, 0);
      this[2] = new Vector2f(-7,-20);
    }

    public override bool Update(Single deltaT, GW gw)
    {
      //A COMPLETE
      return true;
    }

  }
}
