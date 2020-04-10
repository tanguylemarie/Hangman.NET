using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pendu
{
    class GameInstance
    {
        public List<char> Guesses { get; }

        public List<char> Misses { get; }

        public List<Word> Words { get; }

        public Word WordToGuess { get; }

        private int maxErrors { get; set; }

        private bool isWin { get; set; }

        private Random rnd;

        private string currentWordGuessed;

        /// <summary>
        /// Créer une nouvelle instance du jeu du pendu.
        /// Le mot à deviner est choisit aléatoirement.
        /// </summary>
        /// <param name="maxErrors">Nombre d'erreurs maximum autorisés</param>
        public GameInstance(int maxErrors = 10)
        {
            rnd = new Random();
            this.maxErrors = maxErrors;

            Words = new List<Word>
            {
                new Word("Programmation"),
                new Word("Pentiminax"),
                new Word("Soleil"),
                new Word("Immeuble"),
                new Word("Canapé")
            };

            Guesses = new List<char>();
            Misses = new List<char>();

            WordToGuess = Words[rnd.Next(0, Words.Count)];

            Console.WriteLine("Le mot à deviner contient {0} lettres", WordToGuess.Length);
            currentWordGuessed = PrintWordToGuess();
        }

        /// <summary>
        /// Créer une nouvelle instance du jeu du pendu avec votre propre liste de mots à deviner.
        /// Le mot à deviner est choisi aléatoirement.
        /// </summary>
        /// <param name="words">Liste de mots</param>
        /// <param name="maxErrors">Nombre d'erreurs maximum autorisé</param>
        public GameInstance(List<Word> words, int maxErrors)
        {
            rnd = new Random();

            this.maxErrors = maxErrors;

            Words = words;

            Guesses = new List<char>();
            Misses = new List<char>();

            WordToGuess = Words[rnd.Next(0, Words.Count)];

            Console.WriteLine("Le mot à deviner contient {0} lettres", WordToGuess.Length);
            currentWordGuessed = PrintWordToGuess();
        }

        /// <summary>
        /// Permet de jouer au jeu du pendu.
        /// Cette méthode lit la touche sur laquelle l'utilisateur a appuyé
        /// jusqu'à ce que la partie soit gagné ou perdue (10 erreurs).
        /// </summary>
        public void Play()
        {
            while (!isWin)
            {
                Console.WriteLine("Donnez moi une lettre :");

                char letter = char.ToUpper(Console.ReadKey(true).KeyChar);

                int letterIndex = WordToGuess.GetIndexOf(letter);

                Console.WriteLine();

                if (letterIndex != -1)
                {
                    Console.WriteLine("Bravo, vous avez trouvé la lettre : {0}", letter);
                    Guesses.Add(letter);
                }
                else
                {
                    Console.WriteLine("La lettre {0} ne se trouve pas dans le mot à deviner !", letter);
                    Misses.Add(letter);
                }

                Console.WriteLine($"Erreurs ({Misses.Count}) : {string.Join(", ", Misses)}");

                currentWordGuessed = PrintWordToGuess();

                if (currentWordGuessed.IndexOf('_') == -1)
                {
                    isWin = true;
                    Console.WriteLine("Félicitations, c'est gagné !");
                    Console.ReadKey();
                }

                if (Misses.Count >= maxErrors)
                {
                    Console.WriteLine("C'est perdu !");
                    Console.ReadKey();
                    break;
                }
            }
        }

        /// <summary>
        /// Affiche le mot à deviner 
        /// </summary>
        /// <returns></returns>
        private string PrintWordToGuess()
        {
            string currentWordGuessed = "";

            for (int i = 0; i < WordToGuess.Length; i++)
            {
                if (Guesses.Contains(WordToGuess.Text[i]))
                {
                    currentWordGuessed += WordToGuess.Text[i];
                }
                else
                {
                    currentWordGuessed += "_";
                }
            }

            Console.WriteLine(currentWordGuessed);
            Console.WriteLine();

            return currentWordGuessed;
        }
    }
}
