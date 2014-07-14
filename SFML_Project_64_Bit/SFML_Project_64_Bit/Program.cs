using System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using System.Diagnostics;

namespace SFML_Project_64_Bit
{
    class Program
    {
        static void Main(string[] args)
        {
            MySFMLProgram app = new MySFMLProgram();
            app.StartSFMLProgram();
        }
    }
    class MySFMLProgram
    {

        //Window and Cam

        public  RenderWindow _window;
        public  View camera;

        //Asset

        protected Texture playTexture;
        protected Sprite playSprite;

        //Time Vars

        const double frame = 60;
        const double timeStep = 1 / frame;
        double timeBank;
        TimeSpan dT;
        

        public void StartSFMLProgram()
        {
            LoadContent();

            //Render Window

            _window = new RenderWindow(new VideoMode(1280, 760), "SFML window");
            _window.SetVisible(true);
            //_window.SetFramerateLimit(60);
            _window.SetVerticalSyncEnabled(true);
            _window.Closed += new EventHandler(OnClosed);

            //Camera

            camera = new View(new Vector2f(0, 0), new Vector2f(1280, 760));


            //DeltaTime

            dT = new TimeSpan();
            Stopwatch timer = new Stopwatch();
            timer.Start();
           
            while (_window.IsOpen())
            {
                timeBank += dT.TotalSeconds;


                if (timeBank >= timeStep)
                {
                    Console.WriteLine(timeBank);
                    camera.Rotation += 1f;
                    _window.SetView(camera);
                    timeBank -= timeStep;
                }

                _window.DispatchEvents();
                this.Draw();

                dT = timer.Elapsed;
                timer.Restart();
            }
        }

        public void Draw()
        {
            _window.Clear(Color.Red);
            _window.Draw(playSprite);
            _window.Display();
        }

        void OnClosed(object sender, EventArgs e)
        {
            _window.Close();
        }

        public void LoadContent()
        {
            playTexture = new Texture("GraphicAsset/playButton.png");
            playSprite = new Sprite(playTexture);
            playTexture.Smooth = true;
        }
    }
}