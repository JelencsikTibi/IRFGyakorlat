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


            BirthProbabilities = GetBirthProbability(@"C:\Windows.old\WINDOWS\Temp\születés.csv");
            DeathProbabilities = GetDeathProbability(@"C:\Windows.old\WINDOWS\Temp\halál.csv");
            
        }

        private void StartSimulation(int endYear, string csvPath)
        {
            Population = GetPopulation(csvPath);

            for (int year = 2005; year <= endYear; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                    SimStep(year, Population[i]);

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                richTextBox1.Text += 
                    string.Format("Év:{0}\n Fiúk:{1}\n Lányok:{2}\n", year, nbrOfMales, nbrOfFemales);
            }
        }

        private void SimStep(int year, Person person)
        {
            //Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
            if (!person.IsAlive) return;

            // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
            byte age = (byte)(year - person.BirthYear);

            // Halál kezelése
            // Halálozási valószínűség kikeresése
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.P).FirstOrDefault();
            // Meghal a személy?
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            //Születés kezelése - csak az élő nők szülnek
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age
                                 select x.P).FirstOrDefault();
                //Születik gyermek?
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
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
                        Age = byte.Parse(line[0]),
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
                        Age = byte.Parse(line[1]),
                        P = double.Parse(line[2])
                    };
                    deathProbabilities.Add(p);
                }
            }

            return deathProbabilities;

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartSimulation((int)numYear.Value, txtPath.Text);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.FileName = txtPath.Text;

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            txtPath.Text = ofd.FileName;
        }
    }
}
