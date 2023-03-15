using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unit006_tankkill_start
{
    enum GameState
    {
        Running,
        GameOver
    }
    class GameFramework
    {


        public static Graphics g;
        private static GameState gameState = GameState.Running;

        //游戏开始时做的事情
        public static void Start()
        {
            SoundManager.InitSound();
            GameObjectManager.Start();
            GameObjectManager.CreatMap();
            GameObjectManager.CreatMyTank();
            SoundManager.PlayStart();
        }

        //持续要做的事情
        public static void Update()
        { //fps
          //GameObjectManager.DrawMap();
          //GameObjectManager.DrawMyTank();            


            //Console.WriteLine("在画地图");
            if (gameState == GameState.Running)
            {
                GameObjectManager.Update();
            }
            else if (gameState == GameState.GameOver)
            {
                GameOverUpdate();
            }
        }
        private static void GameOverUpdate()
        {
            Bitmap bmp = Properties.Resources.GameOver;
            bmp.MakeTransparent(Color.Black);
            int x = 450 / 2 - Properties.Resources.GameOver.Width / 2;
            int y = 450 / 2 - Properties.Resources.GameOver.Height / 2;
            g.DrawImage(bmp, x, y);
        }
        public static void ChangeToGameOver()
        {
            gameState = GameState.GameOver;
        }


        //按键按下时
        public static void KeyDown(KeyEventArgs args)
        {
        }
        public static void KeyUp(KeyEventArgs args)
        {
        }
    }
}
