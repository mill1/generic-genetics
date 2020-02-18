/*
 *    Wrapper for circle-fit; a JavaScript library for fast circle fitting of a set of 2D points.
 * 
 *       Return-object
 *       success(Boolean) : status of the computation
 *       points(Array) : all points given by the user
 *       projections(Array) : projections of each points onto the circle
 *       distances(Array) : distance of each points to the circle
 *       center(Object) : center of the circle
 *       radius(Number) : radius of the circle
 *       residue(Number) : residue of the least squares method, can be use to define the quality of the circle
 *       computationTime(Number) : time spent in computation(in milliseconds)
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics.Implementations
{
    public class CircleFit
    {
        private readonly string circleFitJS;

        public CircleFit()
        {
            circleFitJS = GetCircleFitJavaScript();
        }

        public double CalculateFitness(Point[] points)
        {
            var engine = new Jint.Engine();
            engine.Execute(circleFitJS);

            //  engine.Execute("CIRCLEFIT.resetPoints()");

            foreach (Point point in points)
                engine.Execute($"CIRCLEFIT.addPoint({point.X}, {point.Y})");

            dynamic result = engine.Execute("CIRCLEFIT.compute()").GetCompletionValue().ToObject();

            if (result.success)
            {
                // The residual for an observation is the difference between the observation and the fitted point.
                // Some residuals are positive and some are negative.
                // double res = 1 - Math.Abs((double)result.residue * 10000000000000);

                //double[] someDoubles = result.distances.Select(x => (double)x).ToArray();
                double[] someDoubles = new double[points.Length];

                for (int i = 0; i < points.Length; i++)
                    someDoubles[i] = Math.Abs(result.distances[i]);

                double res = 1 - CalculateStandardDeviation(someDoubles);

                // Console.WriteLine($"{res}");
                return res;
            }
            else
                throw new Exception("Could not compute.");
        }

        private double CalculateStandardDeviation(double[] someDoubles)
        {
            double average = someDoubles.Average();
            double sumOfSquaresOfDifferences = someDoubles.Select(val => (val - average) * (val - average)).Sum();
            return Math.Sqrt(sumOfSquaresOfDifferences / someDoubles.Length);
        }

        private string GetCircleFitJavaScript()
        {
            /*  
             *  https://github.com/Meakk/circle-fit
             *  http://csharphelper.com/blog/2014/08/find-a-minimal-bounding-circle-of-a-set-of-points-in-c/
             *  https://blog.codeinside.eu/2019/06/30/jint-invoke-javascript-from-dotnet/
             */

            string circleFitJS = "/* \r\n" +
            "The MIT License (MIT) \r\n" +
            "Copyright (c) 2015 Michael MIGLIORE \r\n" +
            "Permission is hereby granted, free of charge, to any person obtaining a copy \r\n" +
            "of this software and associated documentation files (the 'Software'), to deal \r\n" +
            "in the Software without restriction, including without limitation the rights \r\n" +
            "to use, copy, modify, merge, publish, distribute, sublicense, and/or sell \r\n" +
            "copies of the Software, and to permit persons to whom the Software is \r\n" +
            "furnished to do so, subject to the following conditions: \r\n" +
            "The above copyright notice and this permission notice shall be included in all \r\n" +
            "copies or substantial portions of the Software. \r\n" +
            "THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR \r\n" +
            "IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, \r\n" +
            "FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE \r\n" +
            "AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER \r\n" +
            "LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, \r\n" +
            "OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE \r\n" +
            "SOFTWARE. \r\n" +
            "*/ \r\n" +
            "\r\n" +
            "var CIRCLEFIT = (function () { \r\n" +
            "  var my = {}, \r\n" +
            "      points = []; \r\n" +
            "\r\n" +
            "  function linearSolve2x2(matrix, vector) { \r\n" +
            "    var det = matrix[0]*matrix[3] - matrix[1]*matrix[2]; \r\n" +
            "    if (det < 1e-8) return false; //no solution \r\n" +
            "    var y = (matrix[0]*vector[1] - matrix[2]*vector[0])/det; \r\n" +
            "    var x = (vector[0] - matrix[1]*y)/matrix[0]; \r\n" +
            "    return [x,y]; \r\n" +
            "  } \r\n" +
            "\r\n" +
            "  my.addPoint = function (x, y) { \r\n" +
            "    points.push({x: x, y: y}); \r\n" +
            "  } \r\n" +
            "\r\n" +
            "  my.resetPoints = function () { \r\n" +
            "    points = []; \r\n" +
            "  } \r\n" +
            "\r\n" +
            "  my.compute = function () { \r\n" +
            "    var result = { \r\n" +
            "      points: points, \r\n" +
            "      projections: [], \r\n" +
            "      distances: [], \r\n" +
            "      success: false, \r\n" +
            "      center: {x:0, y:0}, \r\n" +
            "      radius: 0, \r\n" +
            "      residue: 0 \r\n" +
            "    }; \r\n" +
            "\r\n" +
            "    //means \r\n" +
            "    var m = points.reduce(function(p, c) { \r\n" +
            "      return {x: p.x + c.x/points.length, \r\n" +
            "              y: p.y + c.y/points.length}; \r\n" +
            "    },{x:0, y:0}); \r\n" +
            "     \r\n" +
            "    //centered points \r\n" +
            "    var u = points.map(function(e){ \r\n" +
            "      return {x: e.x - m.x, \r\n" +
            "              y: e.y - m.y}; \r\n" +
            "    }); \r\n" +
            "\r\n" +
            "    //solve linear equation \r\n" +
            "    var Sxx = u.reduce(function(p,c) { \r\n" +
            "      return p + c.x*c.x; \r\n" +
            "    },0); \r\n" +
            "\r\n" +
            "    var Sxy = u.reduce(function(p,c) { \r\n" +
            "      return p + c.x*c.y; \r\n" +
            "    },0); \r\n" +
            "\r\n" +
            "    var Syy = u.reduce(function(p,c) { \r\n" +
            "      return p + c.y*c.y; \r\n" +
            "    },0); \r\n" +
            "\r\n" +
            "    var v1 = u.reduce(function(p,c) { \r\n" +
            "      return p + 0.5*(c.x*c.x*c.x + c.x*c.y*c.y); \r\n" +
            "    },0); \r\n" +
            "\r\n" +
            "    var v2 = u.reduce(function(p,c) { \r\n" +
            "      return p + 0.5*(c.y*c.y*c.y + c.x*c.x*c.y); \r\n" +
            "    },0); \r\n" +
            "\r\n" +
            "    var sol = linearSolve2x2([Sxx, Sxy, Sxy, Syy], [v1, v2]); \r\n" +
            "\r\n" +
            "    if (sol === false) { \r\n" +
            "      //not enough points or points are colinears \r\n" +
            "      return result; \r\n" +
            "    } \r\n" +
            "\r\n" +
            "    result.success = true; \r\n" +
            "\r\n" +
            "    //compute radius from circle equation \r\n" +
            "    var radius2 = sol[0]*sol[0] + sol[1]*sol[1] + (Sxx+Syy)/points.length; \r\n" +
            "    result.radius = Math.sqrt(radius2); \r\n" +
            "\r\n" +
            "    result.center.x = sol[0] + m.x; \r\n" +
            "    result.center.y = sol[1] + m.y; \r\n" +
            "\r\n" +
            "    points.forEach(function(p) { \r\n" +
            "      var v = {x: p.x - result.center.x, y: p.y - result.center.y}; \r\n" +
            "      var len2 = v.x*v.x + v.y*v.y; \r\n" +
            "      result.residue += radius2 - len2; \r\n" +
            "      var len = Math.sqrt(len2); \r\n" +
            "      result.distances.push(len - result.radius); \r\n" +
            "      result.projections.push({ \r\n" +
            "        x: result.center.x + v.x*result.radius/len, \r\n" +
            "        y: result.center.y + v.y*result.radius/len \r\n" +
            "      });      \r\n" +
            "    }); \r\n" +
            "\r\n" +
            "    return result; \r\n" +
            "  } \r\n" +
            "\r\n" +
            "  return my; \r\n" +
            "}()); \r\n";

            return circleFitJS;
        }
    }
}
