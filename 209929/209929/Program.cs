using System;

namespace Kompozyt
{
    public abstract class Component
    {
        public abstract int Przejazd(int km);
    }

    class Cena_Przejazdu : Component
    {
        int km;
        public Cena_Przejazdu(int t)
        {
            km = t;
        }
        public override int Przejazd(int km)
        {
            Random rnd = new Random();
            int d = rnd.Next(1, 6);
            return km * d;

        }
    }

    abstract class Dekorator : Component
    {
        protected Component _component;

        public Dekorator(Component component)
        {
            this._component = component;
        }

        public void SetComponent(Component component)
        {
            this._component = component;
        }

        public override int Przejazd(int km)
        {
            if (this._component != null)
            {
                return this._component.Przejazd(km);
            }
            else
            {
                Console.WriteLine("ERROR!");
                return 0;
            }
        }
    }

    class Free : Dekorator
    {
        public Free(Component comp) : base(comp)
        {
        }

        public override int Przejazd(int km)
        {
            Console.WriteLine("Cena przejazdu to " + base.Przejazd(km) + "zl.");
            Console.WriteLine("Sprobuj najlepsze pierogi w Pierogarni na Mokotowie.");
            return km;
        }
    }

    class Small_Company : Dekorator
    {
        public Small_Company(Component comp) : base(comp)
        {
        }

        public override int Przejazd(int cena)
        {
            Random rnd = new Random();
            double d = rnd.NextDouble();
            Console.WriteLine("Cena przejazdu to " + cena + "zl.");
            Console.WriteLine("Cena przejazdu rozni sie od ceny rynkowej o " + (int)(cena * d / 2) + "zl.");
            return (int)(cena * d / 2);
        }
    }

    class Enterprise : Dekorator
    {
        public Enterprise(Component comp) : base(comp)
        {
        }

        public override int Przejazd(int cena)
        {
            base.Przejazd(cena);
            Console.WriteLine("Cena za zoptymalizowany przejazd to " + (int)(cena * 0.8) + "zl.");
            return cena;
        }
    }

    public class Client
    {
        public void ClientCode(Component component, int a)
        {
            component.Przejazd(a);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            bool on = true;
            do
            {
                Console.WriteLine("Podaj ilosc kilometrow w trasie:");
                int km = int.Parse(Console.ReadLine());
                Console.WriteLine("Podaj rodzaj subskrypcji (F/S/E):");
                char chr = Convert.ToChar(Console.ReadLine());
                var basic = new Cena_Przejazdu(km);
                
                if (chr == 'F')
                {
                    Free opcja1 = new Free(basic);
                    client.ClientCode(opcja1, basic.Przejazd(km));
                }
                else
                {
                    Small_Company opcja2 = new Small_Company(basic);
                    if (chr == 'S')
                    {
                        client.ClientCode(opcja2, basic.Przejazd(km));

                    }
                    else
                    {
                        Enterprise opcja3 = new Enterprise(opcja2);
                        client.ClientCode(opcja3, basic.Przejazd(km));
                    }


                }
                Console.WriteLine();
                Console.WriteLine("Zeby zakonczyc wcisnij X.\nZeby podac nowa trase wcisnij T.");
                char ex = Convert.ToChar(Console.ReadLine());
                if (ex == 'X')
                {
                    on = false;
                }

            }
            while (on == true);
        }
    }
}