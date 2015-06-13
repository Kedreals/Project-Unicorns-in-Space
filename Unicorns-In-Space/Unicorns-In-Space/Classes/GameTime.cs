using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class GameTime
    {
        Stopwatch watch;
        public TimeSpan TotalTime { get; private set; }
        public TimeSpan EllapsedTime { get; private set; }

        public GameTime()
        {
            watch = new Stopwatch();
            TotalTime = new TimeSpan(0);
            EllapsedTime = new TimeSpan(0);
        }

        public void Start() { watch.Start(); }

        /// <summary>
        /// <para>DONT CALL THIS METHOD MORE THAN ONCE IN GAMEUPDATE!!!!!!!!!!!!!!!!</para>
        /// <para>IF U CALL THIS ANYWHERE ELSE I'M GONNA GETCHA</para>
        /// </summary>
        public void Update()
        {
            EllapsedTime = watch.Elapsed - TotalTime;
            TotalTime = watch.Elapsed;
        }

        public void Restart()
        {
            watch.Restart();
            TotalTime = new TimeSpan(0);
            EllapsedTime = new TimeSpan(0);
        }
    }
}
