using Mikroszimuláció.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikroszimuláció
{
    public partial class Form1 : Form
    {
        Random rng = new Random(1234);

        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        public Form1()
        {
            InitializeComponent();

            Population = GetPopulation(@"C:\Windows.old\WINDOWS\Temp\nép-teszt.csv");
            BirthProbabilities = GetBirthProbability(@"C:\Windows.old\WINDOWS\Temp\születés.csv");
            DeathProbabilities = GetDeathProbability(@"C:\Windows.old\WINDOWS\Temp\halál.csv");

            for (int year = 2005; year <= 2024; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {

                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(
                    string.Format("Év:{0}\n Fiúk:{1}\n Lányok:{2}\n", year, nbrOfMales, nbrOfFemales));
            }
        }

        public List<Person> GetPopulation(string csvPath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    var p = new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    };
                    population.Add(p);
                }
            }

            return population;

        }
        public List<BirthProbability> GetBirthProbability(string csvPath)
        {
            List<BirthProbability> birthProbabilities = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    var p = new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        P = double.Parse(line[2])
                    };
                    birthProbabilities.Add(p);
                }
            }

            return birthProbabilities;

        }
        public List<DeathProbability> GetDeathProbability(string csvPath)
        {
            List<DeathProbability> deathProbabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    var p = new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        Age = int.Parse(line[1]),
                        P = double.Parse(line[2])
                    };
                    deathProbabilities.Add(p);
                }
            }

            return deathProbabilities;

        }
    }
}
