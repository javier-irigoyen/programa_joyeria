using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {

            Circunferencia f = new Circunferencia(5);
           
            f.Radio = 10;
            
            
            Console.WriteLine(f.Radio);
            Console.WriteLine(f.Perimetro);
            Console.WriteLine(f.Area);
            Console.WriteLine(Circunferencia.Descripcion);

        }

        class Circunferencia
        {

            static  Circunferencia()
            {
                Descripcion = "Polígono regular de infinitos lados";
            }
            public static readonly string Descripcion;

            public Circunferencia(double radio)
            {
                this.Radio = radio;
            }

           
            const double PI = 3.1415926;

            public double Radio
            { get; set; }

            public double Perimetro
            {
                get
                {
                    return 2 * PI * this.Radio;
                }
            }

            public double Area
            {
                get
                {
                    return PI * Math.Pow(this.Radio, 2);
                }
            }
        }





    }
}
