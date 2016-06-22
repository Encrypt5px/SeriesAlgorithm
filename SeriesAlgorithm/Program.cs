using System;
using System.Collections.Generic;

namespace SeriesAlgorithm
{
    /// <summary>
    /// Polynomials class
    /// </summary>
    class Poly
    {
        public double ratio;//The ratio of polynomials
        public double exponent;//The exponenet of polynomials

        /// <summary>
        /// Get the polynomials with input string array, will return false when fail
        /// </summary>
        /// <param name="input"></param>
        /// <param name="poly"></param>
        /// <returns></returns>
        public static bool getPoly(string[] input, out Poly poly)
        {
            poly = null;
            if (input.Length != 2)
            {
                Console.WriteLine("Erro in get input string " + "\"" + input.arrayToString() + "\"");
                return false;
            }

            double tRatio;
            double tExponent;
            if (Double.TryParse(input[0].ToString(), out tRatio))
            {
                if (Double.TryParse(input[1].ToString(), out tExponent))
                {
                    poly = new Poly() { ratio = tRatio, exponent = tExponent };
                    return true;
                }
                else
                {
                    Console.WriteLine("Erro try parse exponent with value " + input[1]);
                }
            }
            else
            {
                Console.WriteLine("Erro try parse ratio with value " + input[0]);
            }
            return false;
        }
        
        public static Poly operator *(Poly p1, Poly p2)
        {
            return new Poly() { ratio = p1.ratio * p2.ratio, exponent = p1.exponent + p2.exponent };
        }

        public static Poly operator /(Poly p1, Poly p2)
        {
            return new Poly() { ratio = p1.ratio / p2.ratio, exponent = p1.exponent - p2.exponent };
        }

        public override string ToString()
        {
            if (ratio != 0 && exponent != 0)
            {
                return ratio + "^" + exponent;
            }
            return base.ToString();
        }
    }

    /// <summary>
    /// Extension for string array get polynomials queue
    /// </summary>
    static class Extension
    {
        public static string arrayToString(this String[] array)
        {
            string str = "";
            foreach (string s in array)
                str += s;
            return str;
        }

        public static bool polyToQueue(this String[] value, out Queue<Poly> queue)
        {
            Poly polyTmp;
            queue = new Queue<Poly>();
            for (int i = 0; i < value.Length; i++)
            {
                string[] polyStrArray = value[i].Split('^');
                if (Poly.getPoly(polyStrArray, out polyTmp))
                {
                    queue.Enqueue(polyTmp);
                }
            }
            return true;
        }
    }

    //Main program
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input the polynomials");
            //string s = Console.ReadLine();//For user input data

            #region Init polynomials data

            Queue<Queue<Poly>> queueCollect = new Queue<Queue<Poly>>();

            string[] polyString = new string[5];
            polyString[0] = "1^5 2^4 1^4 2^3 -1^2\n";
            polyString[1] = "1^5 2^4 1^4 2^3 -1^2 2^2\n";
            polyString[2] = "1^5 2^4 1^4 2^3 -1^2 2^2 1^1 2^1\n";
            polyString[3] = "1^5 2^4 1^4 2^3 -1^2 2^2 1^1 2^1 1^1\n";
            polyString[4] = "1^5 2^4 1^4 2^3 -1^2 2^2 1^1 2^1 1^1 2^1\n";
            #endregion

            #region Generate polynomials queue by polyString, enqueue all polyQueue to collection

            foreach (string s in polyString)
            {
                Console.WriteLine(s);
                Queue<Poly> polyQueue = new Queue<Poly>();
                init_SimplifyPolyQueue(s,out polyQueue);
                queueCollect.Enqueue(polyQueue);
            }
            #endregion

            #region Calculate polynomials in QueueCollection

            Queue<Poly> fistQueueTmp = queueCollect.Dequeue();
            while (queueCollect.Count > 0)
            {
                Queue<Poly> secQueueTmp = queueCollect.Dequeue();
                calculateTwoQueue(ref fistQueueTmp,ref secQueueTmp);
            }
            #endregion

            #region Output result

            Console.WriteLine("It's the result below");
            while (fistQueueTmp.Count > 0)
                Console.Write(fistQueueTmp.Dequeue().ToString() + " ");
            Console.ReadKey();
            #endregion
        }

        #region Calculate two polynomials queue

        private static Poly emptyPoly = new Poly() { ratio = 0, exponent = 0 };//Empty cache for calculate
        static void calculateTwoQueue(ref Queue<Poly> queue1, ref Queue<Poly> queue2)
        {
            Queue<Poly> resultQueue = new Queue<Poly>();
            Poly p1 = queue1.Dequeue();
            Poly p2 = queue2.Dequeue();

            for (bool addLoop = false; ;)
            {
                if (p1.exponent > p2.exponent)
                {
                    resultQueue.Enqueue(p1);
                    p1 = queue1.Count == 0 ? emptyPoly : queue1.Dequeue();
                }
                else if (p1.exponent < p2.exponent)
                {
                    resultQueue.Enqueue(p2);
                    p2 = queue2.Count == 0 ? emptyPoly : queue2.Dequeue();
                }
                else
                {
                    p2.ratio += p1.ratio;
                    resultQueue.Enqueue(p2);

                    p1 = queue1.Count == 0 ? emptyPoly : queue1.Dequeue();
                    p2 = queue2.Count == 0 ? emptyPoly : queue2.Dequeue();
                }
                if (queue1.Count == 0 && queue2.Count == 0)
                {
                    addLoop = !addLoop;
                    if (!addLoop) break;
                }
            }
            queue1 = resultQueue;
        }
        #endregion

        #region init and simplify PolyQueue

        /// <summary>
        /// Init polynomials queue by polyData and invoke simplifyPolyQueue() to make it simple
        /// </summary>
        /// <param name="polyData"></param>
        /// <param name="queue"></param>
        static void init_SimplifyPolyQueue(string polyData,out Queue<Poly> queue)
        {
            polyData.Split(' ').polyToQueue(out queue);//Using extension function
            simplifyPolyQueue(ref queue);
        }

        /// <summary>
        /// Make the polynomials queue more simplify by combine two same exponent polynomials in queue
        /// </summary>
        /// <param name="queue"></param>
        static void simplifyPolyQueue(ref Queue<Poly> queue)
        {
            Poly dequeuePoly;//Current operate polynomials
            Poly ptemp = queue.Dequeue();//temp polynomials that last unhandle
            Queue<Poly> qresult = new Queue<Poly>();
            while (queue.Count > 0)
            {
                dequeuePoly = queue.Dequeue();
                if (dequeuePoly.exponent == ptemp.exponent)
                {
                    double sumRatio = dequeuePoly.ratio + ptemp.ratio;
                    ptemp.ratio = sumRatio;
                }
                else if (dequeuePoly.exponent > ptemp.exponent)
                {
                    qresult.Enqueue(dequeuePoly);
                }
                else
                {
                    qresult.Enqueue(ptemp);
                    ptemp = dequeuePoly;
                }
                if (queue.Count == 0)
                    qresult.Enqueue(ptemp);
            }
            queue = qresult;
        }
        #endregion

    }

}
