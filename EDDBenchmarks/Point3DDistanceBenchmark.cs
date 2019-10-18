using System;
using System.Collections.Generic;
using System.Diagnostics;
using EMK.LightGeometry;

namespace EDDBenchmarks
{
    internal static class Point3DDistanceBenchmark
    {
        public static void Main(string[] args)
        {
            const int count = 25000;
            var pointsList = new List<Point3D>(count);

            Console.WriteLine(@"Generating random Point3D list of size " + count);
            {
                var watch = new Stopwatch();
                watch.Start();
                GeneratePoints(count, pointsList);
                watch.Stop();
                Console.Write(@"List generated");
                Console.WriteLine(@", duration: " + watch.Elapsed.TotalSeconds.ToString("0.000s") + Environment.NewLine);
            }

            Console.WriteLine(@"Calculating distances" + Environment.NewLine);
            for(var loop=0; loop<5; loop++)
            {
                var watch = new Stopwatch();
                watch.Start();
                var distanceSquared = CalculateDistances(pointsList);
                watch.Stop();
                Console.Write(@"Total: " + distanceSquared);
                Console.WriteLine(@", duration: " + watch.Elapsed.TotalSeconds.ToString("0.000s"));
            }
        }

        private static void GeneratePoints(int count, ICollection<Point3D> pointsList)
        {
            var rnd = new Random();
            for (var k = 0; k < count; k++)
            {
                var point = new Point3D(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble());
                pointsList.Add(point);
            }
        }

        private static float CalculateDistances(List<Point3D> pointsList)
        {
            float distanceSquared = 0f;
            float dx, dy, dz;

            for (var ii = 0; ii < pointsList.Count; ii++)
            {
                var point1 = pointsList[ii];

                for (var jj = ii; jj < pointsList.Count; jj++)
                {
                    var point2 = pointsList[jj];

                    dx = (float) (point1.X - point2.X);
                    dy = (float) (point1.Y - point2.Y);
                    dz = (float) (point1.Z - point2.Z);

                    distanceSquared += dx * dx + dy * dy + dz * dz;
                }
            }
            return distanceSquared;
        }
    }
}
